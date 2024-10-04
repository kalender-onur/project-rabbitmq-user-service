using Application.Interfaces.AppUser;
using Domain.Entities;
using Infrastructure.Repositories;
using Persistence.Context;

namespace Persistence.Repositories.User
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
