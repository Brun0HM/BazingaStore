using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BazingaStore.Model;

namespace BazingaStore.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<BazingaStore.Model.Produto> Produto { get; set; } = default!;
        public DbSet<BazingaStore.Model.Categoria> Categoria { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
        }
        public DbSet<BazingaStore.Model.Carrinho> Carrinho { get; set; } = default!;
        //public DbSet<BazingaStore.Model.ItemCarrinho> ItemCarrinho { get; set; } = default!;
    }
}
