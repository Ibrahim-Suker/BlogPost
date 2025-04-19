using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasitructure.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        // genaric
        IBaseRepository<Post> _post { get; }
        IBaseRepository<Category> _category { get; }

        // interfaces
        IPostRepo _postRepo { get; }



        int Complete();
        Task<int> CompleteAsync();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();


    }
}
