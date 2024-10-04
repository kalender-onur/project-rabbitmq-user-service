using Application.Interfaces;
using Application.Interfaces.AppUser;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> AddUserAsync(AppUser appUser)
        {

            try
            {

                var mail = await _unitOfWork.AppUser.GetWhere(x => x.Email.Equals(appUser.Email));
                if (mail.FirstOrDefault() != null)
                {
                    return Result<bool>.FailureResult("Bu mail adresi kullanılmış.");
                }
                await _unitOfWork.AppUser.AddAsync(appUser);
                await _unitOfWork.CompleteAsync();

                return Result<bool>.SuccessResult(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.FailureResult($"Hata: {ex.Message}");

            }
        }
    }
}
