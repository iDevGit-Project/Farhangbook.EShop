using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Common.BaseClasses
{
    public interface IHashPasswordHelper
    {
        string MD5Encoding(string password);
    }
}
