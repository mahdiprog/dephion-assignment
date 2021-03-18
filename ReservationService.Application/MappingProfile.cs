using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ReservationService.Domain.Models;
using ReservationService.Messaging.Offices;
using ReservationService.Messaging.Resources;
using ReservationService.Messaging.Rooms;

namespace ReservationService.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Office, OfficeDto>().MaxDepth(0);
            CreateMap<Room, RoomDto>();
            CreateMap<SteadyResource, ResourceDto>().MaxDepth(0);
            CreateMap<MovableResource, ResourceDto>().MaxDepth(0);
        }
    }
}
