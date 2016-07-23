using System.Collections.Generic;
using System.Web.Http;

namespace Hangman.Http.Controllers
{
    public class GameController : ApiController
    {
        // GET: api/Game
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Game/5
        public IHttpActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();
            return Ok("Hello World 2");
        }

        // POST: api/Game
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Game/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Game/5
        public void Delete(int id)
        {
        }
    }
}
