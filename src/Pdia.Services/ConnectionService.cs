using CoreInfrastructure;
using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreInfrastructure;

namespace Pdia.Services
{
    public class ConnectionService : IConnectionService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public ConnectionService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }
        public async Task<Patient> AddPatientAsync(Guid childId, Guid pediaId)
        {
            using (var uow = _uowFac.Create())
            {
                var patient = new Patient() { ChildId = childId, PediaId = pediaId };
                uow.PatientRepository.Insert(patient);
                await uow.SaveChangesAsync();
                //var list = await uow.PatientRepository.Items.Include(p => p.Pedia).ToListAsync();
                return patient;
            }
        }

        public async Task<List<Patient>> GetPatientsAsync(Guid pediaId)
        {
            using (var uow = _uowFac.Create())
            {
                return await uow.PatientRepository.Items.Where(p => p.PediaId == pediaId).Include(p => p.ChildProfile).ToListAsync();
            }
        }

        public async Task<List<Patient>> GetPediatriciansAsync(Guid childId)
        {
            using (var uow = _uowFac.Create())
            {
                return await uow.PatientRepository.Items.Where(p => p.ChildId == childId).Include(p => p.Pedia).ToListAsync();
            }
        }

        public async Task RemovePatientAsync(Guid patientId)
        {
            using (var uow = _uowFac.Create())
            {
                uow.PatientRepository.Delete(patientId);
                await uow.SaveChangesAsync();
            }
        }
    }
}
