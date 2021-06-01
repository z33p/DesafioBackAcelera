
using DesafioBack.Data.Repositories.shared;

namespace DesafioBack.Services.Shared
{
    abstract public class ServiceAbstract
    {
        public readonly IRepository repository;

        public ServiceAbstract(IRepository repository)
        {
            this.repository = repository;
        }
    }
}