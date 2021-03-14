using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Contact;
using MarketPlaceEshop.Web.AllExtensions;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Areas.UserPanel.Controllers
{
    public class TicketController : UserBaseController
    {
        #region متد های سازنده کلاس تیکت

        private readonly IContactService _contactService;
        private readonly IToastNotification _toastNotification;

        public TicketController(IContactService contactService, IToastNotification toastNotification)
        {
            _contactService = contactService;
            _toastNotification = toastNotification;
        }

        #endregion

        #region صفحه اصلی

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Tickets")]
        public async Task<IActionResult> Tickets(DTO_FilterTicket filter)
        {
            filter.UserId = User.GetUserId();
            filter.FilterTicketState = FilterTicketState.NotDeleted;
            filter.OrderBy = FilterTicketOrder.CreateDate_DES;

            return View(await _contactService.FilterTickets(filter));
        }

        #endregion

        #region متد مربوط به ثبت تیکت جدید

        [HttpGet("AddTicket")]
        public async Task<IActionResult> AddTicket()
        {
            return View();
        }

        [HttpPost("AddTicket"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTicket(DTO_AddTicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.AddUserTicket(ticket, User.GetUserId());
                switch (result)
                {
                    case AddTicketResult.Error:
                        _toastNotification.AddErrorToastMessage("کاربرگرامی: ارسال تیکت با خطا موجه گردید. لطفاً در دقایقی دیگر تلاش نمایید", new NotyOptions()
                        {
                            ProgressBar = true,
                            Timeout = 4000,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        break;
                    case AddTicketResult.Success:
                        _toastNotification.AddSuccessToastMessage("تیکت شما با موفقیت ثبت شد", new NotyOptions()
                        {
                            
                            ProgressBar = true,
                            Text = "عملیات موفق",
                            Timeout = 4000,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        _toastNotification.AddInfoToastMessage("پاسخ شما به زودی ارسال خواهد شد", new NotyOptions()
                        {
                            ProgressBar = true,
                            Text = "عملیات موفق",
                            Timeout = 4000,
                            Layout = "topCenter",
                            Theme = "mint"
                        });
                        return RedirectToAction("Index");
                }
            }

            return View(ticket);
        }

        #endregion

        #region متد نمایش جزئیان تیکت

        [HttpGet("Tickets/{TicketsId}")]
        public async Task<IActionResult> TicketDetail(long TicketsId)
        {
            return View();
        }

        #endregion
    }
}
