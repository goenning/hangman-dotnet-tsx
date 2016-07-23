namespace Hangman
{
    public class GameOptions
    {
        private readonly string[] _words = new string[] { "House", "Kitchen", "City" };

        public virtual string[] AvailableWords
        {
            get { return this._words; }
        }

        public virtual int MaxMisses
        {
            get { return 5; }
        }
    }
}
