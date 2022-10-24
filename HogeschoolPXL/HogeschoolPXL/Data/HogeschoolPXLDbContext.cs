using Microsoft.EntityFrameworkCore;

namespace HogeschoolPXL.Data
{
    public class HogeschoolPXLDbContext : DbContext
    {
        public HogeschoolPXLDbContext(DbContextOptions<HogeschoolPXLDbContext> options)
            : base(options)
        { }


    }
}
