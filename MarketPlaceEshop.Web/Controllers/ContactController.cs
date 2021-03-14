using GoogleReCaptcha.V3.Interface;
using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Contact;
using MarketPlaceEshop.Web.AllExtensions;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Controllers
{
    public class ContactController : SiteBaseController
    {
        private readonly IContactService _contactService;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IToastNotification _toastNotification;

        public ContactController(IContactService contactService, ICaptchaValidator captchaValidator, IToastNotification toastNotification)
        {
            _contactService = contactService;
            _captchaValidator = captchaValidator;
            _toastNotification = toastNotification;
        }

        #region index

        public async Task<IActionResult> Index()
        {
            return View();
        }

        #endregion

        [HttpGet("contactUs")]
        public async Task<IActionResult> ContactUs()
        {
            return View();
        }

        [HttpPost("contactUs"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(DTO_CreateContactUs contact)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(contact.Captcha))
            {
                _toastNotification.AddErrorToastMessage("کاربرگرامی: کد کپچای شما تأیید نشد.", new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 3000,
                    Text = "تأیید نشدن کد کپچای",
                    Layout = "topCenter",
                    Theme = "mint"
                });
                return View(contact);
            }

            if (ModelState.IsValid)
            {
                var ip = HttpContext.GetUserIp();
                await _contactService.CreateContactUs(contact, HttpContext.GetUserIp(), User.GetUserId());
                _toastNotification.AddSuccessToastMessage("پیام شما با موفقیت ارسال شد.", new NotyOptions()
                {
                    ProgressBar = true,
                    Text = "داده تکراری",
                    Timeout = 3500,
                    Layout = "topCenter",
                    Theme = "mint"
                });
                return RedirectToAction("/");
            }

            return View(contact);
        }
    }
}
