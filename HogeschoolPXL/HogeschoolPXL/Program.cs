using HogeschoolPXL.Data;
using HogeschoolPXL.Data.DefaultData;
using HogeschoolPXL.Models.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HogeschoolPXLDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("HogeSchoolConnection")); // voor de database
});
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<HogeschoolPXLDbContext>();// voor identity
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



SeeData.PopulateAsync(app);



app.Run();
