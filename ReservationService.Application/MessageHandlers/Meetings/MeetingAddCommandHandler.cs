using System.Linq;
using System.Threading.Tasks;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReservationService.Application.Exceptions;
using ReservationService.Application.Interfaces;
using ReservationService.Messaging.Meetings;
using ReservationService.Messaging.Offices;
using Triskele.Common.Application.Exceptions;

namespace ReservationService.Application.MessageHandlers.Meetings
{
    public class MeetingAddCommandHandler : IConsumer<MeetingAddCommand>, IExternalConsumer
    {
        private readonly IReservationDbContext _dbContext;

        public MeetingAddCommandHandler(IReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<MeetingAddCommand> context)
        {
            // get specified room
            var room = await _dbContext.Rooms.Include(r => r.Resources).Include(r=>r.Office)
               .FirstOrDefaultAsync(o => o.OfficeId == context.Message.RoomId).ConfigureAwait(false);
            if (room == null)
                throw new PrimaryKeyNotFoundException($"Room with Id {context.Message.RoomId} not found");

            // check if meeting is inside office hours
            if(context.Message.From.TimeOfDay<room.Office.WorkingStartTime || context.Message.From.TimeOfDay>room.Office.WorkingEndTime)
                throw new OutsideOfficeHoursException();

            // get movable resource
            if (context.Message.MovableResourceId.HasValue)
            {
                var movableResource = await _dbContext.MovableResources.Include(r => r.Type)
                    .FirstOrDefaultAsync(o => o.ResourceId == context.Message.MovableResourceId).ConfigureAwait(false);
                if (movableResource == null)
                    throw new PrimaryKeyNotFoundException($"Movable resource with Id {context.Message.MovableResourceId} not found");

                // check if room has a resource with the same type of selected resource
                if (room.Resources.Any(sr => sr.ResourceTypeId == movableResource.ResourceTypeId))
                    throw new ResourceExistsInRoomException($"Room has a {movableResource.Type.Name} attached");
            }


            await context.RespondAsync<MeetingDto>(new { });
        }
    }
}
