using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class ChildService : IChildService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public ChildService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task<Child> FindChildAsync(Guid Id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.ChildRepository.ItemsAsync(q => q.Id == Id)).FirstOrDefault();
            }
        }

        public async Task<Child> InsertChildAsync(Child child)
        {
            using (var uow = _uowFac.Create())
            {
                child.Id = new Guid();
                uow.ChildRepository.Insert(child);
                await uow.SaveChangesAsync();
                return child;
            }
        }
    }
}
