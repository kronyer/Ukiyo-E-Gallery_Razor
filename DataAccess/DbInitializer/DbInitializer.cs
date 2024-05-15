using DataAccess.Data;
using DataAccess.DbInitializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Abby.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(AppDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.UserRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.UserRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.ColaboratorRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.AdministratorRole)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                }, "Admin123*").GetAwaiter().GetResult();

                IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == "admin@admin.com");

                _userManager.AddToRoleAsync(user, SD.AdministratorRole).GetAwaiter().GetResult();



            }
            return;
        }
    }
}