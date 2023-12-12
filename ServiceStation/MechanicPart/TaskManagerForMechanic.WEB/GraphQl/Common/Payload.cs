using System.Collections.Generic;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;

namespace TaskManagerForMechanic.WEB.GraphQl.Common
{
    public class Payload
    {
        protected Payload(IReadOnlyList<UserError>? errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<UserError>? Errors { get; }
    }
}
