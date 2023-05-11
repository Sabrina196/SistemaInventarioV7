﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaInventarioV7.Modelos;
using System.Reflection;

namespace SistemaInventarioV7.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        public DbSet<BodegaProducto> BodegasProductos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<InventarioDetalle> InventariosDetalles { get; set; }
        public DbSet<KardexInventario> KardexInventarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}