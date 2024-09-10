// Controllers/PacientesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiClinica.Data;
using apiClinica.Model;

[Route("api/[controller]")]
[ApiController]
public class PacientesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PacientesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
    {
        return await _context.Pacientes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Paciente>> GetPaciente(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);

        if (paciente == null)
        {
            return NotFound();
        }

        return paciente;
    }

    [HttpPost]
    public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPaciente", new { id = paciente.Id }, paciente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
    {
        if (id != paciente.Id)
        {
            return BadRequest();
        }

        _context.Entry(paciente).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaciente(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
        {
            return NotFound();
        }

        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
