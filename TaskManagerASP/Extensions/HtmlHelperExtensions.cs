using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManagerASP.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent Image(this IHtmlHelper helper, string src, string alt = "", string width = "",
            string height = "")
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            if(!string.IsNullOrEmpty(alt))
                builder.MergeAttribute("alt", alt);
            if(!string.IsNullOrEmpty(width))
                builder.MergeAttribute("width", width);
            if(!string.IsNullOrEmpty(height))
                builder.MergeAttribute("height", height);
            builder.TagRenderMode = TagRenderMode.SelfClosing;

            HtmlString result;
            using (var writer = new StringWriter())
            {
                builder.WriteTo(writer, HtmlEncoder.Default);
                result = new HtmlString(writer.ToString());
            }
            return result;
        }
    }
}
