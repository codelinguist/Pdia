using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class PediatricianService : IPediatricianService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public PediatricianService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }
        public async Task<Pediatrician> FindAsync(Guid id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.PediatricianRepository.ItemsAsync(q =>q.Id == id)).FirstOrDefault();
            }
        }

        public async Task<Pediatrician> InsertAsync(Pediatrician pediatrician)
        {
            using (var uow = _uowFac.Create())
            {
                pediatrician.Id = Guid.NewGuid();

                uow.PediatricianRepository.Insert(pediatrician);
                await uow.SaveChangesAsync();

                return pediatrician;
            }
        }

        public async Task<Pediatrician> UpdateAsync(Pediatrician pediatrician)
        {
            using (var uow = _uowFac.Create())
            {
                uow.PediatricianRepository.Update(pediatrician);
                await uow.SaveChangesAsync();

                return pediatrician;
            }
        }

        public async Task DeleteAsync(Pediatrician pediatrician)
        {
            using (var uow = _uowFac.Create())
            {
                pediatrician.Deleted = true;

                uow.PediatricianRepository.Update(pediatrician);
                await uow.SaveChangesAsync();
            }
        }
    }
}
