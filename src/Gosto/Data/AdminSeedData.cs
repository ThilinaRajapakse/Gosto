using Gosto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gosto.Data
{
    public class AdminSeedData
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public AdminSeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("chaturangarajapakshe@gmail.com") == null)
            {
                ApplicationUser administrator = new ApplicationUser()
                {
                    UserName = "chaturangarajapakshe@gmail.com",
                    Email = "chaturangarajapakshe@gmail.com",
                    FirstName = "Thilina",
                    LastName = "Rajapakse",
                    Address = "43/D, Pragathi Mawatha, Peradeniya",
                    PhoneNumber = "0718868786"
                };

                await _userManager.CreateAsync(administrator, "Gosto_admin_1");
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                await _roleManager.CreateAsync(new IdentityRole("Staff"));

                await _userManager.AddToRoleAsync(administrator, "Administrator");
            }
        }
    }
}
