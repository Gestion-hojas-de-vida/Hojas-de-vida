using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHV.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options){

        }
    }
}