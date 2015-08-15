using CoreInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public class UserProfile: IEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Photo { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Parenting> Parentings { get; set; }
        //public Guid AsPediatricianId { get; set; }
        //public virtual Pediatrician AsPediatrician { get; set; }
    }
}
