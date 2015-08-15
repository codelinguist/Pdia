using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IConnectionService
    {
        Task<Patient> AddPatientAsync(Guid childId, Guid pediaId);
        Task RemovePatientAsync(Guid patientId);
        Task<List<Patient>> GetPatientsAsync(Guid pediaId);
        Task<List<Patient>> GetPediatriciansAsync(Guid childId);
        
    }
}
