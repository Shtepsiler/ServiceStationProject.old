namespace TaskManagerForMechanic.WEB.GraphQl.Inputs.Mechanic
{
    public record ChangeMechanicInput(
        int id,
        string FirstName, 
        string LastName, 
        string Address, 
        string Phone, 
        string Password,
        string Specialization
        );
}
