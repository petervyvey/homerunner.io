
using System;

namespace HomeRunner.CommandLine
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute
        : Attribute
    {
        public PluginAttribute(string token)
        {
            this.Token = token;
        }

        public string Token{ get; set; }
    }
}
