using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class ParentingService : IParentingService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public ParentingService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task<Parenting> FindAsync(Guid Id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.ParentingRepository.ItemsAsync(q => q.Id == Id)).FirstOrDefault();
            }
        }

        public async Task<Parenting> InsertAsync(Parenting parenting)
        {
            using (var uow = _uowFac.Create())
            {
                parenting.Id = new Guid();

                uow.ParentingRepository.Insert(parenting);
                await uow.SaveChangesAsync();

                return parenting;
            }
        }

        public async Task<Parenting> UpdateAsync(Parenting parenting)
        {
            using (var uow = _uowFac.Create())
            {
                uow.ParentingRepository.Update(parenting);
                await uow.SaveChangesAsync();

                return parenting;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var uow = _uowFac.Create())
            {
                uow.ParentingRepository.Delete(id);
                await uow.SaveChangesAsync();
            }
        }
    }
}
