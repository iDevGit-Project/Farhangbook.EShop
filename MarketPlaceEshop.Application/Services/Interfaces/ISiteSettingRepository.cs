using MarketPlaceEshop.DataAccessLayer.Entities.SiteAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.Interfaces
{
    public interface ISiteSettingRepository
    {
        SiteSetting GetSetting(int settingId = 1);
        void UpdateSetting(SiteSetting siteSetting);
        void Save();
    }
}
