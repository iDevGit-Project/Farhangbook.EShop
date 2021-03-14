using MarketPlaceEshop.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.ImplementationsServices
{
    public class SmsService : ISmsService
    {
        private string apiKey = "4A46564762594F3232463954524E617535456331493834632F6242654D65654C4752564A46696F673873773D";


        // ارسال کد احراز هویت جهت فعالسازی موبایل در وب سایت
        public async Task SendVerificationSms(string mobile, string activationCode)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            await api.VerifyLookup(mobile, activationCode, "AuthVerify");
        }

        // ارسال رمز عبور جدید هنگام فراموشی رمز قبلی
        public async Task SendForgetPasswordSms(string mobile, string password)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            await api.VerifyLookup(mobile, password, "AuthForgetPassword");
        }
    }
}
