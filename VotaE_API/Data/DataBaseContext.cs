using Microsoft.EntityFrameworkCore;
using VotaE_API.Models;

namespace VotaE_API.Data
{
    public class DataBaseContext : DbContext
    {
        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<SugestaoModel> Sugestoes { get; set; }
        public virtual DbSet<ProjetoModel> Projetos { get; set; }
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DataBaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("TB_Usuario"); //Define o nome da tabela
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Senha).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });

            modelBuilder.Entity<SugestaoModel>(entity =>
            {
                entity.ToTable("TB_Sugestao"); //Define o nome da tabela
                entity.HasKey(e => e.SugestaoId);
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.Property(e => e.Localizacao).IsRequired();
                entity.Property(e => e.DataCriacao).HasColumnType("date");

                //Relacionado Usuario x Sugestao
                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .IsRequired();
            });

            modelBuilder.Entity<ProjetoModel>(entity =>
            {
                entity.ToTable("TB_Projetos"); // nome da tabela
                entity.HasKey(e => e.ProjetoId);
                entity.Property(e => e.Descricao).IsRequired();
                entity.Property(e => e.Titulo).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                entity.Property(e => e.DataCadastro).IsRequired().HasColumnType("date");
                entity.Property(e => e.DataEnvio).IsRequired().HasColumnType("date");
                entity.Property(e => e.DataAprovacao).IsRequired().HasColumnType("date");

                //Relacionamento Projeto x Sugestão
                entity.HasOne(e => e.Sugestao)
                      .WithMany()
                      .HasForeignKey(e => e.SugestaoId)
                      .IsRequired();

            });

            base.OnModelCreating(modelBuilder);


        }
    }
}
