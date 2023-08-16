using System;
using System.Collections.Generic;
using System.Text;


namespace Clash
{
    public interface IArgAttributeData
    {
        string Short { get; set; }
        string Long { get; set; }
        bool Required { get; set; }
        string Description { get; set; }
        object DefaultValue { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ArgAttribute : Attribute, IArgAttributeData
    {
        public string Short { get; set; } //abbreviated name -> max 2 chars...
        public string Long { get; set; }
        public bool Required { get; set; }
        public string Description { get; set; }
        public object DefaultValue { get; set; }
    }
}
