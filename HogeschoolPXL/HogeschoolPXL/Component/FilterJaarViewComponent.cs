using HogeschoolPXL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogeschoolPXL.Component
{
	public class FilterJaarViewComponent : ViewComponent
	{
		HogeschoolPXLDbContext Context { get; set; }
		public FilterJaarViewComponent(HogeschoolPXLDbContext context)
		{
			this.Context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			/*ViewData["jaar"] = await Context.AcademieJaar.Select(x=>x).OrderByDescending(x=> x.StartDatum).ToListAsync();*/ // is voor het aan ropen voor de alleen de jaren uit de data base van de users 
			return View(await Context.AcademieJaar.Select(x => x).OrderByDescending(x => x.StartDatum).ToListAsync());
		}
	}
}
