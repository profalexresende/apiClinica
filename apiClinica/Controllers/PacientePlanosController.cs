// Controllers/PacientePlanosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiClinica.Data;
using apiClinica.Model;

[Route("api/[controller]")]
[ApiController]
public class PacientePlanosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PacientePlanosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public class AssociarPacientePlanoDto
    {
        public int PacienteId { get; set; }
        public int PlanoSaudeId { get; set; }
    }

    [HttpPost("associar")]
    public async Task<IActionResult> AssociarPacientePlano(AssociarPacientePlanoDto dto)
    {
        // Verificar se o paciente existe
        var paciente = await _context.Pacientes.FindAsync(dto.PacienteId);
        if (paciente == null)
        {
            return NotFound($"Paciente com ID {dto.PacienteId} não encontrado.");
        }

        // Verificar se o plano de saúde existe
        var planoSaude = await _context.PlanosSaude.FindAsync(dto.PlanoSaudeId);
        if (planoSaude == null)
        {
            return NotFound($"Plano de saúde com ID {dto.PlanoSaudeId} não encontrado.");
        }

        // Criar a associação
        var pacientePlano = new PacientePlano
        {
            PacienteId = dto.PacienteId,
            PlanoSaudeId = dto.PlanoSaudeId
        };

        _context.PacientePlanos.Add(pacientePlano);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("remover/{pacienteId}/{planoId}")]
    public async Task<IActionResult> RemoverPacientePlano(int pacienteId, int planoId)
    {
        var pacientePlano = await _context.PacientePlanos
            .FirstOrDefaultAsync(pp => pp.PacienteId == pacienteId && pp.PlanoSaudeId == planoId);

        if (pacientePlano == null)
        {
            return NotFound();
        }

        _context.PacientePlanos.Remove(pacientePlano);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("planos/{pacienteId}")]
    public async Task<ActionResult<IEnumerable<PlanoSaude>>> GetPlanosByPaciente(int pacienteId)
    {
        var planos = await _context.PacientePlanos
            .Where(pp => pp.PacienteId == pacienteId)
            .Select(pp => pp.PlanoSaude)
            .ToListAsync();

        if (planos == null || !planos.Any())
        {
            return NotFound();
        }

        return planos;
    }

    [HttpGet("pacientes/{planoId}")]
    public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesByPlanoId(int planoId)
    {
        var pacientes = await _context.PacientePlanos
            .Where(pp => pp.PlanoSaudeId == planoId)
            .Select(pp => pp.Paciente)
            .ToListAsync();

        if (pacientes == null || !pacientes.Any())
        {
            return NotFound($"Nenhum paciente encontrado para o plano de saúde com ID {planoId}.");
        }

        return pacientes;
    }
}
