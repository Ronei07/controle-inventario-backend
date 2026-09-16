using ControleInventario.Api.Data;
using ControleInventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleInventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmprestimosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmprestimosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Emprestimo>>> ObterTodos()
    {
        return Ok(await _context.Emprestimos
            .Include(e => e.Equipamento)
            .Include(e => e.Colaborador)
            .ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> RealizarEmprestimo([FromBody] EmprestimoCriacaoDto dto)
    {
        var equipamento = await _context.Equipamentos.FindAsync(dto.EquipamentoId);
        if (equipamento == null)
            return NotFound("Equipamento não encontrado.");

        if (equipamento.EstaEmprestado)
            return BadRequest("Este equipamento já está emprestado.");

        var colaborador = await _context.Colaboradores.FindAsync(dto.ColaboradorId);
        if (colaborador == null)
            return NotFound("Colaborador não encontrado.");

        equipamento.EstaEmprestado = true;

        var emprestimo = new Emprestimo
        {
            EquipamentoId = dto.EquipamentoId,
            ColaboradorId = dto.ColaboradorId,
            DataEmprestimo = DateTime.UtcNow
        };

        _context.Emprestimos.Add(emprestimo);
        await _context.SaveChangesAsync();

        return Ok("Empréstimo realizado com sucesso!");
    }
}