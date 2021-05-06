using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using LungTracking.API.Controllers;
using LungTracking.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AJP.LungTracking.API.Test
{
    [TestClass]
    public class utUserController
    {
        User loginPass = new User("cdelwater0", "MWlgqO");
        User loginFail = new User("cdelwater0", "password");
        User user = new User();
        Guid userId = Guid.Parse("fce022bb-584d-42d6-97c0-b83ac1985807");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new UserController();
                var task = controller.Get();
                IEnumerable<User> users = (IEnumerable<User>)task;
                Assert.AreEqual(6, users.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByUserIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new UserController();
                var task = controller.Get(userId);
                IEnumerable<User> users = (IEnumerable<User>)task;
                Assert.AreEqual(1, users.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                user.Username = "daffyduck";
                user.Password = "quack";
                user.Role = 0;
                user.Email = "dduck@gmail.com";
                user.Created = DateTime.Now;
                user.LastLogin = DateTime.Now;

                var controller = new UserController();
                var task = controller.Post(user);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<User>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            user.Email = "mrduck@gmail.com";

            Task.Run(async () =>
            {
                var controller = new UserController();
                var task = controller.Put(user);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<User>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void UpdatePasswordTest()
        {
            user.Password = "quack";
            user.NewPassword = "quackquack";
            user.ConfirmPassword = "quackquack";

            Task.Run(async () =>
            {
                var controller = new UserController();
                var task = controller.Put(user.Id, user.Password, user.NewPassword, user.ConfirmPassword);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<User>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new UserController();
                var task = controller.Delete(user.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<User>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}