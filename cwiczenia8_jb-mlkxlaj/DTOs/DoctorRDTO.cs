namespace Zadanie8.DTOs;

public class DoctorRDTO
{
    public DoctorRDTO(int idDoctor, string firstName, string lastName, string email)
    {
        IdDoctor = idDoctor;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public int IdDoctor { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Email { get; set; }
}