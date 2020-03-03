using Domain.Contracts.Infra.Repositories;
using Domain.Entities;

namespace Domain.Contracts.Infra
{
    public interface IUnitOfWork
    {
        IRepository<Profile> ProfileRepository { get; }

        IRepository<Event> EventRepository { get; }

        IRepository<Profile_Event> Profile_EventRepository { get; }

        void Commit();

    }
}
