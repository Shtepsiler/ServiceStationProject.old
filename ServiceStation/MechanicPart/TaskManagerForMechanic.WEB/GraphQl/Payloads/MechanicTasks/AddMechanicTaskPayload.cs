using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;

namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.MechanicTasks
{
    public class AddMechanicTaskPayload : MechanicTasksPayloadBase
    {
        public AddMechanicTaskPayload(UserError error)
              : base(new[] { error })
        {
        }

        public AddMechanicTaskPayload(MechanicsTasks task) : base(task)
        {
        }

        public AddMechanicTaskPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}
