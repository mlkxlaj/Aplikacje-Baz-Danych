namespace Zadanie9.DTOs;

public class PatientRDTO
{
    public PatientRDTO(int idPatient, string firstName, string lastName, DateTime birthdate)
    {
        IdPatient = idPatient;
        FirstName = firstName;
        LastName = lastName;
        Birthdate = birthdate;
    }

    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public DateTime Birthdate { get; set; }
}