using GungeonApp.Database;
using Microsoft.AspNetCore.Mvc;

namespace GungeonApp.API.Controllers
{
    public class SynergyController : Controller
    {
        [Route("synergy/{itemID}")]
        [HttpGet]
        public IActionResult GetSynergies(int itemID)
        {
            try
            {
                var synergies = GungeonDB.GetSynergies(itemID);
                if (synergies is null || synergies.Length == 0)
                {
                    return BadRequest($"Could not locate any synergies for item ID: {itemID}");
                }

                return Ok(synergies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
