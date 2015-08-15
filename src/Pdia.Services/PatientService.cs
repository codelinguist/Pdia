using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class PatientService : IPatientService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public PatientService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task<Patient> FindAsync(Guid Id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.PatientRepository.ItemsAsync(q => q.Id == Id)).FirstOrDefault();
            }
        }

        public async Task<Patient> InsertAsync(Patient patient)
        {
            using (var uow = _uowFac.Create())
            {
                patient.Id = new Guid();

                uow.PatientRepository.Insert(patient);
                await uow.SaveChangesAsync();

                return patient;
            }
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            using (var uow = _uowFac.Create())
            {
                uow.PatientRepository.Update(patient);
                await uow.SaveChangesAsync();

                return patient;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var uow = _uowFac.Create())
            {
                uow.PatientRepository.Delete(id);
                await uow.SaveChangesAsync();
            }
        }
    }
}
