
namespace HomeRunner.CommandLine
{
    public abstract class Plugin
        : IPlugin
    {
        public abstract void Start(string[] args);
    }
}
