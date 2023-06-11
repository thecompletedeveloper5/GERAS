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
    public class ReleaseAssistantTests
    {

        Mock<ILogger<RoadsideAssistanceController>> mockLogger;
        Mock<IRoadsideAssistanceService> mockRoadsideAssistantService;


        public ReleaseAssistantTests()
        {
            mockLogger = new Mock<ILogger<RoadsideAssistanceController>>();
            mockRoadsideAssistantService = new Mock<IRoadsideAssistanceService>();
        }

        [TestMethod]
        public async Task ReleaseAssistant_1()
        {
            mockRoadsideAssistantService.Setup(x => x.ReleaseAssistant(It.IsAny<Customer>(), It.IsAny<Assistant>()));

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.ReleaseAssistant(null);

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid inputs provided !");

        }

        [TestMethod]
        public async Task ReleaseAssistant_2()
        {
            mockRoadsideAssistantService.Setup(x => x.ReleaseAssistant(It.IsAny<Customer>(), It.IsAny<Assistant>()));

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.ReleaseAssistant(new CustomerAssistantDto { Assistant= new Assistant() });

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid customer information provided !");

        }

        [TestMethod]
        public async Task ReleaseAssistant_3()
        {
            mockRoadsideAssistantService.Setup(x => x.ReleaseAssistant(It.IsAny<Customer>(), It.IsAny<Assistant>()));

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.ReleaseAssistant(new CustomerAssistantDto { Customer= new Customer { CustomerId=1 },  Assistant = new Assistant() });

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid assistant provided !");

        }

    }
}
