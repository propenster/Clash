using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clash.Tests
{
    [Command(About = "A simple to use, efficient, and full-featured Command Line Argument Parser", Author = "propenster", Version = "1.0.0.0")]
    internal class CliModel
    {
    
        [Arg(Short = "n", Long = "name", Required = true, Description = "Name of the person to greet", DefaultValue = "John Doe")]
        public string Name { get; set; }
        [Arg(Short = "c", Long = "count", Required = true, Description = "How many times are we trying to greet them for?", DefaultValue = 20)]
        public int Count { get; set; }
        [Arg(Short = "i", Long = "address", Description = "IP Address", DefaultValue = "127.0.0.1")]
        public string IpAddress { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Name, Count, IpAddress);
        }
    
    }
}
