using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ReservationService.Messaging.Meetings
{
    public class MeetingAddCommand
    {
        public int RoomId { get; set; }
        public int? MovableResourceId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class MeetingAddCommandValidator : AbstractValidator<MeetingAddCommand>
    {
        public MeetingAddCommandValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty().WithMessage("Room should be selected");
            RuleFor(x => x.From).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.To).GreaterThanOrEqualTo(DateTime.Now);

        }
    }
}
