using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Zadanie9.DTOs;
using Zadanie9.Models;

namespace Zadanie9.Controllers;

[Route("api/[controller]")]
[ApiController]

public class DoctorsController : ControllerBase
{
    private readonly s24427Context _context;

    public DoctorsController(s24427Context context)
    {
        _context = context;
    }
    
    [HttpGet,Authorize]
    public ActionResult<IEnumerable<Doctor>> GetDoctors()
    {
        var doctors = _context.Doctors
            .Select(d => new DoctorRDTO(d.IdDoctor,d.FirstName,d.LastName,d.Email))
            .ToList();
        return Ok(doctors);
    }

    [HttpPost,Authorize]
    public async Task<ActionResult> CreateDoctor(DoctorCDTO doctorCDto)
    {
        var doctor = new Doctor
        {   
            FirstName = doctorCDto.FirstName,
            LastName = doctorCDto.LastName,
            Email = doctorCDto.Email
        };
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDoctors), new { idDoctor = doctor.IdDoctor }, doctor);
    }

    [HttpPut("{idDoctor:int}"),Authorize]
    public async Task<ActionResult> UpdateDoctor(int idDoctor, DoctorUDTO doctorUDto)
    {
        var doctorToUpdate = await _context.Doctors.FindAsync(idDoctor);
        if (doctorToUpdate == null)
        {
            return NotFound("No doctor with given id exists");
        }
        else
        {
            doctorToUpdate.FirstName = doctorUDto.FirstName;
            doctorToUpdate.LastName = doctorUDto.LastName;
            doctorToUpdate.Email = doctorUDto.Email;

            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }

    [HttpDelete("{idDoctor:int}")]
    public async Task<ActionResult> DeleteDoctor(int idDoctor)
    {
        var doctor = await _context.Doctors.FindAsync(idDoctor);
        if (doctor == null)
        {
            return NotFound("No doctor with given id exists");
        }
        else
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}