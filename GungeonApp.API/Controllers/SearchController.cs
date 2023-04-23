using GungeonApp.Database;
using GungeonApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace GungeonApp.API.Controllers
{
    public class SearchController : Controller
    {
        [Route("search/{name}")]
        [HttpGet]
        public IActionResult Search(string name)
        {
            try
            {
                var items = GungeonDB.MatchItem(name);
                if (items is null || items.Length == 0)
                {
                    return BadRequest($"Could not locate any items matching name: {name}");
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
