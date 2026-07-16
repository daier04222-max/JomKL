using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MapApi.Controllers
{
    public class Attraction
    {
        public string? Name { get; set; }
        public string? Subtitle { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? Description { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        [HttpGet("GetAttractions")]
        public ActionResult<IEnumerable<Attraction>> GetAttractions()
        {
            var attractions = new List<Attraction>
            {
                new Attraction { 
                    Name = "Bukit Jalil National Stadium", 
                    Subtitle = "TM Stadium Nasional", 
                    Lat = 3.0548, 
                    Lng = 101.6915, 
                    Description = "Malaysia's largest stadium, hosting major sporting events and international concerts." 
                },
                new Attraction { 
                    Name = "Petronas Twin Towers", 
                    Subtitle = "KLCC", 
                    Lat = 3.1578, 
                    Lng = 101.7115, 
                    Description = "Iconic twin skyscrapers offering breathtaking city views and stunning architecture." 
                },
                new Attraction { 
                    Name = "Batu Caves", 
                    Subtitle = "Gombak", 
                    Lat = 3.2374, 
                    Lng = 101.6833, 
                    Description = "A limestone hill featuring a series of caves and famous Hindu temples." 
                }
            };

            return Ok(attractions);
        }
    }
}