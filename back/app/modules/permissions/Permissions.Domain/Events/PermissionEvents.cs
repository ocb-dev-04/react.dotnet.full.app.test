using MediatR;

namespace Permissions.Domain.Events;

public sealed record PermissionCreatedOrModifiedDomainEvent(int Id) : INotification;