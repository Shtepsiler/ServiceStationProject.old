using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;

namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.MechanicTasks
{
    public class MechanicTasksPayloadBase : Payload
    {
        protected MechanicTasksPayloadBase(MechanicsTasks mechanicsTasks)

        {
            MechanicsTasks = mechanicsTasks;
        }

        protected MechanicTasksPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public MechanicsTasks? MechanicsTasks { get; }
    }
}
