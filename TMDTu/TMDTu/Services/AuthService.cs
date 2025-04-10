using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDTu.Data;
using TMDTu.Model;

namespace TMDTu.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Login(string usernameOrEmail, string password)
        {
            var user = _context.User
                .FirstOrDefault(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);

            if (user == null)
            {
                throw new Exception("Username hoặc password không đúng");
            }

            if (!user.Enable && user.TimeToEnable > DateTime.Now)
            {
                throw new Exception($"Tài khoản bị khóa đến {user.TimeToEnable}");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                user.LoginCount++;
                if (user.LoginCount >= 5)
                {
                    user.Enable = false;
                    user.TimeToEnable = DateTime.Now.AddMinutes(10);
                    _context.SaveChanges();
                    throw new Exception("Tài khoản bị khóa 10 phút");
                }
                _context.SaveChanges();
                throw new Exception("Username hoặc password không đúng");
            }

            user.LoginCount = 0;
            _context.SaveChanges();
            return user;
        }

        public bool RequiresCaptcha(User user)
        {
            return user.LoginCount >= 3;
        }
    }
}
