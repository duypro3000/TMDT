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
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;
        private User _currentUser;
        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService(new AppDbContext()); // Cung cấp AppDbContext
            pnlCaptcha.Visible = false;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //var forgotPasswordForm = new ForgotPasswordForm(); // Giả sử bạn có form ForgotPasswordForm
            //forgotPasswordForm.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsernameOrEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Phải điền đầy đủ username và password");
                    return;
                }

                if (_currentUser != null && _authService.RequiresCaptcha(_currentUser))
                {
                    if (string.IsNullOrEmpty(txtCaptcha.Text) || txtCaptcha.Text != "1234")
                    {
                        MessageBox.Show("Captcha không đúng");
                        return;
                    }
                }

                var user = _authService.Login(txtUsernameOrEmail.Text, txtPassword.Text);
                _currentUser = user;

                if (_authService.RequiresCaptcha(user))
                {
                    pnlCaptcha.Visible = true;
                    return;
                }

                MessageBox.Show("Đăng nhập thành công!");
                //var productListForm = new ProductListForm(user);
                //productListForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
