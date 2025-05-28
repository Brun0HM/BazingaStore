using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BazingaStore.Model;
using BazingaStore.Data;

namespace BazingaStore.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produto { get; set; } = default!;
        public DbSet<Categoria> Categoria { get; set; } = default!;
        public DbSet<CupomDesconto> CupomDesconto
        {
            get; set;
        }
        public DbSet<ItemVenda> ItemVenda { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Venda> Venda { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
        }
        public DbSet<BazingaStore.Model.Carrinho> Carrinho { get; set; } = default!;
        public DbSet<BazingaStore.Model.CarrinhoItem> CarrinhoItem { get; set; } = default!;
        public DbSet<BazingaStore.Model.Avaliacao> Avaliacao { get; set; } = default!;
        public DbSet<BazingaStore.Model.Pagamento> Pagamento { get; set; } = default!;
    }
}
