using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace LR1Project.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(WebLab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Flower Flower { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Flower = await _context.Flowers.FindAsync(id);

            if (Flower != null)
            {
                _context.Flowers.Remove(Flower);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
