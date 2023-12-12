using TaskManagerForMechanic.WEB.GraphQl.Common;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;
using TaskManagerForMechanic.DAL.Entitys;
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
