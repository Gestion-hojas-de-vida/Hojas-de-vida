using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHV.Models;
using Microsoft.EntityFrameworkCore;

namespace GHV.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options){

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<PermisoDeModelo> PermisosDeModelos { get; set; }
        public DbSet<PermisoDeRol> PermisosDeRoles { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolDeModelo> RolesDeModelos { get; set; }
        public DbSet<InformacionPersonal> InformacionesPersonales { get; set; }

        /*aqui se configuran los modelos y con "HasNoKey" quiere decir que las entidad no tiene llave peimaria*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermisoDeModelo>().HasNoKey();
            modelBuilder.Entity<RolDeModelo>()
            .HasKey(r => new { r.RolId, r.ModeloId });
            modelBuilder.Entity<PermisoDeRol>().HasNoKey();
            // Otras configuraciones de modelos aquí
        }

    }
}