using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IPageService
    {
        Task<Page> FindAsync(Guid Id);
        Task<Page> InsertAsync(Page page);
        Task<Page> UpdateAsync(Page page);
        Task DeleteAsync(Page page);
    }
}
