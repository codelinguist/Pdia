using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class ClinicService : IClinicService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public ClinicService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task<Clinic> FindAsync(Guid Id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.ClinicRepository.ItemsAsync(q => q.Id == Id)).FirstOrDefault();
            }
        }

        public async Task<Clinic> InsertAsync(Clinic clinic)
        {
            using (var uow = _uowFac.Create())
            {
                clinic.Id = new Guid();

                uow.ClinicRepository.Insert(clinic);
                await uow.SaveChangesAsync();

                return clinic;
            }
        }

        public async Task<Clinic> UpdateAsync(Clinic clinic)
        {
            using (var uow = _uowFac.Create())
            {
                uow.ClinicRepository.Update(clinic);
                await uow.SaveChangesAsync();

                return clinic;
            }
        }

        public async Task DeleteAsync(Clinic clinic)
        {
            using (var uow = _uowFac.Create())
            {
                clinic.Deleted = true;

                uow.ClinicRepository.Update(clinic);
                await uow.SaveChangesAsync();
            }
        }
    }
}
