using DEPI.Core.Models;
using IntialRepo.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntialRepo.EF
{
    public class MyContext: IdentityDbContext<ApplicationUser>
    {


        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        #region MyRegion

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Catigory> Catigorys { get; set; }
        public virtual DbSet<Vartion> Vartions { get; set; }
        public virtual DbSet<ProductVartion> ProductVartions { get; set; }
        public virtual DbSet<Guset> Gusets { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        #endregion



        #region MyRegion
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Has a problem
            //builder.Entity<Vartion>()
            //    .HasMany(V => V.ProductVartions)
            //    .WithOne(PV => PV.Vartion)
            //    .OnDelete(DeleteBehavior.Cascade);



            builder.Entity<ProductVartion>()
                .HasMany(V => V.OrderItems)
                .WithOne(OI => OI.ProductVartion)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Product>()
                .HasMany(V => V.ProductImages)
                .WithOne(OI => OI.Product)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductVartion>()
             .HasMany(V => V.CartItems)
            .WithOne(CI => CI.ProductVartion)
            .OnDelete(DeleteBehavior.SetNull);
            base.OnModelCreating(builder);

        }
        #endregion
    }
}
