using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ZMTDotNetCore.PizzaApi;

namespace ZMTDotNetCore.PizzaApi.Db
{
    internal class AddDbContent : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.connectionStrings.ConnectionString);
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaExtra> PizzaExtras { get; set; }
        public DbSet<PizzaOrder> PizzaOrders { get; set; }
        public DbSet<PizzaOrderDetail> PizzaOrderDetails { get; set; }
        [Table("Tbl_Pizza")]
        public class Pizza
        {
            [Key]
            [Column("PizzaId")]
            public int Id { get; set; }
            [Column("PizzaName")]
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
        [Table("Tbl_PizzaExtra")]
        public class PizzaExtra
        {
            [Key]
            [Column("PizzaExtraId")]
            public int Id { get; set; }
            [Column("PizzaExtraName")]
            public string Name { get; set; }

            public decimal Price { get; set; }
        }
        [Table("Tbl_PizzaOrder")]
        public class PizzaOrder
        {
            [Key]
            [Column("OrderId")]
            public int Id { get; set; }
            public int PizzaId { get; set; }
            public string InvoiceNo { get; set; }
            public decimal TotalPrice { get; set; }
            
        }
        [Table("Tbl_PizzaOrderDetail")]
        public class PizzaOrderDetail
        {
            [Key]
            [Column("OrderDetailId")]
            public int Id { get; set; }
            public int PizzaExtraId { get; set; }
            public string InvoiceNo { get; set; } 

        }
      
    }
}
