using System.Threading.Tasks;

namespace Funkmap.Cqrs.Abstract
{
    public interface IQueryContext
    {
        Task<TResponse> ExecuteAsync<TQuery, TResponse>(TQuery query) where TQuery : class where TResponse : class;
    }
}
