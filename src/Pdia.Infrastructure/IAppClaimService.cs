using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IAppClaimService
    {
        Task<AppClaim> FindAsync(Guid Id);
        Task<AppClaim> InsertAsync(AppClaim pediatrician);
        Task<AppClaim> UpdateAsync(AppClaim pediatrician);
        Task DeleteAsync(AppClaim pediatrician);
    }
}
