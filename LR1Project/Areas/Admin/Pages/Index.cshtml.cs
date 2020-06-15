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
    public class IndexModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;

        public IndexModel(WebLab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Flower> Flower { get;set; }

        public async Task OnGetAsync()
        {
            Flower = await _context.Flowers
                .Include(f => f.Group).ToListAsync();
        }
    }
}
