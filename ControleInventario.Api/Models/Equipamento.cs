namespace ControleInventario.Api.Models
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string NumeroPatrimonio { get; set; } = string.Empty;
        public bool EstaEmprestado { get; set; } = false;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
