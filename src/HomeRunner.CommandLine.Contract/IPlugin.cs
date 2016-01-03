
using System;

namespace HomeRunner.CommandLine
{
    public interface IPlugin
    {
        string SessionId { get; }

        void Start(string sessionId, string[] args);
    }
}
