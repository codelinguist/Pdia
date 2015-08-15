using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IBabyBookService
    {
        Task<BabyBook> FindAsync(Guid Id);
        Task<BabyBook> InsertAsync(BabyBook babyBook);
        Task<BabyBook> UpdateAsync(BabyBook babyBook);
        Task DeleteAsync(BabyBook babyBook);
    }
}
