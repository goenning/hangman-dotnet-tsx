using Hangman;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Xunit.Extensions;
using System;

namespace Hangman.Test
{
    public class GameTest
    {
        private GameOptions defaultOptions = new GameOptions();
        private GameOptions customOptions = new TestGameOptions();

        [Fact]
        public void ShouldUseGivenWord()
        {
            var game = new Game("Moon");
            Assert.Equal(game.Word, "Moon");
        }

        [Theory, MemberData("InvalidWords")]
        public void ShouldThrowErrorWhenUsingInvalidWord(string word)
        {
            Assert.Throws<ArgumentException>(() => new Game(word));
        }

        public static IEnumerable<object> InvalidWords
        {
            get
            {
                return new object[]
                {
                    new string[] { "hi@gmail" },
                    new string[] { "hello+world" },
                    new string[] { "hello world" }
                };
            }
        }

        [Fact]
        public void ShouldStartWithDefaultValues()
        {
            var game = new Game("Moon", customOptions);
            Assert.Equal(game.Word, "Moon");
            Assert.Empty(game.Misses);
            Assert.Empty(game.Guesses);
            Assert.Equal(game.Letters, new char[] { '_', '_', '_', '_' });
            Assert.Equal(game.Status, GameStatus.InProgress);
            Assert.Equal(game.RemainingMissesCount, 6);
        }

        [Fact]
        public void ShouldUseDefaultValuesWhenOptionsIsNotSet()
        {
            var game = new Game();
            Assert.Equal(game.RemainingMissesCount, defaultOptions.MaxMisses);
            Assert.Contains(game.Word, defaultOptions.AvailableWords);
        }

        [Fact]
        public void ShouldUseDefaultValuesWhenWordIsNull()
        {
            var game = new Game((string)null);
            Assert.Contains(game.Word, defaultOptions.AvailableWords);
        }

        [Fact]
        public void ShouldReturnRandomWordWhenWordIsNotSetButOptionsIs()
        {
            var game = new Game(customOptions);
            Assert.Contains(game.Word, customOptions.AvailableWords);
        }

        [Fact]
        public void ShouldAddToMissGuessListWhenGuessingWrongLetter()
        {
            var game = new Game("Moon", customOptions);
            game.Guess('k');
            Assert.Equal(game.Misses, new char[] { 'k' });
            Assert.Equal(game.Guesses, new char[] { 'k' });
            Assert.Equal(game.RemainingMissesCount, 5);
        }

        [Fact]
        public void ShouldNotAddToMissListButAddToGuessListWhenGuessingRightLetter()
        {
            var game = new Game("Moon", customOptions);
            game.Guess('m');
            Assert.Empty(game.Misses);
            Assert.Equal(game.Guesses, new char[] { 'm' });
            Assert.Equal(game.RemainingMissesCount, 6);
        }

        [Fact]
        public void ShouldNotAddMissGuessListWhenGuessingTheSameWrongLetterTwice()
        {
            var game = new Game("Moon", customOptions);
            game.Guess('k');
            game.Guess('k');
            Assert.Equal(game.Misses, new char[] { 'k' });
            Assert.Equal(game.Guesses, new char[] { 'k' });
            Assert.Equal(game.RemainingMissesCount, 5);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenGuessingWrongLetter()
        {
            var game = new Game("Moon");
            game.Guess('k');
            Assert.Equal(game.Letters, new char[] { '_', '_', '_', '_' });
        }

        [Fact]
        public void ShouldAddLetterToStatusWhenGuessingRightLetter()
        {
            var game = new Game("Moon", customOptions);
            game.Guess('m');
            Assert.Equal(game.Letters, new char[] { 'M', '_', '_', '_' });
        }

        [Fact]
        public void ShouldAddLetterToStatusWhenGuessingRightLetterWithMoreOccurrence()
        {
            var game = new Game("Moon", customOptions);
            game.Guess('o');
            Assert.Equal(game.Letters, new char[] { '_', 'o', 'o', '_' });
        }

        [Fact]
        public void ShouldChangeStatusToWonWhenGuessingWholeWord()
        {
            var game = new Game("Moon", customOptions);
            game.Guess('o');
            game.Guess('n');
            game.Guess('m');
            Assert.Equal(game.Letters, new char[] { 'M', 'o', 'o', 'n' });
            Assert.Equal(game.Status, GameStatus.Won);
        }

        [Fact]
        public void ShouldChangeStatusToLostMaxMissesIsReached()
        {
            var game = new Game("Moon", customOptions);
            game.Guess('a');
            game.Guess('b');
            game.Guess('c');
            game.Guess('d');
            game.Guess('e');
            game.Guess('f');
            Assert.Equal(game.Letters, new char[] { '_', '_', '_', '_' });
            Assert.Equal(game.Status, GameStatus.Lost);
        }

        [Theory, MemberData("ValidLetters")]
        public void ShouldNotThrowErrorWhenGuessingValidLetter(char letter)
        {
            var game = new Game("Moon", customOptions);
            game.Guess(letter);
        }

        public static IEnumerable<object> ValidLetters
        {
            get
            {
                foreach (var letter in "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")
                    yield return new object[] { letter };
            }
        }

        [Theory, MemberData("InvalidLetters")]
        public void ShouldThrowErrorWhenGuessingInvalidLetter(char letter)
        {
            var game = new Game("Moon", customOptions);
            Assert.Throws<ArgumentException>(() => game.Guess(letter));
        }

        public static IEnumerable<object> InvalidLetters
        {
            get
            {
                foreach (var letter in "+@_$%^*!\"/.-")
                    yield return new object[] { letter };
            }
        }
    }
}
