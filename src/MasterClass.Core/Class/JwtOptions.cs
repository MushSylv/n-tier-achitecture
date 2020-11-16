using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Core.Class
{
    public class JwtOptions

    {
        public bool Enabled { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
