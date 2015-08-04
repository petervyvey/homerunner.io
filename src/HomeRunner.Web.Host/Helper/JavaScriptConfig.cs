
using SlidingApps.Collaboration.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SlidingApps.Collaboration.Web.Host.Helper
{
    public static class JavaScriptConfig
    {
        public static IHtmlString ScriptTag(this HtmlHelper helper, string source, string version = null)
        {
            source = source.StartsWith("~/") ? source.Substring(2) : source;
            version = version ?? DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            return new HtmlString(String.Format("<script type=\"text/javascript\" src=\"{0}?v={1}\"></script>", source, version));
        }

        public static IHtmlString ScriptBundle(this HtmlHelper helper, string configurationFileName, string bundleName)
        {
            StringBuilder scripts =  new StringBuilder();
            string configurationContent = File.ReadAllText(helper.ViewContext.HttpContext.Server.MapPath(configurationFileName));

            BundleConfig bundleConfig = configurationContent.FromJson<BundleConfig>();
            Bundle bundle = bundleConfig.Bundles.SingleOrDefault(x => x.Name == bundleName);

            if (bundle == null) throw new Exception(string.Format("UNKNOWN BUNDLE: {0}", bundleName));

            if (bundle.Scripts != null)
            {
                bundle.Scripts.ForEach(script =>
                {
                    if (script.Source.Contains("/**/"))
                    {
                        var folders = Directory.GetDirectories(script.Source.Substring(0, script.Source.IndexOf("/**/", StringComparison.InvariantCultureIgnoreCase) - 1));
                    }
                    else
                    {
                        scripts.AppendLine(helper.ScriptTag(script.Source, bundleConfig.Version).ToString());
                    }
                });
            }

            return new HtmlString(scripts.ToString());
        }

        public static IHtmlString ScriptBundles(this HtmlHelper helper, string configurationFileName, string target = null)
        {
            target = target ?? (BuildConfiguration.IsDebug ? "DEBUG" : "RELEASE");

            StringBuilder scripts = new StringBuilder();
            string configurationContent = File.ReadAllText(helper.ViewContext.HttpContext.Server.MapPath(configurationFileName));

            BundleConfig bundleConfig = configurationContent.FromJson<BundleConfig>();
            if (bundleConfig.Bundles != null)
            {
                bundleConfig.Bundles.ForEach(bundle =>
                {
                    if (bundle.Scripts != null && bundle.RunTime != null && bundle.RunTime.Targets != null &&
                        bundle.RunTime.Targets.Contains(target, StringComparer.InvariantCultureIgnoreCase))
                    {
                        bundle.Scripts.ForEach(script =>
                        {
                            if (script.Source.Contains("/**/"))
                            {
                                List<string> files = new List<string>();

                                string basePath = script.Source.Substring(0,
                                    script.Source.IndexOf("/**/", StringComparison.InvariantCultureIgnoreCase));
                                string path = helper.ViewContext.HttpContext.Server.MapPath(basePath);
                                string wildcard = Path.GetFileName(script.Source);

                                List<string> folders = Directory.GetDirectories(path).ToList();


                                folders.ForEach(folder => files.AddRange(Directory.GetFiles(folder, wildcard).ToList()));
                                files.ForEach(file =>
                                {
                                    string source = string.Format("{0}/{1}", basePath,
                                        file.Replace(path, string.Empty).Replace(Path.DirectorySeparatorChar, '/'));
                                    scripts.AppendLine(helper.ScriptTag(source, bundleConfig.Version).ToString());
                                });
                            }
                            else
                            {
                                scripts.AppendLine(helper.ScriptTag(script.Source, bundleConfig.Version).ToString());
                            }
                        });
                    }
                });
            }

            return new HtmlString(scripts.ToString());
        }
    }

    public class BundleConfig
    {
        public string Version { get; set; }

        public List<Bundle> Bundles { get; set; }
    }

    public class RunTimeConfig
    {
        public List<string> Targets { get; set; }
    }

    public class BuildConfig
    {
        public List<string> Targets { get; set; }
    }

    public class Bundle
    {
        public string Name { get; set; }

        public RunTimeConfig RunTime { get; set; }

        public BuildConfig Build { get; set; }

        public List<Script> Scripts { get; set; }
    }

    public class Script
    {
        public string Source { get; set; }
    }
}