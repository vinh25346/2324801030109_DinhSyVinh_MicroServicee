using System;
using MediatR;

namespace PolicyService.Api.Events;

public sealed class PolicyTerminated : INotification
{
    public string PolicyNumber { get; init; } = string.Empty;
    public DateTime PolicyTo { get; init; }
    public decimal AmountToReturn { get; init; }
}
