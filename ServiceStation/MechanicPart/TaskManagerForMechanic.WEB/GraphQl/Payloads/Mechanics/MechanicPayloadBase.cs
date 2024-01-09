using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;
namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.Mechanics
{
    public class MechanicPayloadBase : Payload
    {
        protected MechanicPayloadBase(Mechanic mechanic)
        {
            Mechanic = mechanic;
        }

        protected MechanicPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Mechanic? Mechanic { get; }
    }
}
