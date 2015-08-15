using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IChildService
    {
        Task<Child> FindChildAsync(Guid Id);
        Task<Child> InsertChildAsync(Child child);
    }
}
