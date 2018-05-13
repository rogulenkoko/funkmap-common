using System.Threading.Tasks;

namespace Funkmap.Cqrs.Abstract
{
    public interface IEventHandler
    {
        void InitHandlers();
    }

    public interface IEventHandler<in T> : IEventHandler where T : class
    {
        Task Handle(T @event);
    }
}
