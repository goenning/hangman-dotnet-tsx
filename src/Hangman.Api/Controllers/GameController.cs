using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Hangman.Api.Controllers
{
    public class GameController : ApiController
    {
        private static Dictionary<Guid, Game> instances = new Dictionary<Guid, Game>();

        [HttpPost, Route("api/new")]
        public IHttpActionResult New([FromBody] string word)
        {
            Guid id = Guid.NewGuid();
            instances.Add(id, new Game(word));
            return Ok(new GameStateResult(id, instances[id]));
        }

        [HttpPost, Route("api/guess/{id}/{letter}")]
        public IHttpActionResult Guess(Guid id, char letter)
        {
            instances[id].Guess(letter);
            return Ok(new GameStateResult(id, instances[id]));
        }
    }
}
