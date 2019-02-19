﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TTMMC_ESSETRE.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-if")]
    public class IfTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-if")] public bool If { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!If) output.SuppressOutput();
        }
    }
}