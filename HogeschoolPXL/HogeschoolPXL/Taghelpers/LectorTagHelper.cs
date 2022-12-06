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
    [HtmlTargetElement("Lector")]
    public class LectorTagHelper : TagHelper
    {
        [HtmlAttributeName("search")]
        public string? search { get; set; }
        public UserManager<User> _userManger { get; set; }
        public HogeschoolPXLDbContext _context { get; set; }
        private IWebHostEnvironment _environment;
        public LectorTagHelper( UserManager<User> userManger, HogeschoolPXLDbContext context, IWebHostEnvironment environment)
        {
           
            _userManger = userManger;
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
            var vakke = await _context.VakLector
                .Where(x => x.Lector.Gebruiker.UserId == identityUser.Id)                
                .Include(x => x.vak).ThenInclude(x => x.Handboek)
                .Where(x => x.vak.VakNaam.Contains(search))
                .ToListAsync();
           
            TagBuilder divCard = new TagBuilder("div");
            divCard.Attributes["class"] = "course-org-list";
            foreach (var vak in vakke)
            {
                var leerlingen = await _context.Inschrijving
               .Where(x => x.vakLector.vak.VakId == vak.VakId)
               .ToListAsync();
                divCard.InnerHtml.AppendHtml(UserCard(vak, leerlingen.Count()));
            }
            output.TagName = "div";
            output.Content.SetHtmlContent(divCard);

        }
        private TagBuilder UserCard(VakLector user , int leerlingen)
        {
            TagBuilder divCard = new TagBuilder("div");
            divCard.Attributes["class"] = "card";
            divCard.Attributes["style"] = "width: 18rem;";
            //img
            divCard.InnerHtml.AppendHtml(UserCardImg(user));

            //Titele
            divCard.InnerHtml.AppendHtml(UserCardTitle(user));
            //aantal leerlingen 
            divCard.InnerHtml.AppendHtml(UserCardItem( leerlingen));
            //button
            divCard.InnerHtml.AppendHtml(UserCardButton());

            return divCard;
        }
        private TagBuilder UserCardImg(VakLector vak)
        {
            TagBuilder Image = new TagBuilder("img");
            Image.Attributes["class"] = "card-img-top";
            var path = _environment.WebRootPath;
            var fileExist = false;
            if (vak.vak.Handboek.Afbeelding != null)
            {
                var file = Path.Combine($"{path}\\images", vak.vak.Handboek.Afbeelding);
                fileExist = System.IO.File.Exists(file);
            }
            var imgLink = $"/Images/{vak.vak.Handboek.Afbeelding}";
            string content = $@"<div class = 'card'>";
            if (fileExist)
            {
                Image.Attributes["src"] = $"{imgLink}";
            }
            else
            {
                Image.Attributes["src"] = $"{vak.vak.Handboek.Afbeelding}";
            }

            return Image;
        }
        private TagBuilder UserCardTitle(VakLector vak)
        {
            var volname = vak.vak.VakNaam;
            TagBuilder TittleH5 = new TagBuilder("H4");
            TittleH5.Attributes["class"] = "card-title";
            TittleH5.InnerHtml.Append(volname);
            return TittleH5;
        }
        private TagBuilder UserCardItem( int leerlingen)
        {
            
            TagBuilder cardtext = new TagBuilder("p");
            cardtext.Attributes["class"] = "card-text ";
            cardtext.InnerHtml.AppendHtml("aantal leerligen : " + leerlingen);

            return cardtext;
        }
        private TagBuilder UserCardButton()
        {
            TagBuilder cardButton = new TagBuilder("a");
            cardButton.Attributes["class"] = "btn btn-primary ";
            cardButton.Attributes["asp-action"] = "CursusPgInfo";
            cardButton.InnerHtml.AppendHtml("Go somewhere");

            return cardButton;
        }
    }
}
