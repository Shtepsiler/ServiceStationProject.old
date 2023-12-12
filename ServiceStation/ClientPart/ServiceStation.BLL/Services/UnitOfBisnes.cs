using ServiceStation.BLL.Services.Interfaces;

namespace ServiceStation.BLL.Services
{
    public class UnitOfBisnes : IUnitOfBisnes
    {
        public  IClientService _ClientService { get; }
        public IIdentityService _IdentityService { get; }
        public IJobService _JobService { get; }
        public IModelService _ModelService { get; }
        public IManagerService _ManagerService { get; }
        public ITokenService _TokenService { get; }
        public IMechanicService _MechanicService { get; }
                

        public UnitOfBisnes(
            IClientService clientService,
            IIdentityService identityService,
            IJobService jobService, 
            IModelService modelService, 
            IManagerService managerService,
            ITokenService tokenService,
            IMechanicService mechanicService
            
            )
        {
            _ClientService = clientService;
            _IdentityService = identityService;
            _JobService = jobService;
            _ModelService = modelService;
            _ManagerService = managerService;
            _TokenService = tokenService;
            _MechanicService = mechanicService;
        }
    }
}
