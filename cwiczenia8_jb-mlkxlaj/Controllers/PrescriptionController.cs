﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie8.DTOs;
using Zadanie8.Models;

namespace Zadanie8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly s24427Context _context;

        public PrescriptionController(s24427Context context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrescritpionRDTO>> GetPrescriptions(int id)
        {
            var prescription = await _context.Prescriptions
                .Where(p => p.IdPrescription == id)
                .Select(p => new
                {
                    Prescription = p,
                    Patient = p.Patient,
                    Doctor = p.Doctor,
                    PrescriptionMedicaments = p.PrescriptionMedicaments.Select(pm => pm.Medicament)
                })
                .FirstOrDefaultAsync();

            if (prescription == null)
            {
                return NotFound();
            }

            var prescriptionDTO = new PrescritpionRDTO(
                prescription.Prescription.IdPrescription,
                prescription.Prescription.Date,
                prescription.Prescription.DueDate,
                new DoctorRDTO(prescription.Doctor.IdDoctor, prescription.Doctor.FirstName, prescription.Doctor.LastName, prescription.Doctor.Email),
                new PatientRDTO(prescription.Patient.IdPatient, prescription.Patient.FirstName, prescription.Patient.LastName, prescription.Patient.BirthDate),
                prescription.PrescriptionMedicaments
                    .Select(pm => new MedicamentRDTO(pm.IdMedicament, pm.Name, pm.Description, pm.Type))
                    .ToList()
            );

            return prescriptionDTO;
        }
    }
}