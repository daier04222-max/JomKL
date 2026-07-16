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
        }
    }
}