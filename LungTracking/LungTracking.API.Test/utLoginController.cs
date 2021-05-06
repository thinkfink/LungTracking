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

namespace LungTracking.API.Test
{
    [TestClass]
    public class utLoginController
    {
        User loginPass = new User("cdelwater0", "MWlgqO");
        User loginFail = new User("cdelwater0", "password");
        User user = new User();
        Guid userId = Guid.Parse("fce022bb-584d-42d6-97c0-b83ac1985807");

        [TestMethod]
        public void LoginPassTest()
        {
            Task.Run(async () =>
            {
                var controller = new LoginController();
                var task = controller.Post(loginPass);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<User>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void LoginFailTest()
        {
            Task.Run(async () =>
            {
                var controller = new LoginController();
                var task = controller.Post(loginFail);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<User>;
                Assert.IsNull(createdResult);
            });
        }
    }
}