using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDTu.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; } // Liên kết với người dùng
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } // Danh sách chi tiết đơn hàng
    }
}
