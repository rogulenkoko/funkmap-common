using System.Threading.Tasks;

namespace Funkmap.Cqrs.Abstract
{
    public interface ICommandBus
    {
        Task ExecuteEnvelope<TCommand>(Envelope<TCommand> command) where TCommand : class;
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : class;
    }
}
