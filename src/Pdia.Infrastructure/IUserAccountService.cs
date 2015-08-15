using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IUserAccountService
    {
        Task<UserAccount> FindAsync(string id);
        Task<UserAccount> InsertAsync(UserAccount userAccount);
        Task<UserAccount> UpdateAsync(UserAccount userAccount);
        Task DeleteAsync(string id);
    }
}
