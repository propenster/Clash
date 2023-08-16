using System;
using System.Collections.Generic;
using System.Text;

namespace Clash
{
    /// Simple program to greet a person
//#[derive(Parser, Debug)]
//#[command(author, version, about, long_about = None)]
//struct Args
//    {
//    /// Name of the person to greet
//    #[arg(short, long)]
//    name: String,

//    /// Number of times to greet
//    #[arg(short, long, default_value_t = 1)]
//    count: u8,
//}


    public interface ICommandData
    {
        string Author { get; set; }
        string Version { get; set; }
        string About { get; set; }
        string LongAbout { get; set; }

    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandAttribute : Attribute, ICommandData
    {
        public CommandAttribute() { }
        public CommandAttribute(string author, string version, string about, string longAbout)
        {
            Author = author;
            Version = version;
            About = about;
            LongAbout = longAbout;
        }
        public string Author { get; set; }
        public string Version { get; set; }
        public string About { get; set; }
        public string LongAbout { get; set; }
    }
}
