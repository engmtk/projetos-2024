using Microsoft.EntityFrameworkCore;

namespace LojaOnline.Models
{
    public class CadastroLojaContext : DbContext
    {
        public CadastroLojaContext(DbContextOptions<CadastroLojaContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => new { u.CpfCnpj, u.CodigoProduto });  // Chave composta
        }
    }
}
