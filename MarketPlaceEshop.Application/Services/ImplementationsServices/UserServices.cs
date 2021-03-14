using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.Application.Utilities;
using MarketPlaceEshop.Common.BaseClasses;
using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Account;
using MarketPlaceEshop.DataAccessLayer.Entities.Account;
using MarketPlaceEshop.DataAccessLayer.Interfaces;
using MarketPlaceEshop.Web.AllExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.ImplementationsServices
{
    public class UserServices : IUserService
    {
        #region متد سازنده کلاس

        private readonly IGenericRepository<User> _userRepository;
        private readonly IHashPasswordHelper _hashPasswordHelper;
        private readonly ISmsService _smsService;
        public UserServices(IGenericRepository<User> userRepository, IHashPasswordHelper hashPasswordHelper, ISmsService smsService)
        {
            _userRepository = userRepository;
            _hashPasswordHelper = hashPasswordHelper;
            _smsService = smsService;
        }

        #endregion

        #region متد های مربوط به حساب کاربری، فراموشی رمز عبور، بازیابی و غیره
        public async Task<RegisterUserResult> RegisterUser(DTO_RegisterUser register)
        {
            if (!await IsUserExistByMobileNumber(register.Mobile))
            {
                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Mobile = register.Mobile,
                    Password = _hashPasswordHelper.MD5Encoding(register.Password),
                    MobileActiveCode = new Random().Next(10000, 99999).ToString(),
                    EmailActiveCode = Guid.NewGuid().ToString("N")
                };

                await _userRepository.AddEntity(user);
                await _userRepository.SaveChanges();
                await _smsService.SendVerificationSms(user.Mobile, user.MobileActiveCode);
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.MobileExistsError;
        }

        public async Task<bool> IsUserExistByMobileNumber(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().AnyAsync(s => s.Mobile == mobile);
        }

        public async Task<LoginUserResult> GetUserForLogin(DTO_LoginUser login)
        {
            var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Mobile == login.Mobile);
            if (user == null) return LoginUserResult.NotFound;
            if (!user.IsMobileActive) return LoginUserResult.NotActivated;
            if (user.Password != _hashPasswordHelper.MD5Encoding(login.Password)) return LoginUserResult.NotFound;
            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Mobile == mobile);
        }

        public async Task<bool> ActivateMobile(DTO_ActivateMobile activate)
        {
            var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Mobile == activate.Mobile);
            if (user != null)
            {
                if (user.MobileActiveCode == activate.MobileActiveCode)
                {
                    user.IsMobileActive = true;
                    user.MobileActiveCode = new Random().Next(1000000, 999999999).ToString();
                    await _userRepository.SaveChanges();
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region متد مربوط به بازیابی کلمه عبور
        public async Task<ForgotPasswordResult> RecoveryUserPassword(DTO_ForgotPassword forgot)
        {
            var user = await _userRepository.GetQuery().SingleOrDefaultAsync(f => f.Mobile == forgot.Mobile);
            if (user == null) return ForgotPasswordResult.NotFound;
            var newPassword = new Random().Next(1000000, 999999999).ToString();
            user.Password = _hashPasswordHelper.MD5Encoding(newPassword);
            _userRepository.EditEntity(user);
            await _smsService.SendForgetPasswordSms(user.Mobile, newPassword);
            await _userRepository.SaveChanges();
            return ForgotPasswordResult.Success;
        }

        public async Task<bool> ChangeUserPassword(DTO_ChangePassword changePass, long currentUserId)
        {
            var user = await _userRepository.GetEntityById(currentUserId);
            if (user != null)
            {
                var newPassword = _hashPasswordHelper.MD5Encoding(changePass.NewPassword);
                if (newPassword != user.Password)
                {
                    user.Password = newPassword;
                    _userRepository.EditEntity(user);
                    await _userRepository.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        #endregion

        #region متد ویرایش پروفایل، تصویر، تغییر کلمه عبور و دیگر موارد
        public async Task<DTO_EditProfile> GetProfileForEdit(long userId)
        {
            var user = await _userRepository.GetEntityById(userId);
            if (user == null) return null;

            return new DTO_EditProfile
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar
            };
        }

        public async Task<EditUserProfileResult> EditUserProfile(DTO_EditProfile profile, long userId, IFormFile avatarImage)
        {
            var user = await _userRepository.GetEntityById(userId);
            if (user == null) return EditUserProfileResult.NotFound;

            if (user.IsBlocked) return EditUserProfileResult.IsBlocked;
            if (!user.IsMobileActive) return EditUserProfileResult.IsNotActive;

            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;

            if (avatarImage != null && avatarImage.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(avatarImage.FileName);
                avatarImage.AddImageToServer(imageName, PathExtension.UserAvatarOriginServer, 100, 100, PathExtension.UserAvatarThumbnailServer, user.Avatar);
                user.Avatar = imageName;
            }

            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();

            return EditUserProfileResult.Success;
        }
        #endregion

        #region متد های حذف شونده 
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }
        #endregion
    }
}
