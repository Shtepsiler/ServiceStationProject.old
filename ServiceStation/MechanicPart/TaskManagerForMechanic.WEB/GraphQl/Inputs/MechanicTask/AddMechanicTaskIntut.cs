namespace TaskManagerForMechanic.WEB.GraphQl.Inputs.MechanicTask
{
    public record AddMechanicTaskIntut(int MechanicId,
        int? JobId,
        string Task,
        string Status);
   
}
