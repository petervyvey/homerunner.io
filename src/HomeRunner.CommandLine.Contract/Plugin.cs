
using System;

namespace HomeRunner.CommandLine
{
    public abstract class Plugin
        : IPlugin
    {
        public Guid SessionId { get; protected set; }

        public abstract void Start(Guid sessionId, string[] args);
    }
}
