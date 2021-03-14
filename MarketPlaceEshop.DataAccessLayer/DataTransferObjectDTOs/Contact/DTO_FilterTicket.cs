using MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Paging;
using MarketPlaceEshop.DataAccessLayer.Entities.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.DataAccessLayer.DataTransferObjectDTOs.Contact
{
    public class DTO_FilterTicket : BasePaging
    {
        #region متد های مربوط به ارسال فیلتر کردن تیکت ها

        public string Title { get; set; }

        public long? UserId { get; set; }

        public FilterTicketState FilterTicketState { get; set; }

        public TicketSection? TicketSection { get; set; }

        public TicketPriority? TicketPriority { get; set; }

        public FilterTicketOrder OrderBy { get; set; }

        public List<Ticket> Tickets { get; set; }

        #endregion

        #region متد های جاری صفحه بندی

        public DTO_FilterTicket SetTickets(List<Ticket> tickets)
        {
            this.Tickets = tickets;
            return this;
        }

        public DTO_FilterTicket SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;
            return this;
        }

        #endregion
    }

    public enum FilterTicketState
    {
        All,
        NotDeleted,
        Deleted
    }

    public enum FilterTicketOrder
    {
        CreateDate_DES,
        CreateDate_ASC,
    }
}
