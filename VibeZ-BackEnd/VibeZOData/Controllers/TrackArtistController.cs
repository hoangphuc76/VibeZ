using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackArtistController(ITracksArtistRepository tracksArtistRepository, ILogger<TrackArtistController> logger) : ControllerBase
    {
        // GET: api/<TrackArtistController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TrackArtistController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TrackArtistController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TrackArtistController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TrackArtistController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
