namespace BazingaStore.Model
{
    public class CupomDesconto
    {
        public Guid CupomDescontoId { get; set; }
        public string Codigo { get; set; }
        public decimal PercentualDesconto { get; set; }
        public DateTime DataValidade { get; set; }
    }
}
