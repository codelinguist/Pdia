using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IPediatricianService
    {
        Task<Pediatrician> FindAsync(Guid Id);
        Task<Pediatrician> InsertAsync(Pediatrician pediatrician);
        Task<Pediatrician> UpdateAsync(Pediatrician pediatrician);
        Task DeleteAsync(Pediatrician pediatrician);
    }
}
