using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utUserManager
    {
        [TestMethod]
        public void LoginPassTest()
        {
            User user = new User("cdelwater0", "MWlgqO");
            bool actual = UserManager.Login(user);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void LoginFailTest()
        {
            User user = new User("cdelwater0", "password");
            bool actual = UserManager.Login(user);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void LoadTest()
        {
            List<User> users = new List<User>();
            users = UserManager.Load();
            int expected = 250;
            Assert.AreEqual(expected, users.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User();
            user.Username = "daffyduck";
            user.Password = "quack";
            user.Role = 0;
            user.Email = "dduck@gmail.com";
            user.Created = DateTime.Now;
            user.LastLogin = DateTime.Now;

            int result = UserManager.Insert(user);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<User> users = UserManager.Load();

            User user = users.Where(p => p.Email == "dduck@gmail.com").FirstOrDefault();

            user.Email = "mrduck@gmail.com";

            UserManager.Update(user);

            User updateduser = users.FirstOrDefault(p => p.Id == user.Id);

            Assert.AreEqual(user.Email, updateduser.Email);
        }

        [TestMethod]
        public void UpdatePasswordTest()
        {
            List<User> users = UserManager.Load();

            User user = users.Where(p => p.Email == "dduck@gmail.com").FirstOrDefault();

            user.Password = "quack";
            user.NewPassword = "quackquack";
            user.ConfirmPassword = "quackquack";

            UserManager.UpdatePassword(user.Id, user.Password, user.NewPassword, user.ConfirmPassword);

            User updateduser = users.FirstOrDefault(p => p.Id == user.Id);

            Assert.AreEqual(user.Password, updateduser.Password);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<User> users = UserManager.Load();
            User user = users.Where(p => p.Username == "daffyduck").FirstOrDefault();

            int results = UserManager.Delete(user.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
