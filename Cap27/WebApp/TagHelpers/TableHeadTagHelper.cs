using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("tablehead")]
    public class TableHeadTagHelper : TagHelper
    {
        public string BgColor { get; set; } = "light";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "thead";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-white text-center");
            string content = (await output.GetChildContentAsync()).GetContent();


            //output.Content.SetHtmlContent($"<tr><th colspan=\"2\">{content}</th></tr>");

            //TagBuilder mais seuro que digitar html na mao
            TagBuilder header = new TagBuilder("th");
            header.Attributes["colspan"] = "2";
            header.InnerHtml.Append(content);

            TagBuilder row = new TagBuilder("tr");
            row.InnerHtml.AppendHtml(header);

            output.Content.SetHtmlContent(row);
        }
    }
}
