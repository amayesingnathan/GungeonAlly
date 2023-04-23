using Microsoft.AspNetCore.Mvc;

using GungeonApp.Database;

namespace GungeonApp.API.Controllers
{
    [Route("item")]
    public class ItemController : Controller
    {
        [HttpGet]
        [Route("id/{id}")]
        public IActionResult GetItem([FromRoute] int id)
        {
            try
            {
                var item = GungeonDB.GetItem(id);
                if (item is null)
                {
                    return BadRequest($"Could not locate item with id: {id}");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet]
        [Route("name/{name}")]
        public IActionResult GetItem([FromRoute] string name)
        {
            try
            {
                var item = GungeonDB.GetItem(name);
                if (item is null || item.Length == 0)
                {
                    return BadRequest($"Could not locate gun with name: {name}");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
