using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;

namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.Mechanics
{
    public class LoginPayload : MechanicPayloadBase
    {
        public LoginPayload(UserError error)
   : base(new[] { error })
        {
        }

        public LoginPayload(Mechanic mechanic) : base(mechanic)
        {
        }

        public LoginPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}
