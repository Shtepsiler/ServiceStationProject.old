using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;

namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.Mechanics
{
    public class ChangeMechanicPayload: MechanicPayloadBase
    {
        public ChangeMechanicPayload(UserError error)
    : base(new[] { error })
        {
        }

        public ChangeMechanicPayload(Mechanic mechanic) : base(mechanic)
        {
        }

        public ChangeMechanicPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}
