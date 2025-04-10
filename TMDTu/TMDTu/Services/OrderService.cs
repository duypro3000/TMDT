using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDTu.Data;
using TMDTu.Model;

namespace TMDTu.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public void PlaceOrder(User user, List<(Product Product, int Quantity)> cartItems)
        {
            var order = new Order
            {
                UserID = user.UserID,
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(item => item.Product.Price * item.Quantity),
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = item.Product.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
