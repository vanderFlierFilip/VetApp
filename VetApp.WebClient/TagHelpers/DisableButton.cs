using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VDMJasminka.WebClient.TagHelpers
{
    [HtmlTargetElement("button")]
    public class DisableButton : TagHelper
    {
        [HtmlAttributeName("asp-is-disabled")]
        public bool IsDisabled { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsDisabled)
            {
                var d = new TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(d);
            }
            base.Process(context, output);
        }
    }
    [HtmlTargetElement("input")]
    public class ReadonlyInputHelper : TagHelper
    {
        [HtmlAttributeName("asp-is-readonly")]
        public bool IsReadonly { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsReadonly)
            {
                var d = new TagHelperAttribute("readonly", "readonly");
                output.Attributes.Add(d);
            }
            base.Process(context, output);
        }

    }
    [HtmlTargetElement("select")]
    public class DisableSelectHelper : TagHelper
    {
        [HtmlAttributeName("asp-is-disabled")]
        public bool IsDisabled { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsDisabled)
            {
                var d = new TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(d);
            }
            base.Process(context, output);
        }
    }
}
