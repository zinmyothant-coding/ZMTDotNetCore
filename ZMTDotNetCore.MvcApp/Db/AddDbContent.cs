using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMTDotNetCore.MvcApp.Models;

namespace ZMTDotNetCore.MvcApp.Db
{
    public class AddDbContent : DbContext
    {
        public AddDbContent(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blog { get; set; }
    }
}
