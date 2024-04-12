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
    public class IndexModel : PageModel
    {
        private readonly FilmsRazorPages.Models.ClassContext _context;

        public IndexModel(FilmsRazorPages.Models.ClassContext context)
        {
            _context = context;
        }

        public IList<Film> Film { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Film = await _context.Films.ToListAsync();
        }
    }
}
