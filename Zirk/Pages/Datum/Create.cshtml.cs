﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zirk.Models;

namespace Zirk
{
    public class CreateModel : PageModel
    {
        private readonly Zirk.Models.ZirkContext _context;

        public CreateModel(Zirk.Models.ZirkContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Datum Datum { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Datum.Add(Datum);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}