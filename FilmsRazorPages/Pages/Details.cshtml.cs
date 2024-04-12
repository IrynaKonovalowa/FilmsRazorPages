using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmsRazorPages.Models;

namespace FilmsRazorPages.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly FilmsRazorPages.Models.ClassContext _context;

        public DetailsModel(FilmsRazorPages.Models.ClassContext context)
        {
            _context = context;
        }

        public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            else
            {
                Film = film;
            }
            return Page();
        }
    }
}
