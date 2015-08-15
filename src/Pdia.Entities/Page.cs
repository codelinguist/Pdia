using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public class Page
    {
        public Guid Id { get; set; }
   
        public Guid BookId { get; set; }
        public Guid PediatricianId { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public string Notes { get; set; }
        public DateTime DateVisited { get; set; }

        public virtual BabyBook BabyBook { get; set; }
        public virtual Pediatrician Pedia { get; set; }
    }
}
