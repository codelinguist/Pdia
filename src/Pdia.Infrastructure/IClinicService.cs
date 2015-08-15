using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IClinicService
    {
        Task<Clinic> FindAsync(Guid Id);
        Task<Clinic> InsertAsync(Clinic clinic);
        Task<Clinic> UpdateAsync(Clinic clinic);
        Task DeleteAsync(Clinic clinic);
    }
}
