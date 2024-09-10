// Controllers/PlanosSaudeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiClinica.Data;
using apiClinica.Model;

[Route("api/[controller]")]
[ApiController]
public class PlanosSaudeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PlanosSaudeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanoSaude>>> GetPlanosSaude()
    {
        return await _context.PlanosSaude.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlanoSaude>> GetPlanoSaude(int id)
    {
        var planoSaude = await _context.PlanosSaude.FindAsync(id);

        if (planoSaude == null)
        {
            return NotFound();
        }

        return planoSaude;
    }

    [HttpPost]
    public async Task<ActionResult<PlanoSaude>> PostPlanoSaude(PlanoSaude planoSaude)
    {
        _context.PlanosSaude.Add(planoSaude);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPlanoSaude", new { id = planoSaude.Id }, planoSaude);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlanoSaude(int id, PlanoSaude planoSaude)
    {
        if (id != planoSaude.Id)
        {
            return BadRequest();
        }

        _context.Entry(planoSaude).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanoSaude(int id)
    {
        var planoSaude = await _context.PlanosSaude.FindAsync(id);
        if (planoSaude == null)
        {
            return NotFound();
        }

        _context.PlanosSaude.Remove(planoSaude);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
