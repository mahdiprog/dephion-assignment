using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReservationService.Application.Interfaces;
using ReservationService.Messaging.Offices;
using ReservationService.Messaging.Rooms;
using Triskele.Common.Application.Exceptions;

namespace ReservationService.Application.MessageHandlers.Rooms
{
    public class RoomGetAvailableQueryHandler: IConsumer<RoomGetAvailableQuery>,IExternalConsumer
    {
        private readonly IReservationDbContext _dbContext;

        public RoomGetAvailableQueryHandler(IReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<RoomGetAvailableQuery> context)
        {
            
        }
    }
}
