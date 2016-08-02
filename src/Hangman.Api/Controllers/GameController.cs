using System;
using Hangman.Api.Storages;
using Hangman.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Api.Controllers
{
    public class GameController : ControllerBase
    {
        private IGameStorage storage;
        public GameController(IGameStorage storage)
        {
            this.storage = storage;
        }

        [HttpPost("api/new")]
        public ActionResult New([FromBody] string word)
        {
            var game = new Game(word);
            var id = this.storage.Store(game);
            return Ok(new GameStateResult(id, game));
        }

        [HttpPost("api/guess/{id}/{letter}")]
        public ActionResult Guess(Guid id, char letter)
        {
            var game = this.storage.Get(id);
            game.Guess(letter);
            return Ok(new GameStateResult(id, game));
        }
    }
}
