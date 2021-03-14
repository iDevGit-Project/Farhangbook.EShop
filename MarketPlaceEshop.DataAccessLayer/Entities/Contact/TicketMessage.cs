using MarketPlaceEshop.Common.BaseClasses;
using MarketPlaceEshop.DataAccessLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.DataAccessLayer.Entities.Contact
{
    public class TicketMessage : BaseEntity
    {
        #region متد ها و جدول تیکت

        public long TicketId { get; set; }

        public long SenderId { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }

        #endregion

        #region ارتباطات

        public Ticket Ticket { get; set; }

        public User Sender { get; set; }

        #endregion
    }
}
