
using DesafioBack.Data.Repositories;

namespace DesafioBack.Services.Shared
{
    abstract public class ServiceAbstract
    {
        public readonly Repository Repository = new Repository();
    }
}