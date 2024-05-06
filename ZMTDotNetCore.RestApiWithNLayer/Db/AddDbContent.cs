using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMTDotNetCore.RestApiWithNLayer;
using ZMTDotNetCore.RestApiWithNLayer.Model;

namespace ZMTDotNetCore.RestApiWithNLayer.Db
{
    internal class AddDbContent : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.connectionStrings.ConnectionString);
        }
        public DbSet<BlogModel> Blog { get; set; }
    }
}
