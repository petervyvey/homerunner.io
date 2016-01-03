
using System;

namespace HomeRunner.CommandLine
{
    public abstract class Plugin
        : IPlugin
    {
        public string SessionId { get; protected set; }

        public abstract void Start(string sessionId, string[] args);
    }
}
