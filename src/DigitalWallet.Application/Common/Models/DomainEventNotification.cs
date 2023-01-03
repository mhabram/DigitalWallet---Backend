using MediatR;
using DigitalWallet.Domain.Common;

namespace DigitalWallet.Application.Common.Models;
public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent: DomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}