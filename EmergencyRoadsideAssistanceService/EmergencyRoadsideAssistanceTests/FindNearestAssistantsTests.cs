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
    public class FindNearestAssistantsTests
    {
        Mock<ILogger<RoadsideAssistanceController>> mockLogger;
        Mock<IRoadsideAssistanceService> mockRoadsideAssistantService;

        public FindNearestAssistantsTests()
        {
            mockLogger = new Mock<ILogger<RoadsideAssistanceController>>();
            mockRoadsideAssistantService = new Mock<IRoadsideAssistanceService>();

        }

        [TestMethod]
        public async Task FindNearestAssistants_1()
        {
            mockRoadsideAssistantService.Setup(x => x.FindNearestAssistants(It.IsAny<Geolocation>(), It.IsAny<int>())).ReturnsAsync(new SortedSet<Assistant>
            {
                new Assistant { AssistantId=100, AssistantName="TEST", ClosenessIndex=1 }
            });

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.FindNearestAssistants(12, 23, 5);

            Assert.IsNotNull(result);

            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult);

            var objResult = okResult.Value as SortedSet<Assistant>;
            Assert.AreEqual(objResult.Count, 1);

        }

        [TestMethod]
        public async Task FindNearestAssistants_2()
        {
            mockRoadsideAssistantService.Setup(x => x.FindNearestAssistants(It.IsAny<Geolocation>(), It.IsAny<int>())).ReturnsAsync(new SortedSet<Assistant>
            {
                new Assistant { AssistantId=100, AssistantName="TEST", ClosenessIndex=1 }
            });

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.FindNearestAssistants(null, 23, 5);

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Invalid location provided !");

        }

        [TestMethod]
        public async Task FindNearestAssistants_3()
        {
            mockRoadsideAssistantService.Setup(x => x.FindNearestAssistants(It.IsAny<Geolocation>(), It.IsAny<int>())).ReturnsAsync(new SortedSet<Assistant>
            {
                new Assistant { AssistantId=100, AssistantName="TEST", ClosenessIndex=1 }
            });

            RoadsideAssistanceController controller = new RoadsideAssistanceController(mockLogger.Object, mockRoadsideAssistantService.Object);
            var result = await controller.FindNearestAssistants(11, 23, 0);

            Assert.IsNotNull(result);

            var badResult = (BadRequestObjectResult)result;
            Assert.IsNotNull(badResult);

            var objResult = badResult.Value as ErrorDetails;
            Assert.AreEqual(objResult.ErrorText, "Limit cannot be less than 1 !");

        }

    }
}
