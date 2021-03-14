namespace Meetings.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        int OfficeId { get; }
        bool IsAuthenticated { get; }
    }
}
