using Microsoft.AspNetCore.Mvc;

using GungeonApp.Database;

namespace GungeonApp.API.Controllers
{
    [Route("gun")]
    public class GunController : Controller
    {
        [HttpGet]
        [Route("id/{id}")]
        public IActionResult GetGun([FromRoute] int id)
        {
            try
            {
                var gun = GungeonDB.GetGun(id);
                if (gun is null)
                {
                    return BadRequest($"Could not locate gun with id: {id}");
                }

                return Ok(gun);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet]
        [Route("name/{name}")]
        public IActionResult GetGun([FromRoute] string name)
        {
            try
            {
                var gun = GungeonDB.GetGun(name);
                if (gun is null || gun.Length == 0)
                {
                    return BadRequest($"Could not locate gun with name: {name}");
                }

                return Ok(gun);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
