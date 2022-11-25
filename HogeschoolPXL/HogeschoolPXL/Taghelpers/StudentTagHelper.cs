using HogeschoolPXL.Models.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HogeschoolPXL.Taghelpers
{
	// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
	[HtmlTargetElement("Student")]
	public class StudentTagHelper : TagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			 public UserManager<User> _userManger { get; set; }
		public StudentTagHelper(UserManager<User> user)
		{
			_userManger = user;
		}
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var user = ViewContext.HttpContext.User;
			var identityUser = await _userManger.GetUserAsync(user);
			output.Content.SetHtmlContent(UserCard(identityUser));
			

		}
		private TagBuilder UserCard(IdentityUser user)
		{
			TagBuilder divCard = new TagBuilder("div");
			divCard.Attributes["class"] = "card";
			divCard.Attributes["style"] = "width: 18rem;";
			//title
			divCard.InnerHtml.AppendHtml(UserCardTitle());
			if (user != null)
			{
				//email
				divCard.InnerHtml.AppendHtml(UserCardItem("EMAIL", user.Email));
				//username
				divCard.InnerHtml.AppendHtml(UserCardItem("USERNAME", user.UserName));
			}
			return divCard;
		}
		private TagBuilder UserCardTitle(string tittle)
		{
			TagBuilder TittleH5 = new TagBuilder("H5");
			TittleH5.Attributes["class"] = "card-title";
			TittleH5.InnerHtml.Append(tittle);
			return TittleH5;
		}
		private TagBuilder UserCardItem(string Header, string Item)
		{
			TagBuilder CardItem = new TagBuilder("H6");
			CardItem.Attributes["class"] = "card-subtitle mb-2 text-muted p-2";
			CardItem.InnerHtml.Append(Header);
			TagBuilder cardtext = new TagBuilder("p");
			cardtext.Attributes["class"] = "card-text m-1 p-3";
			cardtext.InnerHtml.AppendHtml(Item);
			CardItem.InnerHtml.AppendHtml(cardtext);
			return CardItem;
		}
	}
	
}
