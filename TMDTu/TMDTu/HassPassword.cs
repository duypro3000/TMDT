using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMDTu
{
    public partial class HassPassword : Form
    {
        public HassPassword()
        {
            InitializeComponent();
        }

        private void HassPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            // Lấy mật khẩu từ TextBox
            string password = txtPassword.Text;

            // Mã hóa mật khẩu bằng BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Hiển thị chuỗi mã hóa trên Label
            lblResult.Text = hashedPassword;
        }
    }
}
