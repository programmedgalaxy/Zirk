using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zirk.Models;

namespace Zirk
{
    public class DetailsModel : PageModel
    {
        private readonly Zirk.Models.ZirkContext _context;

        public DetailsModel(Zirk.Models.ZirkContext context)
        {
            _context = context;
        }

        public Datum Datum { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Datum = await _context.Datum.FirstOrDefaultAsync(m => m.id == id);

            if (Datum == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
