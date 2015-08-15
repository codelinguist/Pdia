using CoreInfrastructure;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class ConnectionService : IConnectionService
    {
        IUnitOfWorkFactory _uowFac;
        public ConnectionService(IUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }
        public Task<object> AddPatientAsync(Guid childId, Guid pediaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> GetPatientsAsync(Guid pediaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> GetPediatriciansAsync(Guid childId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePatientAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }
    }
}
