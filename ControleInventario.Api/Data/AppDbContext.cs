using ControleInventario.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleInventario.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Equipamento> Equipamentos => Set<Equipamento>();
    public DbSet<Colaborador> Colaboradores => Set<Colaborador>();
    public DbSet<Emprestimo> Emprestimos => Set<Emprestimo>();
}

// Contexto exclusivo para SQLite
public class SqliteDbContext : AppDbContext
{
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }
}

// Contexto exclusivo para PostgreSQL
public class PostgresDbContext : AppDbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }
}