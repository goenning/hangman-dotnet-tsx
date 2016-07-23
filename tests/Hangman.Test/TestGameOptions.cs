namespace Hangman.Test
{
    public class TestGameOptions : GameOptions
    {
        private readonly string[] _words = new string[] { "Space", "Juno", "Jupter" };

        public override string[] AvailableWords
        {
            get { return _words; }
        }

        public override int MaxMisses
        {
            get { return 6; }
        }
    }
}