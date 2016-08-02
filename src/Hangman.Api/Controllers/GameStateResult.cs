using System;
using System.Collections.Generic;

namespace Hangman.Api.Controllers
{
    public class GameStateResult
    {
        public Guid Id;
        public string Word;
        public IEnumerable<char> Letters;
        public IEnumerable<char> Guesses;
        public GameStatus Status;
        public int RemainingMissesCount;

        public GameStateResult(Guid id, Game game)
        {
            this.Id = id;
            this.Letters = game.Letters;
            this.Guesses = game.Guesses;
            this.Status = game.Status;
            this.RemainingMissesCount = game.RemainingMissesCount;
            this.Word = game.Status == GameStatus.InProgress ? null : game.Word;
        }
    }
}