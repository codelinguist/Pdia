using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class UserAccountService : IUserAccountService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public UserAccountService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task<UserAccount> FindAsync(string id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.UserAccountRepository.ItemsAsync(q => q.Id == id)).FirstOrDefault();
            }
        }

        public async Task<UserAccount> InsertAsync(UserAccount userAccount)
        {
            using (var uow = _uowFac.Create())
            {
                userAccount.Id = Guid.NewGuid().ToString();

                uow.UserAccountRepository.Insert(userAccount);
                await uow.SaveChangesAsync();

                return userAccount;
            }
        }

        public async Task<UserAccount> UpdateAsync(UserAccount userAccount)
        {
            using (var uow = _uowFac.Create())
            {
                uow.UserAccountRepository.Update(userAccount);
                await uow.SaveChangesAsync();

                return userAccount;
            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var uow = _uowFac.Create())
            {
                uow.UserAccountRepository.Delete(id);
                await uow.SaveChangesAsync();
            }
        }
    }
}
