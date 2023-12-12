
using HotChocolate.Types.Descriptors;
using System.Reflection;
using TaskManagerForMechanic.DAL;

namespace TaskManagerForMechanic.WEB.Extensions
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        protected override void OnConfigure(
           IDescriptorContext context,
           IObjectFieldDescriptor descriptor,
           MemberInfo member)
        {
            descriptor.UseDbContext<TaskManagerDbContext> ();
        }
    }




}
