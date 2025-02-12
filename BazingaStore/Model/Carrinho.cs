namespace BazingaStore.Model
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal Total { get; set; } = 0;
        public bool Finalizado { get; set; } = false;


    }
}
