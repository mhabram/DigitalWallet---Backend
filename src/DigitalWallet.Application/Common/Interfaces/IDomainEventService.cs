using DigitalWallet.Domain.Common;

namespace DigitalWallet.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}