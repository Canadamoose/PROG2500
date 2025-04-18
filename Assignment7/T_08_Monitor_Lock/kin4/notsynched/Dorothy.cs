using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace kin4.notsynched
{

    public enum CharacterColor { Silver, Brown, Yellow }

    public class Dorothy
    {
        private string favoriteCharacter = "";
        private CharacterColor favoriteColor;

      

        public void SetFavorite(string character, CharacterColor color)
        {
            {
                favoriteCharacter = character;
                Thread.Sleep(1);  
                favoriteColor = color;
            }
        }

        public string GetFavorite()
        {
            string result;

            {
                result = $"{favoriteCharacter} ({favoriteColor})";
                Thread.Sleep(1);  
            }
            return result;
        }
    }
}
