namespace TaskManagerForMechanic.WEB.GraphQl.Inputs.MechanicTask
{
    public record ChangeTaskInput(
        int id,
        int MechanicId,
        int? JobId,
        string Task,
        string Status);
    
}
