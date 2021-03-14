using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Meetings.Infra.Identity
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser> 
    {     
        public UserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)     
        {
        }
      
        protected override async Task<ClaimsIdentity>GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("OfficeId", user.OfficeId.ToString()));         
            return identity;     
        } 
    }
}