﻿using Microsoft.EntityFrameworkCore;
using System;
using ProEventos.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ProEventos.Persistence
{
    public class ProEventosContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                        IdentityUserToken<int>>
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {

        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            modelBuilder.Entity<Evento>().HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>().HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

        }
    }
}
