using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMDTu.Data;
using TMDTu.Model;
using TMDTu.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TMDTu.Forms
{
    public partial class CartForm : Form
    {
        private readonly User _currentUser;
        private readonly List<(Product Product, int Quantity)> _cart;
        private readonly OrderService _orderService;
        public CartForm(User user, List<(Product Product, int Quantity)> cart)
        {
            InitializeComponent();
            _currentUser = user;
            _cart = cart;
            _orderService = new OrderService(new AppDbContext());
            LoadCart();
        }
        private void LoadCart()
        {
            dgvCart.DataSource = _cart.Select(item => new
            {
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                Price = item.Product.Price,
                Total = item.Product.Price * item.Quantity
            }).ToList();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cart.Count == 0)
                {
                    MessageBox.Show("Giỏ hàng trống!");
                    return;
                }

                _orderService.PlaceOrder(_currentUser, _cart);
                MessageBox.Show("Đặt hàng thành công!");
                _cart.Clear();
                LoadCart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }

        }

        private void CartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
