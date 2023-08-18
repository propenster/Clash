using System;
using System.Collections.Generic;
using System.Text;

namespace Clash
{
    internal class Token
    {
        public string Arg { get; set; }
        public string ArgShortToken { get; set; }
        public string ArgLongToken { get; set;}

        public object ArgValue { get; set; }

    }
}
