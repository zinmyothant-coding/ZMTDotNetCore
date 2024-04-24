using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMTDotNetCore.ConsoleApp
{
    internal class AddDbContent:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.connectionStrings.ConnectionString);
        }
        public DbSet<BlogDto> Blog { get; set; }
    }
}
