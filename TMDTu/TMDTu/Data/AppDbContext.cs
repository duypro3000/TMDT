using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDTu.Model;
using System.Configuration;

namespace TMDTu.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor EF Core cần
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Nếu bạn vẫn muốn khởi tạo bằng GetOptions() thì giữ thêm constructor này cũng được
        public AppDbContext() : base(GetOptions())
        {
        }

        // Hàm hỗ trợ tạo options khi dùng constructor không tham số
        private static DbContextOptions<AppDbContext> GetOptions()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
