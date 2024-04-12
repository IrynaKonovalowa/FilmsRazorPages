using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FilmsRazorPages.Models;

namespace FilmsRazorPages.Pages
{
    public class EditModel : PageModel
    {
        private readonly FilmsRazorPages.Models.ClassContext _context;
        IWebHostEnvironment _appEnvironment;

        public EditModel(FilmsRazorPages.Models.ClassContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public Film Film { get; set; } = default!;
        [BindProperty]
        public IFormFile uploadedFile { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film =  await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            Film = film;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (uploadedFile != null)
            {
                // Путь к папке Files
                string path = "/Posters/" + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                Film.Poster = path;
            }
            else
            {
                ModelState.AddModelError("", "Veuillez selectionner une poster");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(Film.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }
    }
}
