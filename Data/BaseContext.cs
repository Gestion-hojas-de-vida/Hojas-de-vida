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
    }
}