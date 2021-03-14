using PARSGREEN.CORE.RESTful.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Services.ImplementationsServices
{
    public class MessageService
    {
        public void SmsMessageSender(string apikey, string mobile, string body)
        {
            string[] contactNumber = new[] { mobile };
            Message ret = new Message(apikey);
            ret.SendSms(body, contactNumber, contactNumber.ToString());
        }
    }
}
