using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMP.Alexa
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class Intent : Attribute
    {
        public string Name { get; set; }
        public Intent(string Name)
        {
            this.Name = Name;
        }
    }
}
