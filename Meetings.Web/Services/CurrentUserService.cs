using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Meetings.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Meetings.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            OfficeId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue("OfficeId"));
            IsAuthenticated = UserId != null;
        }

        public string UserId { get; }
        public int OfficeId { get; }

        public bool IsAuthenticated { get; }
    }
}
