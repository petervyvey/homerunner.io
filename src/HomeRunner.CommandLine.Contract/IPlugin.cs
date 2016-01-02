
using System;

namespace HomeRunner.CommandLine
{
    public interface IPlugin
    {
        Guid SessionId { get; }

        void Start(Guid sessionId, string[] args);
    }
}
