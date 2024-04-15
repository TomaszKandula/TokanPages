using MediatR;

namespace TokanPages.Backend.Application.Notifications.Mobile.Query;

public class GetInstallationQuery : IRequest<GetInstallationQueryResult>
{
    public Guid Id { get; set; }
}