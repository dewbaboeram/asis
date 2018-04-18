using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AsisLibrary
{
    public class AsisHtmlHelper
    {
        public static string StripHTML(string input)
        {
            //var result = Regex.Replace(input, "<.*?>", String.Empty);
            return HttpUtility.HtmlDecode(input);

        }
    }

    public static class CustomHtmlHelpers
    {
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes, string imageSrc)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("src", VirtualPathUtility.ToAbsolute(imageSrc));
            img.Attributes.Add("alt", linkText);
            img.Attributes.Add("title", linkText);
            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());

        }
    }

}
