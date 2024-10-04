
using Application.Interfaces.AppUser;

namespace Application.Interfaces
{
    public interface IUnitOfWork 
    {
        IAppUserRepository AppUser { get; }
        Task<int> CompleteAsync();
    }
}
