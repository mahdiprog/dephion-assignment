using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReservationService.Application.Interfaces;
using ReservationService.Messaging.Offices;
using Triskele.Common.Application.Exceptions;

namespace ReservationService.Application.MessageHandlers.Offices
{
    public class MeetingAddCommandHandler: IConsumer<OfficeGetByIdQuery>,IExternalConsumer
    {
        private readonly IReservationDbContext _dbContext;

        public MeetingAddCommandHandler(IReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<OfficeGetByIdQuery> context)
        {
            var office = await _dbContext.Offices.Include(o=>o.Rooms).ThenInclude(r=>r.Resources)
                .Include(o=>o.MovableResources)
                .FirstOrDefaultAsync(o=>o.OfficeId==context.Message.OfficeId).ConfigureAwait(false);
            if (office == null)
                throw new PrimaryKeyNotFoundException($"Office with Id {context.Message.OfficeId} not found");
            await context.RespondAsync<OfficeDto>(office).ConfigureAwait(false);
        }
    }
}
