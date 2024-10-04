using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AppUser
{
    public interface IAppUserRepository : IRepository<Domain.Entities.AppUser>
    {
    }
}
