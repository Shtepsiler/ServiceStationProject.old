using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;

namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.MechanicTasks
{
    public class ChangeMechanicsTaskPayload : MechanicTasksPayloadBase
    {
        public ChangeMechanicsTaskPayload(UserError error)
              : base(new[] { error })
        {
        }

        public ChangeMechanicsTaskPayload(MechanicsTasks task) : base(task)
        {
        }

        public ChangeMechanicsTaskPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}
