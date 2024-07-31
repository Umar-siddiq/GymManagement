using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GymManagement.Data.Models;

namespace GymManagement.DataAccess
{
    public class GymDbContext : IdentityDbContext<IdentityUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }

        public DbSet<GymUser> GymUsers { get; set; }

        public DbSet<Gym> Gyms { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<GymUser>().HasData(

                new GymUser { Full_Name = "Test Name", Role = "Trainer" ,Gender = "M", Age = 21, City = "Khaitan", Height = 170, Type = "FullBody", Membership = false, Weight = 55   }
                );
         
            builder.Entity<Gym>().HasData(

                new Gym { Id = 1, Location = "Street 12 Abraq Khaitan", DumbBells = 15, Treadmills = 3  },
				new Gym { Id = 2, Location = "Street 23 Salmiya", DumbBells = 20, Treadmills = 6 },
				new Gym { Id = 3, Location = "Street 27 Kuwait City", DumbBells = 25, Treadmills = 5 },
				new Gym { Id = 4, Location = "Street 31 Jabriya", DumbBells = 34, Treadmills = 7 },
				new Gym { Id = 5, Location = "Street 35 Salwa", DumbBells = 17, Treadmills = 4 }
			);  
        
        }

    }
}
