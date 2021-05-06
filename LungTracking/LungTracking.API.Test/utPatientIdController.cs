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
    public class utPatientIdController
    {
        Patient patient = new Patient();
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientIdController();
                var task = controller.Get(patientId);
                IEnumerable<Patient> appointments = (IEnumerable<Patient>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }
    }
}