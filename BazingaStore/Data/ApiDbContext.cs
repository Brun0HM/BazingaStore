using BazingaStore.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BazingaStore.Data
{
    public class ApiDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Produto> Produto { get; set; } = default!;
        public DbSet<Categoria> Categoria { get; set; } = default!;
        public DbSet<CupomDesconto> CupomDesconto { get; set; }
        public DbSet<ItemVenda> ItemVenda { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItem { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura os nomes das tabelas
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<CupomDesconto>().ToTable("CuponsDesconto");
            modelBuilder.Entity<ItemVenda>().ToTable("ItensVenda");
            modelBuilder.Entity<Venda>().ToTable("Vendas");
            modelBuilder.Entity<Carrinho>().ToTable("Carrinhos");
            modelBuilder.Entity<CarrinhoItem>().ToTable("CarrinhosItens");
            modelBuilder.Entity<Avaliacao>().ToTable("Avaliacoes");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamentos");


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
