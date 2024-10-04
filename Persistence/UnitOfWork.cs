using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Context;
using Application.Interfaces.AppUser;
using Persistence.Repositories.User;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAppUserRepository AppUser { get; private set; }

        public UnitOfWork(IAppUserRepository appUser)
        {
            AppUser = appUser;
        }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            AppUser = new AppUserRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExecuteInTransactionAsync(Func<Task> action)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await action();

                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
