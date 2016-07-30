using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Api.Controllers
{
    public class GameController : ControllerBase
    {
        private static Dictionary<Guid, Game> instances = new Dictionary<Guid, Game>();

        [HttpPost("api/new")]
        public ActionResult New([FromBody] string word)
        {
            Guid id = Guid.NewGuid();
            instances.Add(id, new Game(word));
            return Ok(new GameStateResult(id, instances[id]));
        }

        [HttpPost("api/guess/{id}/{letter}")]
        public ActionResult Guess(Guid id, char letter)
        {
            instances[id].Guess(letter);
            return Ok(new GameStateResult(id, instances[id]));
        }
    }
}
