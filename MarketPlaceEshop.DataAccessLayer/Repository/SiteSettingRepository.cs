using MarketPlaceEshop.DataAccessLayer.Context;
using MarketPlaceEshop.DataAccessLayer.Entities.SiteAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.DataAccessLayer.Repository
{
    public class SiteSettingRepository : ISiteSettingRepository
    {
        private MarketPlaceEshopDbContext _context;

        public SiteSettingRepository(MarketPlaceEshopDbContext context)
        {
            _context = context;
        }
        public SiteSetting GetSetting(int settingId = 1)
        {
            return _context.SiteSettings.Find(settingId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateSetting(SiteSetting setting)
        {
            _context.SiteSettings.Update(setting);
        }

    }
}
