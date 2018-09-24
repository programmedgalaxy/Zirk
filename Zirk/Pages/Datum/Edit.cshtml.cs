using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zirk.Models;

namespace Zirk
{
    public class EditModel : PageModel
    {
        private readonly Zirk.Models.ZirkContext _context;

        public EditModel(Zirk.Models.ZirkContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Datum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatumExists(Datum.id))
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

        private bool DatumExists(int id)
        {
            return _context.Datum.Any(e => e.id == id);
        }
    }
}
