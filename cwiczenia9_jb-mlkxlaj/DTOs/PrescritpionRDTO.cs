namespace Zadanie9.DTOs;

public class PrescritpionRDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public DoctorRDTO Doctor { get; set; }
    public PatientRDTO Patient { get; set; }
    
    public ICollection<MedicamentRDTO> Medicaments { get; set; }

    public PrescritpionRDTO(int idPrescription, DateTime date, DateTime dueDate, DoctorRDTO doctor, PatientRDTO patient, ICollection<MedicamentRDTO> medicaments)
    {
        IdPrescription = idPrescription;
        Date = date;
        DueDate = dueDate;
        Doctor = doctor;
        Patient = patient;
        Medicaments = medicaments;
    }
}