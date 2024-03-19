using Microsoft.EntityFrameworkCore;

namespace URL_Shortener.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }


    }
}
