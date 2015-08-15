using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class PageService : IPageService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public PageService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task<Page> FindAsync(Guid Id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.PageRepository.ItemsAsync(q => q.Id == Id)).FirstOrDefault();
            }
        }

        public async Task<Page> InsertAsync(Page page)
        {
            using (var uow = _uowFac.Create())
            {
                page.Id = new Guid();

                uow.PageRepository.Insert(page);
                await uow.SaveChangesAsync();

                return page;
            }
        }

        public async Task<Page> UpdateAsync(Page page)
        {
            using (var uow = _uowFac.Create())
            {
                uow.PageRepository.Update(page);
                await uow.SaveChangesAsync();

                return page;
            }
        }

        public async Task DeleteAsync(Page page)
        {
            using (var uow = _uowFac.Create())
            {
                page.Deleted = true;

                uow.PageRepository.Update(page);
                await uow.SaveChangesAsync();
            }
        }
    }
}
