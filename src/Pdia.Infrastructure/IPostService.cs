using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IPostService
    {
        Task<Post> FindAsync(Guid Id);
        Task<Post> InsertAsync(Post post);
        Task<Post> UpdateAsync(Post post);
        Task DeleteAsync(Post post);
    }
}
