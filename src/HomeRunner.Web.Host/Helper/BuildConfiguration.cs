
using System.Diagnostics;

namespace SlidingApps.Collaboration.Web.Host.Helper
{
    public static class BuildConfiguration
    {
        static BuildConfiguration()
        {
            BuildConfiguration.SetDebugFlag();
        }

        [Conditional("DEBUG")]
        static void SetDebugFlag()
        {
            BuildConfiguration.IsDebug = true;
        }

        public static bool IsDebug { get; private set; }

        public static bool IsRelease
        {
            get { return !BuildConfiguration.IsDebug; }
        }
    }
}