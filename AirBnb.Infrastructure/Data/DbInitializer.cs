using AirBnb.Application.Common.Interfaces;
using AirBnb.Application.Common.Utility;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public DbInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,ApplicationDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }
        public void Initialize()
        {
            try 
            {
                if (_context.Database.GetPendingMigrations().Count()>0)
                {
                    _context.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                    var adminPassword = _configuration.GetValue<string>("AdminUserConfig:DefaultAdminPassword");
                    _userManager.CreateAsync(new ApplicationUser 
                    {
                        UserName="admin@dotnet.com",
                        Email= "admin@dotnet.com",
                        Name="Admin Manager",
                        NormalizedEmail="ADMIN@DOTNET.COM",
                        NormalizedUserName= "ADMIN@DOTNET.COM",
                        PhoneNumber="1112223333"
                    }, adminPassword).GetAwaiter().GetResult();
                    ApplicationUser user=_context.ApplicationUsers.FirstOrDefault(u=>u.Email== "admin@dotnet.com");
                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
            }
            catch(Exception e) 
            {
                throw;
            }
        }
    }
}
