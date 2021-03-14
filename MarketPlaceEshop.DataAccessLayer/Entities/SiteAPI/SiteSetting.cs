using MarketPlaceEshop.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.DataAccessLayer.Entities.SiteAPI
{
    public class SiteSetting : BaseEntity
    {
        #region تنظیمات وب سایت

        [DisplayName("نام سایت")]
        public string SiteName { get; set; }

        [DisplayName("توضیحات وب سایت")]
        public string SiteDescription { get; set; }

        [DisplayName("کلمات کلیدی")]
        public string SiteKeywordsKey { get; set; }

        [DisplayName("API")]
        public string SmsApi { get; set; }

        [DisplayName("شماره فرستنده")]
        public string SmsSender { get; set; }

        [DisplayName("ایمیل پشتیبانی")]
        public string Email { get; set; }

        [DisplayName("کلمه عبور ایمیل")]
        public string MailPassword { get; set; }

        [DisplayName("متن کپی رایت")]
        public string CopyRight { get; set; }

        [DisplayName("متن فوتر")]
        public string FooterText { get; set; }

        [DisplayName("شماره موبایل پشتیبانی")]
        public string Mobile { get; set; }

        [DisplayName("شماره ثابت فروشگاه")]
        public string Phone { get; set; }

        [DisplayName("آدرس فروشگاه")]
        public string Address { get; set; }

        [DisplayName("اصلی هست / نیست")]
        public bool IsDefault { get; set; }

        #endregion
    }
}
