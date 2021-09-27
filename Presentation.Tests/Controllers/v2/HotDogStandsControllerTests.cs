using Application.Features.HotDogStandsFeatures.Commands;
using Application.Features.HotDogStandsFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebApi.Controllers.v2;
using Xunit;

namespace Presentation.Tests.Controllers.v2
{
    public class HotDogStandsControllerTests : DatabaseBaseTest
    {
        private readonly Mock<IMediator> _mediator;

        public HotDogStandsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Mediatr_GetStands_ShouldReturn_OK()
        {
            //Arrange&Act
            _mediator.Setup(x => x.Send(It.IsAny<GetStandsQuery>(), new System.Threading.CancellationToken()));
            var hotDogStandsController = new HotDogStandsController(_mediator.Object);

            var result = hotDogStandsController.GetStands().Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Mediatr_GetStandBy_NonExisting_Id_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetStandByIdQuery>(), new System.Threading.CancellationToken()));
            var hotDogStandsController = new HotDogStandsController(_mediator.Object);
            Guid standId = Guid.Parse("aa5d86f3-3dca-4d05-85a9-5d94626976a0");

            //Act
            var result = hotDogStandsController.GetStandById(standId).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_Null_Stand_ShouldReturn_BadRequest()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<CreateStandCommand>(), new System.Threading.CancellationToken()));
            var hotDogStandsController = new HotDogStandsController(_mediator.Object);
            CreateStandCommand createdStand = null;


            //Act
            var result = hotDogStandsController.CreateStand(createdStand).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_New_Stand_ShouldReturn_OK()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<CreateStandCommand>(), new System.Threading.CancellationToken()));
            var hotDogStandsController = new HotDogStandsController(_mediator.Object);
            CreateStandCommand createdstand = new()
            {
                Address = "Address Test"
            };

            //Act
            var result = hotDogStandsController.CreateStand(createdstand).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Update_NonExisting_Stand_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<UpdateStandCommand>(), new System.Threading.CancellationToken()));
            var hotDogStandsController = new HotDogStandsController(_mediator.Object);
            Guid standId = Guid.Parse("612fd332-5859-4192-af39-94c183cdf328");
            UpdateStandCommand updatedstand = new()
            {
                Id = standId,
                Address = "Test Address"
            };

            //Act
            var result = hotDogStandsController.UpdateStand(standId, updatedstand).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Remove_NonExisting_Stand_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<UpdateStandCommand>(), new System.Threading.CancellationToken()));
            var hotDogStandsController = new HotDogStandsController(_mediator.Object);
            Guid standId = Guid.Parse("f6213f98-4029-4752-999f-6811ca57528b");

            //Act
            var result = hotDogStandsController.RemoveStand(standId).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
