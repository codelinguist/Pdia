using CoreInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public class Pediatrician: IEntity
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public string LicenseNo { get; set; }
        public bool Deleted { get; set; }
        public virtual UserProfile Profile { get; set; }
    }
}
