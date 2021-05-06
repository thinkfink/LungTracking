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
    public class utAppointmentIdController
    {
        Appointment appt = new Appointment();
        Guid appointmentId = Guid.Parse("66dde9a9-0cab-4096-a15a-57c2749b60a0");

        [TestMethod]
        public void GetByAppointmentIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new AppointmentIdController();
                var task = controller.Get(appointmentId);
                IEnumerable<Appointment> appointments = (IEnumerable<Appointment>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }
    }
}