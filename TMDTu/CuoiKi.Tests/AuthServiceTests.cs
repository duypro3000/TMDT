using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDTu.Data;
using TMDTu.Model;
using TMDTu.Services;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace CuoiKi.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod] // Thay [Fact] bằng [TestMethod]
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TMDT")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var user = new User
                {
                    UserID = 1,
                    Username = "duy",
                    Email = "duy@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123"),
                    LoginCount = 0,
                    Enable = true,
                    TimeToEnable = null
                };
                context.User.Add(user); // Sửa từ context.User thành context.Users
                context.SaveChanges();

                var authService = new AuthService(context);

                // Act
                var result = authService.Login("duy", "123");

                // Assert
                Assert.IsNotNull(result); // Thay Assert.NotNull bằng Assert.IsNotNull
                Assert.AreEqual("duy", result.Username); // Thay Assert.Equal bằng Assert.AreEqual
            }
        }

        [TestMethod] // Thay [Fact] bằng [TestMethod]
        public void Login_InvalidCredentials_ThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TMDT_Invalid")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var user = new User
                {
                    UserID = 1,
                    Username = "duy",
                    Email = "duy@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123"),
                    LoginCount = 0,
                    Enable = true,
                    TimeToEnable = null
                };
                context.User.Add(user); // Sửa từ context.User thành context.Users
                context.SaveChanges();

                var authService = new AuthService(context);

                // Act & Assert
                Assert.ThrowsException<Exception>(() => authService.Login("duy", "sai_mk")); // Thay Assert.Throws bằng Assert.ThrowsException
            }
        }
    }
}
