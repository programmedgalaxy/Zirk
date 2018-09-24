using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zirk.Models
{
    [Table("data")]
    public class Datum
    { 
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Data Type")]
        public string datatype { get; set; }

        [Display(Name = "Value")]
        public string value { get; set; }

        public string prevValue { get; set; }

        [Display(Name = "Last Modified")]
        public DateTime editdate { get; set; }

        public int edits { get; set; }
    }
}
