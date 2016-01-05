
using CommandLine;

namespace WebApiProxy.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments arguments = new Arguments();
            if (Parser.Default.ParseArguments(args, arguments))
            {
                Generator.Generate(arguments);
            }
        }
    }
}
