namespace ThriveActiveWellness.Modules.Exercises.Domain.Clients;

public interface IClientRepository
{
    Task<Client?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Client attendee);
}
