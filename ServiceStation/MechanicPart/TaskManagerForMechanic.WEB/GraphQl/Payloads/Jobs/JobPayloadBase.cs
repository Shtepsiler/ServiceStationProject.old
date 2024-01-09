using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;
namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.Jobs
{
    public class JobPayloadBase : Payload
    {
        protected JobPayloadBase(Job job)
        {
            Job = job;
        }

        protected JobPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Job? Job { get; }
    }
}
