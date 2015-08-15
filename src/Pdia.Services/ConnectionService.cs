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
                return patient;
            }
        }

        public Task<List<Patient>> GetPatientsAsync(Guid pediaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetPediatriciansAsync(Guid childId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePatientAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }
    }
}
