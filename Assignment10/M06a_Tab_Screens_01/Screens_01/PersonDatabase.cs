using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Screens_01
{
    public static class PersonDatabase
    {
        private const string ConnStr = "Data Source=people.db;Version=3;";

        public static void EnsureDatabase()
        {
            if (!File.Exists("people.db"))
            {
                SQLiteConnection.CreateFile("people.db");
            }

            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Person (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FirstName TEXT NOT NULL,
                        LastName  TEXT NOT NULL,
                        Address   TEXT,
                        Occupation TEXT,
                        Hobby      TEXT,
                        PetPreference INTEGER,
                        HairIndex  INTEGER,
                        EyeIndex   INTEGER,
                        NoseIndex  INTEGER,
                        MouthIndex INTEGER
                    );

                    CREATE TABLE IF NOT EXISTS Occupation (
                        Id   INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Hobby (
                        Id   INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL
                    );
                    INSERT OR IGNORE INTO Occupation(Name) VALUES
                        ('Developer'),('Designer'),('Manager'),('Student'),('Other');
                    INSERT OR IGNORE INTO Hobby(Name) VALUES
                        ('Reading'),('Sports'),('Gaming'),('Cooking'),('Traveling');
                    ";
                    cmd.ExecuteNonQuery();
                }

                SeedLookupTable(conn, "Occupation", new[] { "Developer", "Designer", "Manager", "Student", "Other" });
                SeedLookupTable(conn, "Hobby", new[] { "Reading", "Sports", "Gaming", "Cooking", "Traveling" });
            }
        }


        private static void SeedLookupTable(SQLiteConnection conn, string tableName, string[] values)
        {
            using (var check = conn.CreateCommand())
            {
                check.CommandText = $"SELECT COUNT(*) FROM {tableName};";
                var count = Convert.ToInt32(check.ExecuteScalar());
                if (count == 0)
                {
                    using (var insert = conn.CreateCommand())
                    {
                        insert.CommandText = $"INSERT INTO {tableName}(Name) VALUES (@name);";
                        var p = insert.CreateParameter();
                        p.ParameterName = "@name";
                        insert.Parameters.Add(p);

                        foreach (var v in values)
                        {
                            p.Value = v;
                            insert.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


        public static List<Person> GetAllPeople()
        {
            var list = new List<Person>();
            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, FirstName, LastName, Address, Occupation, Hobby,
                    PetPreference, HairIndex, EyeIndex, NoseIndex, MouthIndex
                    FROM Person
                    ORDER BY Id;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Person
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Address = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                SelectedOccupation = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                SelectedHobby = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                PetPreference = (PetPreference)(reader.IsDBNull(6) ? 0 : reader.GetInt32(6)),
                                HairIndex = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                EyeIndex = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                NoseIndex = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                                MouthIndex = reader.IsDBNull(10) ? 0 : reader.GetInt32(10)
                            });
                        }
                    }
                }
                return list;
            }
        }

        public static List<string> GetAllHobbies()
        {
            var list = new List<string>();

            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Hobby ORDER BY Name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return list;
        }

        public static List<string> GetAllOccupations()
        {
            var list = new List<string>();

            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Occupation ORDER BY Name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return list;
        }

        private static List<string> GetLookupValues(string tableName)
        {
            var list = new List<string>();
            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT FirstName FROM {tableName} ORDER BY FirstName;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                    }
                }
                return list;
            }
        }

        public static void SavePerson(Person p)
        {
            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    if (p.Id == 0)
                    {
                        cmd.CommandText = @"
                            INSERT INTO Person 
                              (FirstName, LastName, Address, Occupation, Hobby, PetPreference,
                               HairIndex, EyeIndex, NoseIndex, MouthIndex)
                            VALUES
                              (@fn, @ln, @addr, @occ, @hob, @pet, @hair, @eye, @nose, @mouth);
                            SELECT last_insert_rowid();";
                    }
                    else
                    {
                        cmd.CommandText = @"
                            UPDATE Person SET
                              FirstName=@fn, LastName=@ln, Address=@addr,
                              Occupation=@occ, Hobby=@hob, PetPreference=@pet,
                              HairIndex=@hair, EyeIndex=@eye, NoseIndex=@nose, MouthIndex=@mouth
                            WHERE Id=@id;";
                        cmd.Parameters.AddWithValue("@id", p.Id);
                    }

                    cmd.Parameters.AddWithValue("@fn", p.FirstName);
                    cmd.Parameters.AddWithValue("@ln", p.LastName);
                    cmd.Parameters.AddWithValue("@addr", p.Address ?? "");
                    cmd.Parameters.AddWithValue("@occ", p.SelectedOccupation ?? "");
                    cmd.Parameters.AddWithValue("@hob", p.SelectedHobby ?? "");
                    cmd.Parameters.AddWithValue("@pet", (int)p.PetPreference);
                    cmd.Parameters.AddWithValue("@hair", p.HairIndex);
                    cmd.Parameters.AddWithValue("@eye", p.EyeIndex);
                    cmd.Parameters.AddWithValue("@nose", p.NoseIndex);
                    cmd.Parameters.AddWithValue("@mouth", p.MouthIndex);

                    if (p.Id == 0)
                    {
                        // ExecuteScalar returns last_insert_rowid()
                        var id = cmd.ExecuteScalar();
                        p.Id = Convert.ToInt32(id);
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void DeletePerson(int id)
        {
            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Person WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
