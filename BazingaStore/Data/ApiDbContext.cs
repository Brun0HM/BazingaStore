using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BazingaStore.Model;

namespace BazingaStore.Data
{
    public class ApiDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public DbSet<Produto> Produto { get; set; } = default!;
        public DbSet<Categoria> Categoria { get; set; } = default!;
        public DbSet<CupomDesconto> CupomDesconto { get; set; }
        public DbSet<ItemVenda> ItemVenda { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<BazingaStore.Model.Carrinho> Carrinho { get; set; } = default!;
        public DbSet<BazingaStore.Model.CarrinhoItem> CarrinhoItem { get; set; } = default!;
        public DbSet<BazingaStore.Model.Avaliacao> Avaliacao { get; set; } = default!;
        public DbSet<BazingaStore.Model.Pagamento> Pagamento { get; set; } = default!;




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura os nomes das tabelas
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");

            // Garante que o ProdutoId e CategoriaId sejam gerados automaticamente com NEWID()
            modelBuilder.Entity<Produto>()
                .Property(p => p.ProdutoId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Categoria>()
                .Property(c => c.CategoriaId)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
