using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class BabyBookService : IBabyBookService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public BabyBookService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }
        public async Task<BabyBook> FindAsync(Guid id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.BabyBookRepository.ItemsAsync(q =>q.Id == id)).FirstOrDefault();
            }
        }

        public async Task<BabyBook> InsertAsync(BabyBook babyBook)
        {
            using (var uow = _uowFac.Create())
            {
                babyBook.Id = Guid.NewGuid();

                uow.BabyBookRepository.Insert(babyBook);
                await uow.SaveChangesAsync();

                return babyBook;
            }
        }

        public async Task<BabyBook> UpdateAsync(BabyBook babyBook)
        {
            using (var uow = _uowFac.Create())
            {
                uow.BabyBookRepository.Update(babyBook);
                await uow.SaveChangesAsync();

                return babyBook;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var uow = _uowFac.Create())
            {
                uow.BabyBookRepository.Delete(id);
                await uow.SaveChangesAsync();
            }
        }
    }
}
