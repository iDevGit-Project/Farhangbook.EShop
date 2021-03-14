using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Account;
using MarketPlaceEshop.Web.AllExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Areas.UserPanel.Controllers
{

    public class AccountController : UserBaseController
    {
        #region متد های سازنده

        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;

        public AccountController(IUserService userService, IToastNotification toastNotification)
        {
            _userService = userService;
            _toastNotification = toastNotification;
        }
        #endregion

        #region صفحه اصلی

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region تغییر رمز عبور با قالب اصلی وب سایت

        [HttpGet("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost("ChangePassword"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(DTO_ChangePassword dto_ChangePassword)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.ChangeUserPassword(dto_ChangePassword, User.GetUserId());
                if (res)
                {
                    _toastNotification.AddSuccessToastMessage("کلمه ی عبور شما تغییر یافت", new NotyOptions()
                    {
                        ProgressBar = true,
                        Text = "داده تکراری",
                        Timeout = 3500,
                        Layout = "topCenter",
                        Theme = "mint"
                    });
                    _toastNotification.AddInfoToastMessage("لطفا جهت تکمیل فرایند تغییر کلمه ی عبور ، مجددا وارد سایت شوید", new NotyOptions()
                    {
                        ProgressBar = true,
                        Text = "داده تکراری",
                        Timeout = 4500,
                        Layout = "topCenter",
                        Theme = "mint"
                    });
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
                else
                {
                    _toastNotification.AddWarningToastMessage("کاربرگرامی : لطفا از کلمه ی عبور جدیدی استفاده کنید", new NotyOptions()
                    {
                        ProgressBar = true,
                        Text = "داده تکراری",
                        Timeout = 4500,
                        Layout = "topCenter",
                        Theme = "mint"
                    });
                    //ModelState.AddModelError("NewPassword", "لطفا از کلمه ی عبور جدیدی استفاده کنید");
                }

            }

            return View(dto_ChangePassword);
        }
        #endregion

        #region تغییر رمز عبور با قالب پنل مدیریت

        [HttpGet("DashboardChangePassword")]
        public async Task<IActionResult> DashboardChangePassword()
        {
            return View();
        }

        [HttpPost("DashboardChangePassword"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DashboardChangePassword(DTO_ChangePassword dto_ChangePassword)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.ChangeUserPassword(dto_ChangePassword, User.GetUserId());
                if (res)
                {
                    _toastNotification.AddSuccessToastMessage("کلمه ی عبور شما تغییر یافت", new NotyOptions()
                    {
                        ProgressBar = true,
                        Text = "داده تکراری",
                        Timeout = 3500,
                        Layout = "topCenter",
                        Theme = "mint"
                    });
                    _toastNotification.AddInfoToastMessage("لطفا جهت تکمیل فرایند تغییر کلمه ی عبور ، مجددا وارد سایت شوید", new NotyOptions()
                    {
                        ProgressBar = true,
                        Text = "داده تکراری",
                        Timeout = 4500,
                        Layout = "topCenter",
                        Theme = "mint"
                    });
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
                else
                {
                    _toastNotification.AddWarningToastMessage("کاربرگرامی : لطفا از کلمه ی عبور جدیدی استفاده کنید", new NotyOptions()
                    {
                        ProgressBar = true,
                        Text = "داده تکراری",
                        Timeout = 4500,
                        Layout = "topCenter",
                        Theme = "mint"
                    });
                    //ModelState.AddModelError("NewPassword", "لطفا از کلمه ی عبور جدیدی استفاده کنید");
                }

            }

            return View(dto_ChangePassword);
        }
        #endregion

        #region ویرایش اطلاعات حساب کاربری در پنل مدیریت
        [HttpGet("EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var userProfile = await _userService.GetProfileForEdit(User.GetUserId());
            if (userProfile == null) return NotFound();
            return View(userProfile);
        }

        [HttpPost("EditProfile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(DTO_EditProfile profile, IFormFile avatarImage)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditUserProfile(profile, User.GetUserId(), avatarImage);
                switch (result)
                {
                    case EditUserProfileResult.IsBlocked:
                        _toastNotification.AddWarningToastMessage("کاربرگرامی : حساب کاربری شما بلاک شده است", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "بلاک حساب کاربری",
                            Timeout = 4500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        break;
                    case EditUserProfileResult.IsNotActive:
                        _toastNotification.AddErrorToastMessage("کاربرگرامی : حساب کاربری شما فعال نشده است", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "حساب غیرفعال",
                            Timeout = 4500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        break;
                    case EditUserProfileResult.NotFound:
                        _toastNotification.AddErrorToastMessage("کاربری با مشصخات وارد شده یافت نشد", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "داده تکراری",
                            Timeout = 4000,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        break;
                    case EditUserProfileResult.Success:
                        _toastNotification.AddSuccessToastMessage($"{ profile.FirstName} { profile.LastName} , پروفایل شما با موفقیت ویرایش شد", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "داده تکراری",
                            Timeout = 4000,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return RedirectToAction("Index");
                }
            }
            return View(profile);
        }
        #endregion
    }
}
