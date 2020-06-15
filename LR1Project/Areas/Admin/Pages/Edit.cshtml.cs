using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace LR1Project.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;
      
        private readonly IHostingEnvironment _environment;
        public EditModel(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
        }

        [BindProperty]
        public Flower Flower { get; set; }
        [BindProperty]
        public IFormFile image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Flower = await _context.Flowers
                .Include(f => f.Group).FirstOrDefaultAsync(m => m.FlowerId == id);

            if (Flower == null)
            {
                return NotFound();
            }
           ViewData["FlowerGroupId"] = new SelectList(_context.FlowerGroups, "FlowerGroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string path = "";
            // предыдущее изображение
            string previousImage = String.IsNullOrEmpty(Flower.FlowerImage)
            ? ""
            : Flower.FlowerImage;
            if (image != null)
            {
                // новый файл изображения
                Flower.FlowerImage = Flower.FlowerId + Path.GetExtension(image.FileName);
                // путь для нового файла изображения
                path = Path.Combine(_environment.WebRootPath, "images", Flower.FlowerImage);
            }
            _context.Attach(Flower).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                if (image != null)
                {
                    // если было предыдущее изображение
                    if (!String.IsNullOrEmpty(previousImage))
                    {
                        // если файл существует
                        var fileInfo = _environment.WebRootFileProvider
                        .GetFileInfo("/Images/Flowers/" + previousImage);
                        if (fileInfo.Exists)
                        {
                            var oldPath = Path.Combine(_environment.WebRootPath,
                            "images", previousImage);
                            // удалить предыдущее изображение
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        // сохранить новое изображение
                        await image.CopyToAsync(stream);
                    };
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlowerExists(Flower.FlowerId))
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

        private bool FlowerExists(int id)
        {
            return _context.Flowers.Any(e => e.FlowerId == id);
        }
    }
}
