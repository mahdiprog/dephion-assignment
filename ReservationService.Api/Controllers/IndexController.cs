using System;
using System.Collections.Generic;
using System.Linq;
using Common.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReservationService.Application.Interfaces;
using ReservationService.Domain.Models;

namespace ReservationService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<IndexController> _logger;
        private readonly IReservationDbContext _dbContext;

        public IndexController(ILogger<IndexController> logger, IReservationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _dbContext.Rooms.ToList();
        }
    }
}
