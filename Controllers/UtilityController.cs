using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using BookStore.Models;
using System.Text;
using System.Text.Json;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilityController : ControllerBase
    {
        [HttpPost("xml-to-json")]
        public IActionResult FromXmltoJson([FromBody] BooksXml books)
        {

            try
            { 
                var json = JsonSerializer.Serialize(books, new JsonSerializerOptions 
                { WriteIndented = true });

                return Ok(json);

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
