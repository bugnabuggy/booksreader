using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/author/book/{bookId}/chapter")]
    [ApiController]
    public class BookChapterController : ControllerBase
    {
        
        [HttpGet]
        public IEnumerable<string> Get(string bookId)
        {

            return new string[] { bookId, "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
