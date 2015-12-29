
using CommandLine;
using CommandLine.Text;

namespace HomeRunner.CommandLine.Import
{
    internal class Arguments
    {
        [Option('v', "verbose", DefaultValue = false, HelpText = "Toon verwerkingsdetail")]
        public bool Verbose { get; set; }

        [Option('f', "file", Required = true, HelpText = "De naam van het import bestand")]
        public string File { get; set; }

        [Option('c', "connection", Required = true, HelpText = "De geconfigureerde database alias")]
        public string Connection { get; set; }

        [HelpOption]
        public string GetHelpText()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
