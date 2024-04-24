using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMTDotNetCore.ConsoleApp.Dtos;
using ZMTDotNetCore.ConsoleApp.Services;

namespace ZMTDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class AddDbContent : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.connectionStrings.ConnectionString);
        }
        public DbSet<BlogDto> Blog { get; set; }
    }
}
