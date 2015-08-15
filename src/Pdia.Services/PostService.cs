using Pdia.Entities;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class PostService : IPostService
    {
        IPdiaUnitOfWorkFactory _uowFac;
        public PostService(IPdiaUnitOfWorkFactory uowFac)
        {
            _uowFac = uowFac;
        }

        public async Task<Post> FindAsync(Guid Id)
        {
            using (var uow = _uowFac.Create())
            {
                return (await uow.PostRepository.ItemsAsync(q => q.Id == Id)).FirstOrDefault();
            }
        }

        public async Task<Post> InsertAsync(Post post)
        {
            using (var uow = _uowFac.Create())
            {
                post.Id = new Guid();

                uow.PostRepository.Insert(post);
                await uow.SaveChangesAsync();

                return post;
            }
        }

        public async Task<Post> UpdateAsync(Post post)
        {
            using (var uow = _uowFac.Create())
            {
                uow.PostRepository.Update(post);
                await uow.SaveChangesAsync();

                return post;
            }
        }

        public async Task DeleteAsync(Post post)
        {
            using (var uow = _uowFac.Create())
            {
                post.Deleted = true;

                uow.PostRepository.Update(post);
                await uow.SaveChangesAsync();
            }
        }
    }
}
