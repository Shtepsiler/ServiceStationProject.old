using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        
        UserManager<Client> _ClientManager { get; }
        SignInManager<Client> _SignInManager { get; }
        IJobRepository _JobRepository { get; }
        IManagerRepository _ManagerRepository { get; }
        IModelRepository _ModelRepository { get; }
        ITokenRepository _TokenRepository { get; }
        IMechanicRepository _MechanicRepository { get; }
        Task SaveChangesAsync();
    }
}
