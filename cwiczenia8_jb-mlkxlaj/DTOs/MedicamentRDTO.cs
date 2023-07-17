namespace Zadanie8.DTOs;

public class MedicamentRDTO
{
    public MedicamentRDTO(int idMedicament, string name, string description, string type)
    {
        IdMedicament = idMedicament;
        Name = name;
        Description = description;
        Type = type;
    }

    public int IdMedicament { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Type { get; set; }
}