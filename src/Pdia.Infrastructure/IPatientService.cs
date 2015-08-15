using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IPatientService
    {
        Task<Patient> FindAsync(Guid Id);
        Task<Patient> InsertAsync(Patient patient);
        Task<Patient> UpdateAsync(Patient patient);
        Task DeleteAsync(Guid id);
    }
}
