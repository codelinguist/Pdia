using Pdia.Infrastructure;
using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public AuthorizationService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task InsertClaimAsync(AppClaim claim)
        {
            using (var uow = _uowFac.Create())
            {
                uow.AppClaimRepository.Insert(claim);
                await uow.SaveChangesAsync();
            }
        }

        public Task<AppClaim> UpdateClaimAsync(AppClaim claim)
        {
            throw new NotImplementedException();
        }

        public async Task<AppClaim> GetClaimAsync(Guid claimId)
        {
            using (var uow = _uowFac.Create())
            {
                var result = await uow.AppClaimRepository.ItemsAsync(i => i.Id == claimId);
                return result.FirstOrDefault();
            }
        }

    }
}
