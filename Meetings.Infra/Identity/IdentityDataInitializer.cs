
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Meetings.Infra.Identity
{
    public static class IdentityDataInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("user1").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "user1", Email = "user1@localhost", FullName = "Nancy Davolio", OfficeId = 1,EmailConfirmed = true, PhoneNumberConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "123456").GetAwaiter().GetResult();
                
                
            }


            if (userManager.FindByNameAsync("user2").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "user2", Email = "user2@localhost", FullName = "Mark Smith", OfficeId = 2,EmailConfirmed = true, PhoneNumberConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "123456").GetAwaiter().GetResult();
                
            }
        }
    }
}
