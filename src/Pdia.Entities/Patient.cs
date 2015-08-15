using CoreInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public class Patient: IEntity
    {
        public Guid Id { get; set; }

        public Guid PediaId { get; set; }
        public Guid ChildId { get; set; }

        public virtual Child PatientProfile { get; set; }
        public virtual Pediatrician Pedia { get; set; }
    }
}
