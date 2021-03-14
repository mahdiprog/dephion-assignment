using Meetings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Meetings.Web.Controllers
{
    public class MeetingController : BaseController
    {
        private readonly ILogger<MeetingController> _logger;

        public MeetingController(ILogger<MeetingController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
    }
    
}
