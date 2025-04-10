using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDTu.Data
{
    public class DesignTimeDbContextFactory
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 🔧 Hardcoded connection string để tránh lỗi null
            var connectionString = "Server=LAPTOP-DJFD6UDO\\SQLEXPRESS;Database=TMDT;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
