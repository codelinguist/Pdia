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
    public class Child
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        public ICollection<UserProfile> Parents { get; set; }
        public ICollection<Pediatrician> Pediatricians { get; set; }
    }
}
