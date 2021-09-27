using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web.Helpers;

namespace Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<HotDogStand> HotDogStands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrdersProducts { get; set; }
        public DbSet<InventoryProduct> InventoryProducts { get; set; }
        public DbSet<HotDogStandProduct> HotDogStandProducts { get; set; }
        public DbSet<ProductRequest> ProductRequests { get; set; }
        public DbSet<ProductsRequest> ProductsRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUserProperties(modelBuilder);
            SetHotDogStandProperties(modelBuilder);
            SetProductProperties(modelBuilder);
            SetOrderProperties(modelBuilder);
            SetOrderProductProperties(modelBuilder);
            SetInventoryProductProperties(modelBuilder);

            SeedUsers(modelBuilder);
            SeedHotDogStandsWithProducts(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SetOrderProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Order>()
                .Property(op => op.OperatorId)
                .HasColumnName("operator_id");
            modelBuilder.Entity<Order>()
                .Property(o => o.UserId)
                .HasColumnName("user_id");
            modelBuilder.Entity<Order>()
                .Property(o => o.Timestamp)
                .HasColumnName("timesptamp");
            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnName("total");
        }

        private static void SetOrderProductProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .Property(o => o.Id)
                .HasColumnName("id");
            modelBuilder.Entity<OrderProduct>()
                .Property(o => o.OrderId)
                .HasColumnName("order_id");
            modelBuilder.Entity<OrderProduct>()
                .Property(p => p.ProductId)
                .HasColumnName("product_id");
            modelBuilder.Entity<OrderProduct>()
                .Property(q => q.Quantity)
                .HasColumnName("quantity");
        }

        private static void SetUserProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("id");
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasColumnName("username");
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email");
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired()
                .HasColumnName("role");
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasColumnName("password");
        }

        private static void SetHotDogStandProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotDogStand>()
                .Property(s => s.Id)
                .HasColumnName("id");
            modelBuilder.Entity<HotDogStand>()
                .Property(s => s.Address)
                .IsRequired()
                .HasColumnName("address");
            modelBuilder.Entity<HotDogStand>()
                .Property(s => s.OperatorId)
                .IsRequired()
                .HasColumnName("operator_id");
        }

        private static void SetProductProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("name");
            modelBuilder.Entity<Product>()
               .Property(p => p.Description)
               .HasMaxLength(500)
               .IsRequired()
               .HasColumnName("description");
            modelBuilder.Entity<Product>()
               .Property(p => p.Category)
               .HasMaxLength(100)
               .IsRequired()
               .HasColumnName("category");
        }

        private static void SetInventoryProductProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryProduct>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<InventoryProduct>()
                .Property(p => p.ProductId)
                .HasColumnName("product_id");
            modelBuilder.Entity<InventoryProduct>()
                .Property(q => q.Quantity)
                .HasColumnName("quantity");
        }

        private static void SeedUsers(ModelBuilder model)
        {
            model.Entity<User>()
                .HasData(
                    new User { Id = Guid.NewGuid(), Username = "customer", Email = "customer@gmail.com", Password = Crypto.SHA256("customer"), Role = Role.CUSTOMER },
                    new User { Id = Guid.NewGuid(), Username = "admin", Email = "admin@gmail.com", Password = Crypto.SHA256("admin"), Role = Role.ADMIN },
                    new User { Id = Guid.NewGuid(), Username = "supplier", Email = "supplier@gmail.com", Password = Crypto.SHA256("supplier"), Role = Role.SUPPLIER }
                );
        }

        private static void SeedHotDogStandsWithProducts(ModelBuilder model)
        {
            User operator1 = new() { Id = Guid.NewGuid(), Username = "operator1", Email = "operator1@gmail.com", Password = Crypto.SHA256("operator1"), Role = Role.OPERATOR };
            User operator2 = new() { Id = Guid.NewGuid(), Username = "operator2", Email = "operator2@gmail.com", Password = Crypto.SHA256("operator2"), Role = Role.OPERATOR };
            User operator3 = new() { Id = Guid.NewGuid(), Username = "operator3", Email = "operator3@gmail.com", Password = Crypto.SHA256("operator3"), Role = Role.OPERATOR };
            User operator4 = new() { Id = Guid.NewGuid(), Username = "operator4", Email = "operator4@gmail.com", Password = Crypto.SHA256("operator4"), Role = Role.OPERATOR };

            HotDogStand stand1 = new() { Id = Guid.NewGuid(), Address = "Grimmer's Road", OperatorId = operator1.Id };
            HotDogStand stand2 = new() { Id = Guid.NewGuid(), Address = "Fieldfare Banks", OperatorId = operator2.Id };
            HotDogStand stand3 = new() { Id = Guid.NewGuid(), Address = "Imperial Passage", OperatorId = operator3.Id };
            HotDogStand stand4 = new() { Id = Guid.NewGuid(), Address = "Woodville Square", OperatorId = operator4.Id };

            Product product1 = new() { Id = Guid.NewGuid(), Name = "Hot Dog", Description = "Basic hot dog with ketchup/mustard", Category = "HotDogs", Price = 10 };
            Product product2 = new() { Id = Guid.NewGuid(), Name = "Hot Onion Dog", Description = "Hot dog with caramelized onions and ketchup", Category = "HotDogs", Price = 12.5F };
            Product product3 = new() { Id = Guid.NewGuid(), Name = "Bacon Melt", Description = "Hot dog with melted gouda cheese and bacon", Category = "HotDogs", Price = 15 };
            Product product4 = new() { Id = Guid.NewGuid(), Name = "Fries", Description = "Regular fries", Category = "Extras", Price = 7.5F };
            Product product5 = new() { Id = Guid.NewGuid(), Name = "Coke", Description = "Coke bottle", Category = "Drinks", Price = 5 };

            //stand1
            HotDogStandProduct standProduct1 = new() { Id = Guid.NewGuid(), StandId = stand1.Id, ProductId = product1.Id, Quantity = 7 };
            HotDogStandProduct standProduct2 = new() { Id = Guid.NewGuid(), StandId = stand1.Id, ProductId = product2.Id, Quantity = 10 };
            HotDogStandProduct standProduct3 = new() { Id = Guid.NewGuid(), StandId = stand1.Id, ProductId = product3.Id, Quantity = 13 };

            //stand2
            HotDogStandProduct standProduct4 = new() { Id = Guid.NewGuid(), StandId = stand2.Id, ProductId = product1.Id, Quantity = 20 };
            HotDogStandProduct standProduct5 = new() { Id = Guid.NewGuid(), StandId = stand2.Id, ProductId = product2.Id, Quantity = 6 };

            model.Entity<User>()
                .HasData(operator1, operator2, operator3, operator4);

            model.Entity<HotDogStand>()
                .HasData(stand1, stand2, stand3, stand4);

            model.Entity<Product>()
                .HasData(product1, product2, product3, product4, product5);

            model.Entity<HotDogStandProduct>()
                .HasData(standProduct1, standProduct2, standProduct3, standProduct4, standProduct5);
        }
    }
}