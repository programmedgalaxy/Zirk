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
    public class DeleteModel : PageModel
    {
        private readonly Zirk.Models.ZirkContext _context;

        public DeleteModel(Zirk.Models.ZirkContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Datum = await _context.Datum.FindAsync(id);

            if (Datum != null)
            {
                _context.Datum.Remove(Datum);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
