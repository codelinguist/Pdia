using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IAuthorizationService
    {
        Task<AppClaim> GetClaimAsync(Guid claimId);
        Task InsertClaimAsync(AppClaim claim);
        Task<AppClaim> UpdateClaimAsync(AppClaim claim);
    }
}
