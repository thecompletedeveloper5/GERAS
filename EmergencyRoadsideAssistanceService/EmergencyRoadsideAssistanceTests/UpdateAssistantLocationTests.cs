using EmergencyRoadsideAssistanceService.Controllers;
using EmergencyRoadsideAssistanceService.DTOs;
using EmergencyRoadsideAssistanceService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyRoadsideAssistanceTests
{
    [TestClass]
    public class UpdateAssistantLocationTests
    {
        Mock<ILogger<RoadsideAssistanceController>> mockLogger;
        Mock<IRoadsideAssistanceService> mockRoadsideAssistantService;

        public UpdateAssistantLocationTests
()
        {
            mockLogger = new Mock<ILogger<RoadsideAssistanceController>>();
            mockRoadsideAssistantService = new Mock<IRoadsideAssistanceService>();
        }

        [TestMethod]
        public async Task UpdateAssistantLocation_1()
        {
            mockRoadsideAssistantService.Setup(x => x.UpdateAssistantLocation(It.IsAny<Assistant>(), It.IsAny<Geolocation>()));

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.UpdateAssistantLocation(default(AssistantGeolocationDto));

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Input not provided !");

        }

        [TestMethod]
        public async Task UpdateAssistantLocation_2()
        {
            mockRoadsideAssistantService.Setup(x => x.UpdateAssistantLocation(It.IsAny<Assistant>(), It.IsAny<Geolocation>()));

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.UpdateAssistantLocation(new AssistantGeolocationDto { Assistant = new Assistant(), Geolocation = new Geolocation() });

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid assistant provided !");

        }

        [TestMethod]
        public async Task UpdateAssistantLocation_3()
        {
            mockRoadsideAssistantService.Setup(x => x.UpdateAssistantLocation(It.IsAny<Assistant>(), It.IsAny<Geolocation>()));

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.UpdateAssistantLocation(new AssistantGeolocationDto { Assistant = new Assistant { AssistantId=1 } });

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid location provided !");

        }

    }
}
