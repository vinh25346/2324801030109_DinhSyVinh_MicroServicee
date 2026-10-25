using System;
using MediatR;

namespace PolicyService.Api.Events;

public sealed class PolicyCreated : INotification
{
    public string AgentLogin { get; init; } = string.Empty;
    public string ProductCode { get; init; } = string.Empty;
    public string PolicyNumber { get; init; } = string.Empty;
    public DateTime PolicyFrom { get; init; }
    public DateTime PolicyTo { get; init; }
    public decimal TotalPremium { get; init; }
    public PolicyHolder PolicyHolder { get; init; } = new();
}

public sealed class PolicyHolder
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
