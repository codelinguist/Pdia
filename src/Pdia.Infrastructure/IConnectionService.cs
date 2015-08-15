using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IConnectionService
    {
        Task<object> AddPatientAsync(Guid childId, Guid pediaId);
        Task RemovePatientAsync(Guid patientId);
        Task<List<object>> GetPatientsAsync(Guid pediaId);
        Task<List<object>> GetPediatriciansAsync(Guid childId);
        
    }
}
