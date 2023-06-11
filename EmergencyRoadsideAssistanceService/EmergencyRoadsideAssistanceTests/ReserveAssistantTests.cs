using EmergencyRoadsideAssistanceService.Common;
using EmergencyRoadsideAssistanceService.Controllers;
using EmergencyRoadsideAssistanceService.DTOs;
using EmergencyRoadsideAssistanceService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyRoadsideAssistanceTests
{

    [TestClass]
    public class ReserveAssistantTests
    {

        Mock<ILogger<RoadsideAssistanceController>> mockLogger;
        Mock<IRoadsideAssistanceService> mockRoadsideAssistantService;


        public ReserveAssistantTests()
        {
            mockLogger = new Mock<ILogger<RoadsideAssistanceController>>();
            mockRoadsideAssistantService = new Mock<IRoadsideAssistanceService>();
        }

        [TestMethod]
        public async Task ReserveAssistant_1()
        {
            mockRoadsideAssistantService.Setup(x => x.ReserveAssistant(It.IsAny<Customer>(), It.IsAny<Geolocation>())).ReturnsAsync(Optional<Assistant>.CreateEmpty());

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.ReserveAssistant(null);

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid inputs provided !");

        }

        [TestMethod]
        public async Task ReserveAssistant_2()
        {
            mockRoadsideAssistantService.Setup(x => x.ReserveAssistant(It.IsAny<Customer>(), It.IsAny<Geolocation>())).ReturnsAsync(Optional<Assistant>.CreateEmpty());

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.ReserveAssistant(new CustomerGeolocationDto { Customer = new Customer() });

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid location provided !");

        }

        [TestMethod]
        public async Task ReserveAssistant_3()
        {
            mockRoadsideAssistantService.Setup(x => x.ReserveAssistant(It.IsAny<Customer>(), It.IsAny<Geolocation>())).ReturnsAsync(Optional<Assistant>.CreateEmpty());

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.ReserveAssistant(new CustomerGeolocationDto { Geolocation = new Geolocation() });

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid customer information provided !");

        }


    }
}
