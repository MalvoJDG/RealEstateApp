using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<TipoPropiedad> TiposPropiedad { get; set; }
        public DbSet<TipoVenta> TiposVenta { get; set; }
        public DbSet<Mejora> Mejoras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API

            #region Tablas
            modelBuilder.Entity<Propiedad>().ToTable("Propiedades");
            modelBuilder.Entity<TipoPropiedad>().ToTable("TiposPropiedad");
            modelBuilder.Entity<TipoVenta>().ToTable("TiposVenta");
            modelBuilder.Entity<Mejora>().ToTable("Mejoras");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Propiedad>().HasKey(p => p.Id);
            modelBuilder.Entity<TipoPropiedad>().HasKey(tp => tp.Id);
            modelBuilder.Entity<TipoVenta>().HasKey(tv => tv.Id);
            modelBuilder.Entity<Mejora>().HasKey(m => m.Id);
            #endregion

            #region Property Configurations

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Tipo)
                .IsRequired();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Codigo)
                .IsRequired();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.TipoVenta)
                .IsRequired();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Valor)
                .IsRequired();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.CantidadHabitaciones)
                .IsRequired();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.CantidadBaños)
                .IsRequired();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Tamaño)
                .IsRequired();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Descripcion)
                .IsRequired(false);

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Mejoras)
                .IsRequired(false);

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Imagenes)
                .IsRequired(false);

            modelBuilder.Entity<TipoPropiedad>()
                .Property(tp => tp.Nombre)
                .IsRequired();

            modelBuilder.Entity<TipoPropiedad>()
                .Property(tp => tp.Descripcion)
                .IsRequired(false);

            modelBuilder.Entity<TipoVenta>()
                .Property(tv => tv.Nombre)
                .IsRequired();

            modelBuilder.Entity<TipoVenta>()
                .Property(tv => tv.Descripcion)
                .IsRequired(false);

            modelBuilder.Entity<Mejora>()
                .Property(m => m.Nombre)
                .IsRequired();

            modelBuilder.Entity<Mejora>()
                .Property(m => m.Descripcion)
                .IsRequired(false);

            #endregion
        }
    }
}