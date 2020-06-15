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

using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace LR1Project.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        public CreateModel(WebLab.DAL.Data.ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        public IActionResult OnGet()
        {
        ViewData["FlowerGroupId"] = new SelectList(_context.FlowerGroups, "FlowerGroupId", "GroupName");
            return Page();
        }

        [BindProperty]
        public Flower Flower { get; set; }
        [BindProperty]
        public IFormFile image { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Flowers.Add(Flower);
            await _context.SaveChangesAsync();
            if (image != null)
            {
                Flower.FlowerImage = Flower.FlowerId + Path.GetExtension(image.FileName);
                var path = Path.Combine(_environment.WebRootPath, "images", Flower.FlowerImage);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                };
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
