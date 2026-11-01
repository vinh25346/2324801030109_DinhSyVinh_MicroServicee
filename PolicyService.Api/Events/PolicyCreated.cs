using MediatR;

namespace PolicyService.Api.Events;

public sealed class PolicyCreated : INotification
{
    public string PolicyNumber { get; init; } = string.Empty;
    public PolicyHolder PolicyHolder { get; init; } = new();
}

public sealed class PolicyHolder
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
