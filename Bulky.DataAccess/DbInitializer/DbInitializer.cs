using BookBazaar.DataAccess.Data;
using BookBazaar.Models;
using BookBazaar.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookBazaar.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }


        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

                // create admin user if it does not exist  
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@aliarmaganuygun.com",
                    Email = "admin@aliarmaganuygun.com",
                    Name = "Ali Armağan Uygun",
                    PhoneNumber = "1234567890",
                    StreetAddress = "123 Main St",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10000"
                }, "Admin123*").GetAwaiter().GetResult();
            }

            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@aliarmaganuygun.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
    }
}
