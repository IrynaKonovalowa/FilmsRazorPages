using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FilmsRazorPages.Models;

namespace FilmsRazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly FilmsRazorPages.Models.ClassContext _context;
        IWebHostEnvironment _appEnvironment;

        public CreateModel(FilmsRazorPages.Models.ClassContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Film Film { get; set; } = default!;
        [BindProperty]
        public IFormFile uploadedFile { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            _context.Films.Add(Film);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
