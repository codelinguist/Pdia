using CoreInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public class Parenting:IEntity
    {
        public Guid Id { get; set; }
        public virtual ICollection<UserProfile> Parents { get; set; }
        public virtual ICollection<Child> Children { get; set; }
    }
}
