using System;
using System.Collections.Generic;
using System.Linq;
using Meetings.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Common.Application.Messaging;
using MassTransit;
using Meetings.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using ReservationService.Messaging.Meetings;
using ReservationService.Messaging.Offices;
using Triskele.Common.Application.Exceptions;

namespace Meetings.Web.Controllers
{
    [Authorize]
    public class MeetingController : BaseController
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly ICurrentUserService _currentUser;
        private readonly IExternalBus _bus;

        public MeetingController(ILogger<MeetingController> logger, ICurrentUserService currentUser, IExternalBus bus)
        {
            _logger = logger;
            _currentUser = currentUser;
            _bus = bus;
        }

        public async Task<IActionResult> IndexAsync(IEnumerable<string> validationErrors)
        {
            var response = await _bus.CreateRequestClient<OfficeGetByIdQuery>()
                .GetResponse<OfficeDto>(new { OfficeId = _currentUser.OfficeId }).ConfigureAwait(false);

            var model = new MeetingViewModel
            {
                OfficeLocation = response.Message.Location,
                Rooms = response.Message.Rooms,
                MovableResources = response.Message.MovableResources,
                ValidationErrors = validationErrors
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(MeetingViewModel model)
        {
            try
            {
                var meeting =await _bus.CreateRequestClient<MeetingAddCommand>().GetResponse<MeetingDto>(new
                {
                    model.RoomId,
                    model.MovableResourceId,
                    model.From,
                    model.To
                })
                                .ConfigureAwait(false);
            }
            catch (RequestFaultException e)
            {
                var validationException = e.Fault.Exceptions.FirstOrDefault();
                return RedirectToAction("Index", validationException?.ExceptionType == "FluentValidation.ValidationException" ?
                    new{validationErrors=validationException.Message.Split("\r\n")} : 
                    new {validationErrors = new[] {validationException.Message}});
            }

            return RedirectToAction("Index");

        }

    }

}
