using GoogleReCaptcha.V3.Interface;
using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region سازنده کلاس متد کنترلر ثبت نام
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IToastNotification _toastNotification;
        public AccountController(IUserService userService, ICaptchaValidator captchaValidator, IToastNotification toastNotification)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
            _toastNotification = toastNotification;
        }
        #endregion

        #region کنترلر ثبت نام
        [HttpGet("register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(DTO_RegisterUser register)
        {
            // بررسی متد گوگل ری کپچا در صورتی که کاربر هیچ مشخصه ای وارد نکند
            if (!await _captchaValidator.IsCaptchaPassedAsync(register.Captcha))
            {
                _toastNotification.AddErrorToastMessage("کاربرگرامی: کد کپچای شما تأیید نشد", new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 3000,
                    Text = "تأیید نشدن کد کپچای",
                    Layout = "topCenter",
                    Theme = "mint"
                });
                return View(register);
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(register);
                switch (result)
                {
                    case RegisterUserResult.MobileExistsError:
                        _toastNotification.AddErrorToastMessage("کاربرگرامی: این شماره قبلاً در سامانه ثبت شده است.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "داده تکراری",
                            Timeout = 3500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        //ModelState.AddModelError("Mobile", "شماره موبایل وارد شده تکراری می باشد");
                        break;
                    case RegisterUserResult.Success:
                        _toastNotification.AddSuccessToastMessage("ثبت نام شما با موفقیت انجام شد", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "داده تکراری",
                            Timeout = 3500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        _toastNotification.AddInfoToastMessage("کد تأیید به شماره موبایل شما ارسال گردید", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "داده تکراری",
                            Timeout = 4500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return RedirectToAction("ActivateMobile", "Account", new { mobile = register.Mobile });
                }
            }
            return View(register);
        }
        #endregion

        #region فعال سازی کد موبایل و ورود به سیستم
        [HttpGet("activate-mobile/{mobile}")]
        public IActionResult ActivateMobile(string mobile)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            var activateMobileDto = new DTO_ActivateMobile { Mobile = mobile };
            return View(activateMobileDto);
        }

        [HttpPost("activate-mobile/{mobile}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateMobile(DTO_ActivateMobile activate)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(activate.Captcha))
            {
                _toastNotification.AddErrorToastMessage("کاربرگرامی: کد کپچای شما تأیید نشد", new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 3000,
                    Text = "تأیید نشدن کد کپچای",
                    Layout = "topCenter",
                    Theme = "mint"
                });
                return View(activate);
            }

            if (ModelState.IsValid)
            {
                var res = await _userService.ActivateMobile(activate);
                if (res)
                {
                    _toastNotification.AddSuccessToastMessage("فعالسازی موبایل با موفقیت انجام شد", new NotyOptions()
                    {
                        ProgressBar = true,
                        Text = "داده تکراری",
                        Timeout = 4000,
                        Layout = "topCenter",
                        Theme = "mint"
                    });
                    return RedirectToAction("Login");
                }

                _toastNotification.AddErrorToastMessage("متأسفانه چنین کاربری در سامانه وجود ندارد", new NotyOptions()
                {
                    ProgressBar = true,
                    Text = "داده تکراری",
                    Timeout = 3500,
                    Layout = "topCenter",
                    Theme = "mint"
                });
            }

            return View(activate);
        }
        #endregion

        #region کنترلر ورود به سیستم
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(DTO_LoginUser login)
        {
            // بررسی متد گوگل ری کپچا در صورتی که کاربر هیچ مشخصه ای وارد نکند
            if (!await _captchaValidator.IsCaptchaPassedAsync(login.Captcha))
            {
                _toastNotification.AddErrorToastMessage("اطلاعات دریافتی کپچا از سرور با خطا مواجه شد.", new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 2000,
                    Text = "تأیید نشدن کد کپچای",
                    Layout = "topCenter",
                    Theme = "mint"
                });
                return View(login);
            }

            if (ModelState.IsValid)
            {
                var res = await _userService.GetUserForLogin(login);
                switch (res)
                {
                    case LoginUserResult.NotFound:
                        _toastNotification.AddErrorToastMessage("متأسفانه چنین کاربری در سامانه وجود ندارد.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "نبود حساب کاربری",
                            Timeout = 2500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return Redirect("login");
                    case LoginUserResult.Danger:
                        _toastNotification.AddErrorToastMessage("شماره موبایل یا رمز عبور استباه است.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "خطا در دریافت اطلاعات",
                            Timeout = 2500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return Redirect("/");
                    case LoginUserResult.NotActivated:
                        _toastNotification.AddWarningToastMessage("کاربرگرامی: حساب کاربری شما هنوز فعال نشده است.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "فعال نبودن حساب کاربری",
                            Timeout = 2500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return Redirect("/");
                    case LoginUserResult.Success:

                        var user = await _userService.GetUserByMobile(login.Mobile);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Mobile),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };

                        await HttpContext.SignInAsync(principal, properties);

                        ViewBag.SuccessMessage = true;
                        _toastNotification.AddSuccessToastMessage("فروشگاه اینترنتی کتاب فرهنگ بوک | خوش آمدید.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "خوش آمدید",
                            Timeout = 2000,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return Redirect("/");
                }
            }

            return View(login);
        }
        #endregion

        #region کنترلر خروج از سیستم
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            _toastNotification.AddInfoToastMessage("شما از سیستم خاج شدید.", new NotyOptions()
            {
                ProgressBar = true,
                Text = "خروج از سیستم",
                Timeout = 2000,
                Layout = "topCenter",
                Theme = "mint"
            });
            return Redirect("/");
        }
        #endregion

        #region کنترلر فراموشی رمز عبور
        [HttpGet("forgotPass")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgotPass"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(DTO_ForgotPassword forgot)
        {
            // بررسی متد گوگل ری کپچا در صورتی که کاربر هیچ مشخصه ای وارد نکند
            if (!await _captchaValidator.IsCaptchaPassedAsync(forgot.Captcha))
            {
                _toastNotification.AddErrorToastMessage("اطلاعات دریافتی کپچا از سرور با خطا مواجه شد.", new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 2000,
                    Text = "تأیید نشدن کد کپچای",
                    Layout = "topCenter",
                    Theme = "mint"
                });
                return View(forgot);
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.RecoveryUserPassword(forgot);
                switch (result)
                {
                    case ForgotPasswordResult.NotFound:
                        _toastNotification.AddErrorToastMessage("متأسفانه چنین کاربری در سامانه وجود ندارد.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "نبود حساب کاربری",
                            Timeout = 1500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        break;
                    case ForgotPasswordResult.Success:
                        _toastNotification.AddSuccessToastMessage("کلمه ی عبور جدید برای شما ارسال شد.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "داده تکراری",
                            Timeout = 3000,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        _toastNotification.AddInfoToastMessage("جهت بالابردن امنیت حساب، رمز عبور خود را تغییر دهید.", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "داده تکراری",
                            Timeout = 5500,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return RedirectToAction("login");
                }
            }

            return View(forgot);
        }
        #endregion
    }
}
