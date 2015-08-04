
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SlidingApps.Collaboration.Web.Host.Helper
{
    public static class LinkBuilder
    {
        public static MvcHtmlString FontAwesomeActionLink(this HtmlHelper helper, string linkText, string iconClass, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            iconClass = string.IsNullOrEmpty(iconClass) ? "fa-fw" : iconClass;
            linkText = "<i class=\"fa " + iconClass + "\"></i>" + linkText;
            MvcHtmlString link = helper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);

            return MvcHtmlString.Create(HttpUtility.HtmlDecode(link.ToHtmlString()));
        }

        public static MvcHtmlString FontAwesomeActionLink(this HtmlHelper helper, string linkText, string iconClass, string actionName, string controllerName, string fragment, object routeValues, object htmlAttributes)
        {
            iconClass = string.IsNullOrEmpty(iconClass) ? "fa-fw" : iconClass;
            linkText = "<i class=\"fa " + iconClass + "\"></i>" + linkText;
            MvcHtmlString link = helper.ActionLink(linkText, actionName, controllerName, null, null, fragment, routeValues, htmlAttributes);

            return MvcHtmlString.Create(HttpUtility.HtmlDecode(link.ToHtmlString()));
        }

        public static MvcHtmlString ActionLinkNoEscape(this HtmlHelper html, string linkText, string actionName, string controllerName, object values, object htmlAttributes)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(values);
            RouteValueDictionary htmlValues = new RouteValueDictionary(htmlAttributes);

            UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext, RouteTable.Routes);
            string url = urlHelper.Action(actionName, controllerName);
            url += "?";
            List<string> paramList = new List<string>();
            foreach (KeyValuePair<string, object> pair in routeValues)
            {
                object value = pair.Value ?? "";
                paramList.Add(String.Concat(pair.Key, "=", Convert.ToString(value, CultureInfo.InvariantCulture)));
            }
            url += String.Join("&", paramList.ToArray());

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = string.IsNullOrEmpty(linkText) ? "" : HttpUtility.HtmlEncode(linkText);
            builder.MergeAttributes<string, object>(htmlValues);
            builder.MergeAttribute("href", url);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkWithConfig(this HtmlHelper helper, string linkText, string actionName, string controllerName, object values, object htmlAttributes)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(values);
            RouteValueDictionary htmlValues = new RouteValueDictionary(htmlAttributes);

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext, RouteTable.Routes);
            string url = urlHelper.Action(actionName, controllerName, new { groepCode = routeValues["groepCode"], applicationName = routeValues["applicationName"], config = string.Empty });
            foreach (KeyValuePair<string, object> pair in routeValues)
            {
                if (pair.Key.Equals("config", StringComparison.InvariantCultureIgnoreCase))
                {
                    url += pair.Value + "/";
                }
            }

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = string.IsNullOrEmpty(linkText) ? "" : HttpUtility.HtmlEncode(linkText);
            builder.MergeAttributes<string, object>(htmlValues);
            builder.MergeAttribute("href", url);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
    }
}