using MarketPlaceEshop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.ViewComponents
{

    #region هدر وب سایت فروشگاه
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;
        private readonly IUserService _userService;
        public SiteHeaderViewComponent(ISiteService siteService, IUserService userService)
        {
            _siteService = siteService;
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();
            // در صورتی که کاربر به سایت ورود نکرده بود!
            ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);

            // نمایش نام فرد ثبت نام کننده در پنل کاربری
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);
            }

            return View("SiteHeader");
        }
    }
    #endregion

    #region فوتر وب سایت فروشگاه
    public class SiteFooterViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;
        public SiteFooterViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();

            return View("SiteFooter");
        }
    }
    #endregion

    #region منوی کانواس موبایل وب سایت فروشگاه
    public class CanvasMenuViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;
        private readonly IUserService _userService;

        public CanvasMenuViewComponent(ISiteService siteService, IUserService userService)
        {
            _siteService = siteService;
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();
            // در صورتی که کاربر به سایت ورود نکرده بود!
            ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);

            // نمایش نام فرد ثبت نام کننده در پنل کاربری
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);
            }

            return View("CanvasMenu");
        }
    }
    #endregion

    #region منوی سایدبار وب سایت فروشگاه
    public class SideBarMenuViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;
        public SideBarMenuViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();

            return View("SideBarMenu");
        }
    }
    #endregion

    #region منوی اصلی وب سایت فروشگاه
    public class MenuBarViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;
        public MenuBarViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();

            return View("MenuBar");
        }
    }
    #endregion

    #region کادر جستجو وب سایت فروشگاه
    public class SearchBarViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;
        public SearchBarViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.siteSetting = await _siteService.GetDefaultSiteSetting();

            return View("SearchBar");
        }
    }
    #endregion

    #region اسلایدر فروشگاه

    public class HomeSliderViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;

        public HomeSliderViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _siteService.GetAllActiveSliders();
            return View("HomeSlider", sliders);
        }
    }

    #endregion

}
