using CoreInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public enum Gender
    {
        Male, Female
    }
    public class Child: IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string Photo { get; set; }

        public Guid ParentingId { get; set; }
        public Parenting Parenting { get; set; }

        public virtual ICollection<Patient> Pediatricians { get; set; }
    }
}
