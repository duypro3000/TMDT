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

namespace TMDTu.Forms
{
    public partial class ProductListForm : Form
    {
        private readonly ProductService _productService;
        private readonly User _currentUser;
        private List<(Product Product, int Quantity)> _cart;
        public ProductListForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _productService = new ProductService(new AppDbContext());
            _cart = new List<(Product, int)>();
            LoadProducts();
        }
        private void LoadProducts()
        {
            var products = _productService.GetAllProducts();
            dgvProducts.DataSource = products;
        }
        private void ProductListForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;
                var quantity = 1; // Có thể thêm TextBox để người dùng nhập số lượng
                _cart.Add((selectedProduct, quantity));
                MessageBox.Show($"Đã thêm {selectedProduct.Name} vào giỏ hàng!");
            }
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            var cartForm = new CartForm(_currentUser, _cart);
            cartForm.Show();
            this.Hide();

        }
    }
}
