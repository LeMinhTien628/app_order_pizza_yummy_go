using api_app_pizza_flutter.Models;
using Microsoft.EntityFrameworkCore;

namespace api_app_pizza_flutter.Data
{
    //Add-Migration "InitialMigration" khởi tạo database
    // AppDbContext: Quản lý kết nối và ánh xạ các model tới cơ sở dữ liệu.
    public class AppOrderDbContext : DbContext
    {
        public AppOrderDbContext(DbContextOptions<AppOrderDbContext> options) : base(options)
        {
        }

        // DbSet cho các bảng trong cơ sở dữ liệu
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Ranking> Rankings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình khóa chính
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<SubCategory>().HasKey(sc => sc.SubCategoryId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Address>().HasKey(a => a.AddressId);
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Topping>().HasKey(t => t.ToppingId);
            modelBuilder.Entity<Promotion>().HasKey(p => p.PromotionId);
            modelBuilder.Entity<PromotionProduct>().HasKey(pp => pp.PromotionProductId);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<OrderDetail>().HasKey(od => od.OrderDetailId);
            modelBuilder.Entity<Feedback>().HasKey(f => f.FeedbackId);
            modelBuilder.Entity<Suggestion>().HasKey(s => s.SuggestionId);
            modelBuilder.Entity<Ranking>().HasKey(r => r.RankingId);

            // Cấu hình khóa ngoại
           

            // Address -> User
            modelBuilder.Entity<Address>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId);

            // Product -> Category
            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            // Product -> SubCategory (nullable)
            modelBuilder.Entity<Product>()
                .HasOne<SubCategory>()
                .WithMany()
                .HasForeignKey(p => p.SubCategoryId)
                .IsRequired(false);

            // Topping -> Product
            modelBuilder.Entity<Topping>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(t => t.ProductId);

            // PromotionProduct -> Promotion
            modelBuilder.Entity<PromotionProduct>()
                .HasOne<Promotion>()
                .WithMany()
                .HasForeignKey(pp => pp.PromotionId);

            // PromotionProduct -> Product
            modelBuilder.Entity<PromotionProduct>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(pp => pp.ProductId);

            // Order -> User
            modelBuilder.Entity<Order>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId);

            // Order -> Address (nullable)
            modelBuilder.Entity<Order>()
                .HasOne<Address>()
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .IsRequired(false);

            // Order -> Promotion (nullable)
            modelBuilder.Entity<Order>()
                .HasOne<Promotion>()
                .WithMany()
                .HasForeignKey(o => o.PromotionId)
                .IsRequired(false);

            // OrderDetail -> Order
            modelBuilder.Entity<OrderDetail>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(od => od.OrderId);

            // OrderDetail -> Product
            modelBuilder.Entity<OrderDetail>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(od => od.ProductId);

            // OrderDetail -> Topping (nullable)
            modelBuilder.Entity<OrderDetail>()
                .HasOne<Topping>()
                .WithMany()
                .HasForeignKey(od => od.ToppingId)
                .IsRequired(false);

            // Feedback -> Order
            modelBuilder.Entity<Feedback>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(f => f.OrderId);

            // Feedback -> User
            modelBuilder.Entity<Feedback>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.UserId);
            // Feedback -> Product
            modelBuilder.Entity<Feedback>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(f => f.ProductId);
            // Suggestion -> Product (ProductID)
            modelBuilder.Entity<Suggestion>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(s => s.ProductId);

            // Suggestion -> Product (SuggestedProductID)
            modelBuilder.Entity<Suggestion>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(s => s.SuggestedProductId);

            // Ranking -> Product
            modelBuilder.Entity<Ranking>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(r => r.ProductId);
        }
    }
}