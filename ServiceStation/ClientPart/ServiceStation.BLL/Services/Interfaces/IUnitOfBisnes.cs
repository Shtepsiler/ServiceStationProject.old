

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IUnitOfBisnes
    {
        IClientService _ClientService { get; }
        IIdentityService _IdentityService { get; }
        IJobService _JobService { get; }
        IModelService _ModelService { get; }
        IManagerService _ManagerService { get; }
        ITokenService _TokenService { get; }
        IMechanicService _MechanicService { get; }  
    }
}
