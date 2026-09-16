using ControleInventario.Api.Data;
using ControleInventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ControleInventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    // Injetamos o DbContext aqui para ter acesso ao banco
    public EquipamentosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/equipamentos
    // Busca todos os registros do banco e devolve como JSON
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipamento>>> ObterTodos()
    {
        var equipamentos = await _context.Equipamentos.ToListAsync();
        return Ok(equipamentos);
    }

    // GET: api/equipamentos/5
    // Busca um único equipamento pelo ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipamento>> ObterPorId(int id)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);

        if (equipamento == null)
        {
            return NotFound("Equipamento não encontrado.");
        }

        return Ok(equipamento);
    }

    // POST: api/equipamentos
    // Recebe um JSON no corpo da requisição e grava no banco
    [HttpPost]
    public async Task<ActionResult<Equipamento>> Criar([FromBody] Equipamento novoEquipamento)
    {
        _context.Equipamentos.Add(novoEquipamento);
        await _context.SaveChangesAsync();

        // Retorna status 201 (Created) e aponta a rota onde o item pode ser consultado
        return CreatedAtAction(nameof(ObterPorId), new { id = novoEquipamento.Id }, novoEquipamento);
    }

    // PUT: api/equipamentos/5
    // Recebe o ID na URL e os novos dados no corpo (JSON) para atualizar
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Equipamento equipamentoAtualizado)
    {
        if (id != equipamentoAtualizado.Id)
        {
            return BadRequest("O ID da URL não coincide com o ID do corpo da requisição.");
        }

        var equipamentoExistente = await _context.Equipamentos.FindAsync(id);

        if (equipamentoExistente == null)
        {
            return NotFound("Equipamento não encontrado para atualização.");
        }

        // Atualiza os campos necessários
        equipamentoExistente.Nome = equipamentoAtualizado.Nome;
        equipamentoExistente.Categoria = equipamentoAtualizado.Categoria;
        equipamentoExistente.NumeroPatrimonio = equipamentoAtualizado.NumeroPatrimonio;
        equipamentoExistente.EstaEmprestado = equipamentoAtualizado.EstaEmprestado;

        await _context.SaveChangesAsync();

        // 204 No Content indica que a alteração foi feita com sucesso e não há nada a retornar no corpo
        return NoContent();
    }

    // DELETE: api/equipamentos/5
    // Remove o registro do banco a partir do ID fornecido
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);

        if (equipamento == null)
        {
            return NotFound("Equipamento não encontrado para exclusão.");
        }

        _context.Equipamentos.Remove(equipamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}