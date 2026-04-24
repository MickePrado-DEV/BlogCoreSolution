using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCoreSolution.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }


        //aqui iran los dbSet
        public DbSet<Category> Categories { get; set; }
    }

   

    
}
