using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hangman
{
    public class Game
    {
        public Game()
            : this(null, new GameOptions())
        {
        }

        public Game(string word)
            : this(word, null)
        {
        }

        public Game(GameOptions options)
            : this(null, options)
        {
        }


        public Game(string word, GameOptions options)
        {
            if (string.IsNullOrWhiteSpace(word))
                word = options.AvailableWords.Random();

            this._word = word;
            this._lcWord = word.ToLower();
            this._options = options;
            this._misses = new HashSet<char>();
            this._guesses = new HashSet<char>();
            this._letters = Enumerable.Repeat('_', this._word.Length).ToList();
        }

        public void Guess(char letter)
        {
            if (!_validChars.IsMatch(letter.ToString()))
                throw new ArgumentException("Invalid letter was given.", "letter");

            letter = char.ToLower(letter);

            this._guesses.Add(letter);
            var indexes = this._lcWord.IndexesOf(letter);
            if (!indexes.Any())
            {
                this._misses.Add(letter);
                return;
            }

            foreach(var idx in indexes)
                this._letters[idx] = this._word[idx];
        }

        public string Word
        {
            get { return this._word; }
        }

        public IEnumerable<char> Misses
        {
            get { return this._misses; }
        }

        public IEnumerable<char> Guesses
        {
            get { return this._guesses; }
        }

        public int RemainingMissesCount
        {
            get { return this._options.MaxMisses - _misses.Count; }
        }

        public GameStatus Status
        {
            get
            {
                if (this.RemainingMissesCount <= 0)
                    return GameStatus.Lost;

                if (this._letters.Contains('_'))
                    return GameStatus.InProgress;

                return GameStatus.Won;
            }
        }

        public IEnumerable<char> Letters
        {
            get { return this._letters; }
        }

        private static Regex _validChars = new Regex("[a-zA-Z0-9]");
        private readonly GameOptions _options;
        private readonly string _word;
        private readonly string _lcWord;
        private IList<char> _letters;
        private ISet<char> _guesses;
        private ISet<char> _misses;

    }
}