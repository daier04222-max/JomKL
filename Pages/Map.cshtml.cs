using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using eExploreKL.Models; 

namespace eExploreKL.Pages
{
    public class MapModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MapModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Landmark> Landmarks { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Landmarks = await _context.Landmarks.ToListAsync();

            if (Landmarks.Count == 0)
            {
                Landmarks.Add(new Landmark 
                {
                    Id = 1,
                    Name = "Batu Caves",
                    Description = "Batu Caves is a limestone hill that has a series of caves and cave temples in Gombak, Selangor, Malaysia. It is one of the most popular Hindu shrines outside India.",
                    Latitude = 3.2374,   
                    Longitude = 101.6839, 
                    AudioUrl = "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3" 
                });
            }
        }
    }
}