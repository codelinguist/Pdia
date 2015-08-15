using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IParentingService
    {
        Task<Parenting> FindAsync(Guid Id);
        Task<Parenting> InsertAsync(Parenting parenting);
        Task<Parenting> UpdateAsync(Parenting parenting);
        Task DeleteAsync(Guid id);
    }
}
