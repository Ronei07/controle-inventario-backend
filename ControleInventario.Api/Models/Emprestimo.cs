namespace ControleInventario.Api.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public int EquipamentoId { get; set; }
        public Equipamento? Equipamento { get; set; }

        public int ColaboradorId { get; set; }
        public Colaborador? Colaborador { get; set; }

        public DateTime DataEmprestimo { get; set; } = DateTime.UtcNow;
        public DateTime? DataDevolucao { get; set; }
    }
}
