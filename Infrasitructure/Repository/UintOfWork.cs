using Domain.Models;
using Infrasitructure.Data;
using Infrasitructure.IRepository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasitructure.Repository
{
    public class UintOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;


        public IBaseRepository<Post> _post { get; private set; }
        public IBaseRepository<Category> _category { get; private set; }
        public IPostRepo _postRepo { get; private set; }


        public UintOfWork(AppDbContext context)
        {
            _context = context;
            _post = new BaseRepository<Post>(_context);
            _category = new BaseRepository<Category>(_context);
            _postRepo = new PostRepo(_context);
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try {
                if(_transaction is not null)
                    await _transaction.CommitAsync();
            }
            finally
            {
                if(_transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }

            }
        }
        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_transaction is not null)
                    await _transaction.RollbackAsync();
            }
            finally
            {
                if (_transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }







    }
}
