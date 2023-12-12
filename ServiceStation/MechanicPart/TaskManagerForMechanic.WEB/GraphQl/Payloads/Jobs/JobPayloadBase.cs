using TaskManagerForMechanic.WEB.GraphQl.Common;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;
using TaskManagerForMechanic.DAL.Entitys;
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
