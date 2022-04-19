namespace Vega.API.Core
{
    public interface IUnitOfWork
    {
        Task CompeleteAsync();
    }
}