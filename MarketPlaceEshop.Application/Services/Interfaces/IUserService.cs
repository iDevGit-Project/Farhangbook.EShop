using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Account;
using MarketPlaceEshop.DataAccessLayer.Entities.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region متد های مربوط به حساب کاربری
        Task<RegisterUserResult> RegisterUser(DTO_RegisterUser register);
        Task<LoginUserResult> GetUserForLogin(DTO_LoginUser login);
        Task<User> GetUserByMobile(string mobile);
        // متد بررسی شماره موبایل کاربر در پایگاه داده
        Task<bool> IsUserExistByMobileNumber(string mobile);
        Task<ForgotPasswordResult> RecoveryUserPassword(DTO_ForgotPassword forgot);
        Task<bool> ActivateMobile(DTO_ActivateMobile activate);
        Task<bool> ChangeUserPassword(DTO_ChangePassword changePass, long currentUserId);
        Task<DTO_EditProfile> GetProfileForEdit(long userId);
        Task<EditUserProfileResult> EditUserProfile(DTO_EditProfile profile, long userId, IFormFile avatarImage);
        #endregion
    }
}
