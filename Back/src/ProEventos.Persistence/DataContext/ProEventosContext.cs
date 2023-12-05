using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;
using System;
using System.Linq;

namespace ProEventos.Persistence.DataContext
{
    public class ProEventosContext : IdentityDbContext<User, Role, Int32, 
                                                       IdentityUserClaim<Int32>, UserRole, 
                                                       IdentityUserLogin<Int32>, IdentityRoleClaim<Int32>, IdentityUserToken<Int32>>
    {
        //Enviando minha conexão SQL para o constructor do DbContext
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {
            
        }
        //Objetos para criação de dados no SQL
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
            base.OnModelCreating(modelBuilder);

            ////alteração
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            //Tive que utilizar fora do curso... por conta de usar o SQL Server
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                
                userRole.HasOne(u => u.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new{PE.EventoId, PE.PalestranteId});
            
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);                      
        }

    }
}