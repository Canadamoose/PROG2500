using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kin4.synched
{
    public enum CharacterColor { Silver, Brown, Yellow }

    public class Dorothy
    {
        private string favoriteCharacter = "";
        private CharacterColor favoriteColor;

        private readonly object lockObj = new object();

        public void SetFavorite(string character, CharacterColor color)
        {
           
            lock (lockObj)
            {
                favoriteCharacter = character;
                Thread.Sleep(1);  // Encourage thread switching
                favoriteColor = color;
            }
        }

        public string GetFavorite()
        {
            string result;

            lock (lockObj)
            {
                result = $"{favoriteCharacter} ({favoriteColor})";
                Thread.Sleep(1);  // Encourage thread switching
            }
            return result;
        }
    }
}
