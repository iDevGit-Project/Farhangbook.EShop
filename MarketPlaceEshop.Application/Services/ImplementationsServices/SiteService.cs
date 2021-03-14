using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.DataAccessLayer.Entities.SiteAPI;
using MarketPlaceEshop.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.ImplementationsServices
{
    public class SiteService : ISiteService
    {
        private readonly IGenericRepository<SiteSetting> _sitesettingRepository;
        private readonly IGenericRepository<Slider> _sliderRepository;
        private readonly IGenericRepository<SiteBanner> _bannerRepository;

        public SiteService(IGenericRepository<SiteSetting> sitesettingRepository, IGenericRepository<Slider> sliderRepository, IGenericRepository<SiteBanner> bannerRepository)
        {
            _sitesettingRepository = sitesettingRepository;
            _sliderRepository = sliderRepository;
            _bannerRepository = bannerRepository;
        }
        public async ValueTask DisposeAsync()
        {
            if (_sitesettingRepository != null) await _sitesettingRepository.DisposeAsync();
            if (_sliderRepository != null) await _sliderRepository.DisposeAsync();
            if (_bannerRepository != null) await _bannerRepository.DisposeAsync();
        }

        #region متد دریافت کلیه آیتم های اسلایدر در وب سایت
        public async Task<List<Slider>> GetAllActiveSliders()
        {
            return await _sliderRepository.GetQuery().AsQueryable().Where(s => s.IsActive && !s.IsDelete).ToListAsync();
        }
        #endregion

        #region متد دریافت کلیه تنظیمات وب سایت
        public async Task<SiteSetting> GetDefaultSiteSetting()
        {
            return await _sitesettingRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.IsDefault && !s.IsDelete);
        }
        #endregion

        #region متدهای مربوط به بنرهای وب سایت

        public async Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements)
        {
            return await _bannerRepository.GetQuery().AsQueryable().Where(s => placements.Contains(s.BannerPlacement)).ToListAsync();
        }

        #endregion
    }
}
