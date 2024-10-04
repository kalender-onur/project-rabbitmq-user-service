using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AppUser
{
    public interface IAppUserService
    {
        Task<Result<bool>> AddUserAsync(Domain.Entities.AppUser appUser);
    }
}
