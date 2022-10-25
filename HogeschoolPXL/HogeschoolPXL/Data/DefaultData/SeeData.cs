namespace HogeschoolPXL.Data.DefaultData
{
    public class SeeData
    {
        public static void Populate(WebApplication app)
        {
            using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<HogeschoolPXLDbContext>())
            {
                //context.database.ens
                if (!context.Student.Any())
                {

                }
            }
        }
    }
}
