using HogeschoolPXL.Data;
using HogeschoolPXL.Models;
using HogeschoolPXL.Models.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HogeschoolPXL.Taghelpers
{
	// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
	[HtmlTargetElement("Student")]
	public class StudentTagHelper : TagHelper
	{

		public UserManager<User> _userManger { get; set; }
		public HogeschoolPXLDbContext _context { get; set; }
		private IWebHostEnvironment _environment;

		public StudentTagHelper(UserManager<User> user, HogeschoolPXLDbContext context, IWebHostEnvironment environment)
		{
			_userManger = user;
			_context = context;
			_environment = environment;
		}


		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{

			var user = ViewContext.HttpContext.User;
			var identityUser = await _userManger.GetUserAsync(user);
			var inschrijving = await _context.Inschrijving
				.Where(x => x.Student.Gebruiker.UserId == identityUser.Id)
				.Include(x=> x.vakLector).ThenInclude(x=> x.Lector).ThenInclude(x => x.Gebruiker)
                .Include(x => x.vakLector).ThenInclude(x => x.vak).ThenInclude(x => x.Handboek)
                .ToListAsync();
            TagBuilder divCard = new TagBuilder("div");
            divCard.Attributes["class"] = "course-org-list";
            foreach (var insch in inschrijving)
			{
                divCard.InnerHtml.AppendHtml(UserCard(insch));
                
			}
            output.TagName = "div";
            output.Content.SetHtmlContent(divCard);

        }
        private TagBuilder UserCard(Inschrijving user)
        {
            TagBuilder divCard = new TagBuilder("div");
            divCard.Attributes["class"] = "card";
            divCard.Attributes["style"] = "width: 18rem;";
            //img
            divCard.InnerHtml.AppendHtml(UserCardImg(user));
           
                //Titele
                divCard.InnerHtml.AppendHtml(UserCardTitle( user));
                //Lector
                divCard.InnerHtml.AppendHtml(UserCardItem( user));
            //button
            divCard.InnerHtml.AppendHtml(UserCardButton());

            return divCard;
        }
        private TagBuilder UserCardImg(Inschrijving inschrijving)
        {
            TagBuilder Image = new TagBuilder("img");
            Image.Attributes["class"] = "card-img-top";
            var path = _environment.WebRootPath;
            var fileExist = false;
            if (inschrijving.vakLector.vak.Handboek.Afbeelding != null)
            {
                var file = Path.Combine($"{path}\\images", inschrijving.vakLector.vak.Handboek.Afbeelding);
                fileExist = System.IO.File.Exists(file);
            }
            var imgLink = $"/Images/{inschrijving.vakLector.vak.Handboek.Afbeelding}";
            string content = $@"<div class = 'card'>";
			if (fileExist)
			{
				Image.Attributes["src"] = $"{imgLink}";
			}else
			{
                Image.Attributes["src"] = $"{inschrijving.vakLector.vak.Handboek.Afbeelding}";
            }
           
            return Image;
        }
        private TagBuilder UserCardTitle(Inschrijving inschrijving)
        {
			var volname = inschrijving.vakLector.vak.VakNaam ;
            TagBuilder TittleH5 = new TagBuilder("H4");
            TittleH5.Attributes["class"] = "card-title";
            TittleH5.InnerHtml.Append(volname);
            return TittleH5;
        }
        private TagBuilder UserCardItem(Inschrijving inschrijving)
        {
			var volname = inschrijving.vakLector.Lector.Gebruiker.VoorNaam + " " + inschrijving.vakLector.Lector.Gebruiker.Naam;
            TagBuilder cardtext = new TagBuilder("p");
            cardtext.Attributes["class"] = "card-text ";
            cardtext.InnerHtml.AppendHtml("Lector : " + volname);
          
            return cardtext;
        }
        private TagBuilder UserCardButton()
        {
            TagBuilder cardButton = new TagBuilder("a");
            cardButton.Attributes["class"] = "btn btn-primary ";
            cardButton.Attributes["href"] = "# ";
            cardButton.InnerHtml.AppendHtml("Go somewhere");

            return cardButton;
        }
    }
}
	

