using MediatR;

namespace TokanPages.Backend.Application.NotificationsMobile.Query;

public class GetInstallationQuery : IRequest<GetInstallationQueryResult>
{
    public Guid Id { get; set; }
}