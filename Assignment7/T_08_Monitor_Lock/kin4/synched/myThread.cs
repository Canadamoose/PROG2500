using System.Threading;

namespace kin4.synched
{
    internal class myThread
    {
        private readonly Dorothy dorothy;
        private readonly string character;
        private readonly CharacterColor color;

        public myThread(Dorothy d, string character, CharacterColor color)
        {
            dorothy = d;
            this.character = character;
            this.color = color;
        }

        public void ThreadProc()
        {
            for (int i = 0; i < 80; i++)
            {
                dorothy.SetFavorite(character, color);
                Thread.Sleep(1);
            }
        }
    }
}