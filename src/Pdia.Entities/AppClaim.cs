using CoreInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Entities
{
    public class AppClaim : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}
