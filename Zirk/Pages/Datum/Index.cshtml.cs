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
    public class IndexModel : PageModel
    {
        private readonly Zirk.Models.ZirkContext _context;

        public IndexModel(Zirk.Models.ZirkContext context)
        {
            _context = context;
        }

        public IList<Datum> Datum { get;set; }

        public async Task OnGetAsync()
        {
            Datum = await _context.Datum.ToListAsync();
        }
    }
}
