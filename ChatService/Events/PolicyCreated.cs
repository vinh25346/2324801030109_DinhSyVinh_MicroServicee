using MediatR;

namespace PolicyService.Api.Events;

public sealed class PolicyCreated : INotification
{
    public string AgentLogin { get; init; } = string.Empty;
    public string ProductCode { get; init; } = string.Empty;
}
