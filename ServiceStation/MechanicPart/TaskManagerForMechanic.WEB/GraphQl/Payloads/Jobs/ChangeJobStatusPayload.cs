using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.Common.Exceptions;

namespace TaskManagerForMechanic.WEB.GraphQl.Payloads.Jobs
{
    public class ChangeJobStatusPayload: JobPayloadBase
    {
        public ChangeJobStatusPayload(UserError error)
    : base(new[] { error })
        {
        }

        public ChangeJobStatusPayload(Job job) : base(job)
        {
        }

        public ChangeJobStatusPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}
