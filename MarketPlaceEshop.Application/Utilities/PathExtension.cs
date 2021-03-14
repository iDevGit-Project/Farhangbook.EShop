using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Utilities
{
    public static class PathExtension
    {

        #region متد دریافت اطلاعات مربوط به اسلایدر

        public static string SliderOrigin = "/assets/img/slider/";

        #endregion

        #region متد دریافت اطلاعات مربوط به بنرها در وب سایت

        public static string BannerOrigin = "/assets/img/bg/";

        #endregion

        #region متد دریافت اطلاعات مربوط به تصویر آواتار کاربر در پنل مدیریت

        public static string UserAvatarOrigin = "/Dashboard/assets/images/users/userAvatar/origin/";
        public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Dashboard/assets/images/users/userAvatar/origin/");

        public static string UserAvatarThumbnail = "/Dashboard/assets/images/users/userAvatar/Thumbnail/";
        public static string UserAvatarThumbnailServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Dashboard/assets/images/users/userAvatar/Thumbnail/");

        #endregion

        #region متد دریافت تصویر پیش فرض رمبوط به کاربر در پنل مدیریت

        public static string UserAvatarDefaultOrigin = "/Dashboard/assets/images/users/defaultAvatar/LogoCircle.png";

        #endregion
    }
}
