using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class AppClaimService : IAppClaimService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public AppClaimService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }
        public async Task<AppClaim> FindAsync(Guid id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.AppClaimRepository.ItemsAsync(q =>q.Id == id)).FirstOrDefault();
            }
        }

        public async Task<AppClaim> InsertAsync(AppClaim appClaim)
        {
            using (var uow = _uowFac.Create())
            {
                appClaim.Id = Guid.NewGuid();

                uow.AppClaimRepository.Insert(appClaim);
                await uow.SaveChangesAsync();

                return appClaim;
            }
        }

        public async Task<AppClaim> UpdateAsync(AppClaim appClaim)
        {
            using (var uow = _uowFac.Create())
            {
                uow.AppClaimRepository.Update(appClaim);
                await uow.SaveChangesAsync();

                return appClaim;
            }
        }

        public async Task DeleteAsync(AppClaim appClaim)
        {
            using (var uow = _uowFac.Create())
            {
                uow.AppClaimRepository.Delete(appClaim.Id);
                await uow.SaveChangesAsync();
            }
        }
    }
}
