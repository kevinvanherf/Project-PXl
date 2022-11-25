using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HogeschoolPXL.Taghelpers
{
	// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
	[HtmlTargetElement("tag-name")]
	public class StudentTagHelper : TagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{

		}
	}
}
