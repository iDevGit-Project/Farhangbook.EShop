using MarketPlaceEshop.DataAccessLayer.Entities.SiteAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.Interfaces
{
    public interface ISiteService : IAsyncDisposable
    {
        #region فیلدهای مربوط به سایت

        Task<SiteSetting> GetDefaultSiteSetting();

        #endregion

        #region متد های مربوط به اسلایدر
        Task<List<Slider>> GetAllActiveSliders();

        #endregion

        #region متد های مربوط به بنرهای تبلیغاتی درون وب سایت

        Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements);

        #endregion

    }
}
