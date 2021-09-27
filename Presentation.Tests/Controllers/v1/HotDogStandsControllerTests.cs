using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository.v1;
using System;
using System.Collections.Generic;
using WebApi.Controllers.v1;
using Xunit;

namespace Presentation.Tests.Controllers.v1
{
    [Collection("Sequential")]
    public class HotDogStandsControllerTests : DatabaseBaseTest
    {
        private readonly HotDogStandsController _hotDogStandsController;

        public HotDogStandsControllerTests()
        {
            Repository<HotDogStand> hotDogStandRepository = new(dataContext);
            _hotDogStandsController = new HotDogStandsController(hotDogStandRepository);
        }

        [Fact]
        public async void GetStands_ShouldReturn_Ok()
        {
            // Act
            ActionResult<IEnumerable<HotDogStand>> actionResult = await _hotDogStandsController.GetStands();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetStandBy_Generated_Id_ShouldReturn_NotFound()
        {
            //Arrange
            Guid id = Guid.Parse("f7cb8e84-b440-4b6f-886e-496cc5dc3ccd");

            // Act
            ActionResult<HotDogStand> actionResult = await _hotDogStandsController.GetStandById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetStandById_ShouldReturn_Ok()
        {
            //Arrange
            Guid id = Guid.Parse("154b9350-ccef-4ab1-aa7a-9eddc0b3cd6a");

            // Act
            ActionResult<HotDogStand> actionResult = await _hotDogStandsController.GetStandById(id);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Create_Null_Stand_ShouldReturn_BadRequest()
        {
            //Arrange
            HotDogStand stand = null;

            // Act
            ActionResult<HotDogStand> actionResult = await _hotDogStandsController.CreateStand(stand);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Create_New_Stand_ShouldReturn_CreatedAtAction()
        {
            //Arrange
            HotDogStand hotDogStand = new()
            {
                Address = "Address"
            };

            // Act
            ActionResult<HotDogStand> actionResult = await _hotDogStandsController.CreateStand(hotDogStand);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public async void Update_Stand_With_Null_ShouldReturn_BadRequest()
        {
            //Arrange
            HotDogStand hotDogStand = null;

            // Act
            ActionResult<HotDogStand> actionResult = await _hotDogStandsController.UpdateStand(hotDogStand);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Remove_NonExisting_Stand_ShouldReturn_NotFound()
        {
            //Arrange
            Guid id = Guid.Parse("0e9b0951-0788-4498-a694-f50a916c56b5");

            // Act
            ActionResult<HotDogStand> actionResult = await _hotDogStandsController.RemoveStand(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void RemoveStand_ShouldReturn_NoContent()
        {
            //Arrange
            Guid id = Guid.Parse("2d6e0358-3307-409f-90d4-4656f5c63e7f");

            // Act
            ActionResult<HotDogStand> actionResult = await _hotDogStandsController.RemoveStand(id);

            // Assert
            Assert.IsType<NoContentResult>(actionResult.Result);
        }
    }
}

