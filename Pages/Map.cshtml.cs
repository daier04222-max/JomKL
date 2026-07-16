using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace YourProjectName.Pages 
{
    public class Attraction
    {
        public string? Name { get; set; }
        public string? Subtitle { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? Description { get; set; }
    }

    public class MapModel : PageModel
    {
        public List<Attraction> ?Attractions { get; set; }

        public void OnGet()
        {
            Attractions = new List<Attraction>
            {
                new Attraction { Name = "Bukit Jalil National Stadium", Subtitle = "TM Stadium Nasional", Lat = 3.0548, Lng = 101.6915, Description = "Malaysia's largest stadium, hosting major sporting events and international concerts." },
                new Attraction { Name = "Petronas Twin Towers", Subtitle = "KLCC", Lat = 3.1578, Lng = 101.7115, Description = "Iconic twin skyscrapers offering breathtaking city views and stunning architecture." },
                new Attraction { Name = "Batu Caves", Subtitle = "Gombak", Lat = 3.2374, Lng = 101.6833, Description = "A limestone hill featuring a series of caves and famous Hindu temples." },
                new Attraction { Name = "National Mosque of Malaysia", Subtitle = "Masjid Negara", Lat = 3.1415, Lng = 101.6915, Description = "A prominent national mosque surrounded by beautiful, lush green gardens." },
                new Attraction { Name = "Merdeka Square", Subtitle = "Dataran Merdeka", Lat = 3.1486, Lng = 101.6945, Description = "The historical square where Malaya's independence was officially declared." },
                new Attraction { Name = "Bukit Bintang", Subtitle = "Shopping District", Lat = 3.1466, Lng = 101.7111, Description = "The premier shopping and entertainment district of Kuala Lumpur." },
                new Attraction { Name = "Kuala Lumpur Tower", Subtitle = "KL Tower", Lat = 3.1528, Lng = 101.7038, Description = "A communications tower offering spectacular panoramic views of the entire city." },
                new Attraction { Name = "Petaling Street", Subtitle = "Chinatown", Lat = 3.1440, Lng = 101.6976, Description = "A bustling street known for bargains, rich heritage, and amazing street food." },
                new Attraction { Name = "KLCC Park", Subtitle = "Urban Sanctuary", Lat = 3.1556, Lng = 101.7150, Description = "A beautiful urban park featuring a man-made lake and musical water fountain shows." },
                new Attraction { Name = "Suria KLCC", Subtitle = "Premier Shopping", Lat = 3.1582, Lng = 101.7124, Description = "A luxurious shopping mall located at the base of the Petronas Twin Towers." }
            };
        }
    }
}