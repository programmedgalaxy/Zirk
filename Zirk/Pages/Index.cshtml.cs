using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Zirk.Models;

namespace Zirk.Pages
{
    public class IndexModel : PageModel
    {
        public string debugText { get; set; }

        private readonly Zirk.Models.ZirkContext _context;

        public IndexModel(Zirk.Models.ZirkContext context)
        {
            _context = context;
        }

        public IList<Models.Datum> Datum { get; set; }

        public async Task OnGetAsync()
        {
            Datum = await _context.Datum.ToListAsync();
            //debugText = new Random().Next().ToString();
        }

        // Set the data input fields to the values from the selected item
        public async Task EditData(int id)
        {
            await Task.Run(() =>
            {
                ViewData["new_ID"] = id;
                ViewData["data_contents"] = _context.Datum.Find(id);
            });
        }

        // Delete the selected item from the database
        public async Task DeleteData(int id)
        {
            _context.Datum.Remove(_context.Datum.Find(id));
            await _context.SaveChangesAsync();
        }

        // Handles actions performed by form submissions
        public async Task OnPostAsync()
        {
            var op = Request.Form["op"];

            if (op.Equals("edit"))
            {   // calls the EditData task on the selected ID
                int.TryParse(Request.Form["id"], out int id);
                await EditData(id);
            }
            else if (op.Equals("delete"))
            {   // calls the DeleteData task on the selected ID
                int.TryParse(Request.Form["id"], out int id);
                await DeleteData(id);
            }
            else
            {
                var id = Request.Form["new_ID"];
                var datatype = Request.Form["data_type"];
                var value = Request.Form["data_contents"];

                // type inference
                if (int.TryParse(value, out int valInt)) datatype = "Integer";
                else if (double.TryParse(value, out double valFloat)) datatype = "Float";
                else datatype = "String";

                // get an integer form of the selected ID
                int.TryParse(id[0], out int idInt);

                if (id.Equals("New Data")) // Value when nothing has been written in the ID field
                {   // create a new Datum and write the values to the database
                    int newID = -1;
                    foreach (var i in _context.Datum)
                    {   // Determine the ID to assign to the new item
                        if (i.id > newID) newID = i.id;
                    }
                    newID++;

                    Models.Datum newDatum = new Models.Datum
                    {
                        id = newID,
                        datatype = datatype[0],
                        value = value[0],
                        editdate = DateTime.Now,
                        edits = 0
                    };

                    _context.Datum.Add(newDatum);
                    await _context.SaveChangesAsync();
                }
                else
                {   // modify an existing data entry by replacing it with a new one of the same ID
                    if (_context.Datum.Find(idInt) is null) debugText = "Invalid data ID specified.";
                    else
                    {
                        Models.Datum newDatum = new Models.Datum
                        {
                            id = _context.Datum.Find(idInt).id,
                            datatype = datatype[0],
                            value = value[0],
                            editdate = DateTime.Now,
                            edits = _context.Datum.Find(idInt).edits + 1    // add an edit to the counter
                        };
                        _context.Datum.Remove(_context.Datum.Find(idInt));
                        _context.Datum.Add(newDatum);
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
