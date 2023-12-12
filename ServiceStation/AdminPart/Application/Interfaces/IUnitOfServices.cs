using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfServices
    {

        public IClientsService _clientsService { get; }
        public IJobService _jobService { get; }
        public IManagerService _managerService { get; }
        public IMechanicService _mechanicService { get; }
        public IMechanicsTasksService _mechanicsTasksService { get; }
        public IModelService _modelService { get; }
        public IOrderPartsService _orderPartsService { get; }
        public IOrderService _orderService { get; }
        public IPartNeededService _partNeededService { get; }
        public IPartService _partService { get; }
        public IVendorsService _vendorsService { get; }

    }
}
