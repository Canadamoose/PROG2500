using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Data.Sqlite;


namespace Screens_01
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly string _connStr = "Data Source=people.db;";
        private ObservableCollection<Person> _people = new ObservableCollection<Person>();
        private int _currentIndex = -1;
        private Person _person = new Person();
        private int _currentTabIndex;

        public ObservableCollection<Person> People
        {
            get => _people;
            set { _people = value; OnPropertyChanged(); }
        }

        public Person Person
        {
            get => _person;
            set { _person = value; OnPropertyChanged(); }
        }

        private string _searchLastName;
        public string SearchLastName
        {
            get => _searchLastName;
            set { _searchLastName = value; OnPropertyChanged(); }
        }

        public int CurrentTabIndex
        {
            get => _currentTabIndex;
            set
            {
                _currentTabIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanGoBackTab));
                OnPropertyChanged(nameof(CanGoForwardTab));
            }
        }
        public bool CanGoBackTab => CurrentTabIndex > 0;
        public bool CanGoForwardTab => CurrentTabIndex < 3;

        public ICommand PreviousTabCommand { get; }
        public ICommand NextTabCommand { get; }
        public ICommand PreviousRecordCommand { get; }
        public ICommand NextRecordCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        public MainViewModel()
        {
            EnsureDatabaseExists();
            PreviousTabCommand = new RelayCommand(_ => CurrentTabIndex--, _ => CanGoBackTab);
            NextTabCommand = new RelayCommand(_ => CurrentTabIndex++, _ => CanGoForwardTab);
            PreviousRecordCommand = new RelayCommand(_ => ChangeRecord(-1), _ => _currentIndex > 0);
            NextRecordCommand = new RelayCommand(_ => ChangeRecord(+1), _ => _people != null && _currentIndex < _people.Count - 1);
            SaveCommand = new RelayCommand(_ => SavePerson(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => ReloadPerson(), _ => true);
            DeleteCommand = new RelayCommand(_ => DeletePerson(), _ => Person?.Id > 0);
            SearchCommand = new RelayCommand(_ => SearchByLastName(), _ => !string.IsNullOrWhiteSpace(SearchLastName));

            LoadAllPeople();
            if (_people.Any()) ChangeRecord(0);
            else Person = new Person();
        }

        private void EnsureDatabaseExists()
        {
            if (!System.IO.File.Exists("people.db"))
            {

                using (var conn = new SqliteConnection(_connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            CREATE TABLE IF NOT EXISTS Person (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                fname TEXT NOT NULL,
                                lname TEXT NOT NULL,
                                address TEXT,
                                Occupation TEXT,
                                Hobby TEXT,
                                PetPreference INTEGER,
                                HairIndex INTEGER,
                                EyeIndex INTEGER,
                                NoseIndex INTEGER,
                                MouthIndex INTEGER
                            );";
                        cmd.ExecuteNonQuery();
                    }
                }

            }
        }

        private void LoadAllPeople()
        {
            var list = new ObservableCollection<Person>();
            using (var conn = new SqliteConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqliteCommand(
                    "SELECT Id, fname, lname, address, Occupation, Hobby, PetPreference, HairIndex, EyeIndex, NoseIndex, MouthIndex FROM Person ORDER BY Id", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Person
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Address = reader.GetString(3),
                            SelectedOccupation = reader.GetString(4),
                            SelectedHobby = reader.GetString(5),
                            PetPreference = (PetPreference)reader.GetInt32(6),
                            HairIndex = reader.GetInt32(7),
                            EyeIndex = reader.GetInt32(8),
                            NoseIndex = reader.GetInt32(9),
                            MouthIndex = reader.GetInt32(10)
                        });
                    }
                }
            }
            People = list;
        }

        private void ChangeRecord(int offset)
        {
            if (offset == 0 && _currentIndex == -1) _currentIndex = 0;
            else _currentIndex += offset;

            if (_currentIndex >= 0 && _currentIndex < _people.Count)
                Person = new Person(_people[_currentIndex]);
        }

        private bool CanSave()
            => Person != null && !string.IsNullOrWhiteSpace(Person.FirstName) && !string.IsNullOrWhiteSpace(Person.LastName);

        private void SavePerson()
        {
            using (var conn = new SqliteConnection(_connStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    if (Person.Id == 0)
                    {
                        cmd.CommandText =
                            "INSERT INTO Person(fname,lname,address,Occupation,Hobby,PetPreference,HairIndex,EyeIndex,NoseIndex,MouthIndex) " +
                            "VALUES(@fn,@ln,@addr,@occ,@hobby,@pet,@hair,@eye,@nose,@mouth);";
                        cmd.Parameters.AddWithValue("@fn", Person.FirstName);
                        cmd.Parameters.AddWithValue("@ln", Person.LastName);
                        cmd.Parameters.AddWithValue("@addr", Person.Address ?? string.Empty);
                        cmd.Parameters.AddWithValue("@occ", Person.SelectedOccupation ?? string.Empty);
                        cmd.Parameters.AddWithValue("@hobby", Person.SelectedHobby ?? string.Empty);
                        cmd.Parameters.AddWithValue("@pet", (int)Person.PetPreference);
                        cmd.Parameters.AddWithValue("@hair", Person.HairIndex);
                        cmd.Parameters.AddWithValue("@eye", Person.EyeIndex);
                        cmd.Parameters.AddWithValue("@nose", Person.NoseIndex);
                        cmd.Parameters.AddWithValue("@mouth", Person.MouthIndex);
                        cmd.ExecuteNonQuery();
                        // Get last inserted Id
                        cmd.CommandText = "SELECT last_insert_rowid();";
                        Person.Id = (int)(long)cmd.ExecuteScalar();
                        _people.Add(new Person(Person));
                        _currentIndex = _people.Count - 1;
                    }
                    else
                    {
                        cmd.CommandText =
                            "UPDATE Person SET fname=@fn,lname=@ln,address=@addr,Occupation=@occ,Hobby=@hobby,PetPreference=@pet,HairIndex=@hair,EyeIndex=@eye,NoseIndex=@nose,MouthIndex=@mouth WHERE Id=@id";
                        cmd.Parameters.AddWithValue("@fn", Person.FirstName);
                        cmd.Parameters.AddWithValue("@ln", Person.LastName);
                        cmd.Parameters.AddWithValue("@addr", Person.Address ?? string.Empty);
                        cmd.Parameters.AddWithValue("@occ", Person.SelectedOccupation ?? string.Empty);
                        cmd.Parameters.AddWithValue("@hobby", Person.SelectedHobby ?? string.Empty);
                        cmd.Parameters.AddWithValue("@pet", (int)Person.PetPreference);
                        cmd.Parameters.AddWithValue("@hair", Person.HairIndex);
                        cmd.Parameters.AddWithValue("@eye", Person.EyeIndex);
                        cmd.Parameters.AddWithValue("@nose", Person.NoseIndex);
                        cmd.Parameters.AddWithValue("@mouth", Person.MouthIndex);
                        cmd.Parameters.AddWithValue("@id", Person.Id);
                        cmd.ExecuteNonQuery();
                        _people[_currentIndex] = new Person(Person);
                    }
                }
            }
            MessageBox.Show("Record saved to database.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeletePerson()
        {
            if (Person.Id > 0)
            {
                using (var conn = new SqliteConnection(_connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Person WHERE Id=@id";
                        cmd.Parameters.AddWithValue("@id", Person.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                _people.RemoveAt(_currentIndex);
                if (_currentIndex >= _people.Count) _currentIndex = _people.Count - 1;
                ChangeRecord(0);
            }
        }

        private void SearchByLastName()
        {
            int idx = _people.ToList().FindIndex(p =>
                string.Equals(p.LastName, SearchLastName, StringComparison.OrdinalIgnoreCase));
            if (idx >= 0)
            {
                _currentIndex = idx;
                ChangeRecord(0);
            }
            else MessageBox.Show("Person not found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReloadPerson()
        {
            if (_currentIndex >= 0 && _currentIndex < _people.Count)
                Person = new Person(_people[_currentIndex]);
            else Person = new Person();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class Person : INotifyPropertyChanged
    {
        private int _id; public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        private string _firstName; public string FirstName { get => _firstName; set { _firstName = value; OnPropertyChanged(); } }
        private string _lastName; public string LastName { get => _lastName; set { _lastName = value; OnPropertyChanged(); } }
        private string _address; public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }
        private string _occ; public string SelectedOccupation { get => _occ; set { _occ = value; OnPropertyChanged(); } }
        private string _hobby; public string SelectedHobby { get => _hobby; set { _hobby = value; OnPropertyChanged(); } }
        private PetPreference _pet; public PetPreference PetPreference { get => _pet; set { _pet = value; OnPropertyChanged(); } }
        private int _hair; public int HairIndex { get => _hair; set { _hair = value; OnPropertyChanged(); } }
        private int _eye; public int EyeIndex { get => _eye; set { _eye = value; OnPropertyChanged(); } }
        private int _nose; public int NoseIndex { get => _nose; set { _nose = value; OnPropertyChanged(); } }
        private int _mouth; public int MouthIndex { get => _mouth; set { _mouth = value; OnPropertyChanged(); } }

        public Person() { }
        public Person(Person other)
        {
            Id = other.Id; FirstName = other.FirstName; LastName = other.LastName;
            Address = other.Address; SelectedOccupation = other.SelectedOccupation;
            SelectedHobby = other.SelectedHobby; PetPreference = other.PetPreference;
            HairIndex = other.HairIndex; EyeIndex = other.EyeIndex;
            NoseIndex = other.NoseIndex; MouthIndex = other.MouthIndex;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) { _execute = execute; _canExecute = canExecute; }
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged { add => CommandManager.RequerySuggested += value; remove => CommandManager.RequerySuggested -= value; }
    }

    public enum PetPreference { None, Dog, Cat }
}
