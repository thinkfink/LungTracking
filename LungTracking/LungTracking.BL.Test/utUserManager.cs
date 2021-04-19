using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utUserManager
    {
        Guid userId = Guid.Parse("fce022bb-584d-42d6-97c0-b83ac1985807");

        [TestMethod]
        public void LoginPassTest()
        {
            Task.Run(async () => 
            {
                User user = new User("cdelwater0", "MWlgqO");
                bool actual = await UserManager.Login(user);
                Assert.IsTrue(actual);
            });
        }

        [TestMethod]
        public void LoginFailTest()
        {
            Task.Run(async () => 
            {
                User user = new User("cdelwater0", "password");
                bool actual = await UserManager.Login(user);
                Assert.IsFalse(actual);
            });
        }

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await UserManager.Load();
                IEnumerable<Models.User> users = task;
                Assert.AreEqual(300, users.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByUserIdTest()
        {
            Task.Run(async () =>
            {
                var results = await UserManager.LoadByUserId(userId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                User user = new User();
                user.Username = "daffyduck";
                user.Password = "quack";
                user.Role = 0;
                user.Email = "dduck@gmail.com";
                user.Created = DateTime.Now;
                user.LastLogin = DateTime.Now;

                int results = await UserManager.Insert(user);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = UserManager.Load();
                IEnumerable<Models.User> users = task.Result;
                Models.User user = users.FirstOrDefault(p => p.Username == "daffyduck");
                user.Email = "mrduck@gmail.com";
                var results = UserManager.Update(user);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void UpdatePasswordTest()
        {
            Task.Run(async () =>
            {
                var task = UserManager.Load();
                IEnumerable<Models.User> users = task.Result;
                User user = users.Where(p => p.Email == "dduck@gmail.com").FirstOrDefault();
                user.Password = "quack";
                user.NewPassword = "quackquack";
                user.ConfirmPassword = "quackquack";
                UserManager.UpdatePassword(user.Id, user.Password, user.NewPassword, user.ConfirmPassword);
                User updateduser = users.FirstOrDefault(p => p.Id == user.Id);
                Assert.AreEqual(user.Password, updateduser.Password);
            });
            
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = UserManager.Load();
                IEnumerable<Models.User> users = task.Result;
                task.Wait();
                Models.User user = users.FirstOrDefault(p => p.Username == "daffyduck");
                var results = UserManager.Delete(user.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}
