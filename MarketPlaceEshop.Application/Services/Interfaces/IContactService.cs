using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.Interfaces
{
    public interface IContactService : IAsyncDisposable
    {
        Task CreateContactUs(DTO_CreateContactUs contact, string userIp, long? userId);

        #region متد های مربوط به ارسال و فیلترینگ تیکت
        Task<AddTicketResult> AddUserTicket(DTO_AddTicketViewModel ticket, long userId);
        Task<DTO_FilterTicket> FilterTickets(DTO_FilterTicket filter);

        #endregion
    }
}
