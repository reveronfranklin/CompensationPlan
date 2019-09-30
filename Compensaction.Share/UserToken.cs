using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
