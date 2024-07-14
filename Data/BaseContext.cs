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
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<ModelosDePermiso> ModelosDePermisos { get; set; }
        public DbSet<RolesDePermiso> RolesDePermisos { get; set; }
        public DbSet<RolesDeModelo> RolesDeModelos { get; set; }

        /*aqui se configuran los modelos y con "HasNoKey" quiere decir que las entidad no tiene llave peimaria*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelosDePermiso>().HasNoKey();
            modelBuilder.Entity<RolesDeModelo>().HasNoKey();
            modelBuilder.Entity<RolesDePermiso>().HasNoKey();
            // Otras configuraciones de modelos aquí
        }


    }

   
  
}