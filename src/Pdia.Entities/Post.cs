using CoreInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public class Post : IEntity
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public string Topic { get; set; }
        public string File { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
        public Enums.Enums.PrivacySetting PrivacySetting { get; set; }

        public virtual UserProfile Parent { get; set; }
    }
}
