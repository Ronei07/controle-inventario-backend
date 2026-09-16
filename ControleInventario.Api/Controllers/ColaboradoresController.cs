using ControleInventario.Api.Data;
using ControleInventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleInventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColaboradoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public ColaboradoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Colaborador>>> ObterTodos()
    {
        return Ok(await _context.Colaboradores.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Colaborador>> Criar([FromBody] Colaborador colaborador)
    {
        _context.Colaboradores.Add(colaborador);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObterTodos), new { id = colaborador.Id }, colaborador);
    }
}