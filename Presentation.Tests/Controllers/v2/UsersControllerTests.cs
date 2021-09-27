using Application.Features.UserFeatures.Commands;
using Application.Features.UserFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Security.Helpers;
using Security.Services;
using Security.Settings;
using System;
using System.Net.Http;
using WebApi.Controllers.v2;
using Xunit;

namespace Presentation.Tests.Controllers.v2
{
    public class UsersControllerTests : DatabaseBaseTest
    {
        private readonly Mock<IMediator> _mediator;
        private const string SECRET = "JWT SECRET LONG KEY";
        private readonly IFacebookAuthService _facebookService;
        private readonly IJwtService _jwtService;

        public UsersControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _jwtService = GetJwt();
            _facebookService = GetFacebookService();
        }

        [Fact]
        public void Mediatr_GetUsers_ShouldReturn_OK()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), new System.Threading.CancellationToken()));
            var usersController = new UsersController(_mediator.Object, _jwtService, _facebookService);

            //Act
            var result = usersController.GetUsers().Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Mediatr_GetCustomers_ShouldReturn_OK()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), new System.Threading.CancellationToken()));
            JwtService jwtService = GetJwt();
            var usersController = new UsersController(_mediator.Object, jwtService, _facebookService);

            //Act
            var result = usersController.GetCustomers().Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Mediatr_GetUserBy_Generated_Id_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), new System.Threading.CancellationToken()));
            var usersController = new UsersController(_mediator.Object, _jwtService, _facebookService);
            Guid userId = Guid.Parse("c5c230eb-e39c-4a04-a6f0-89d577e85a1d");

            //Act
            var result = usersController.GetUserById(userId).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_Null_User_ShouldReturn_BadRequest()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), new System.Threading.CancellationToken()));
            var usersController = new UsersController(_mediator.Object, _jwtService, _facebookService);
            CreateUserCommand createUser = null;

            //Act
            var result = usersController.Register(createUser).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Authenticate_NonExisting_User_ShouldReturn_BadRequest()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<AuthenticateUserQuery>(), new System.Threading.CancellationToken()));
            var usersController = new UsersController(_mediator.Object, _jwtService, _facebookService);
            AuthenticateUserQuery authenticateUser = new()
            {
                Username = "Test",
                Password = "Test"
            };

            //Act
            var result = usersController.Authenticate(authenticateUser).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Delete_NonExisting_User_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), new System.Threading.CancellationToken()));
            var usersController = new UsersController(_mediator.Object, _jwtService, _facebookService);
            Guid userId = Guid.Parse("ef7b6f44-d1a4-4bcf-8a2a-bc66424afb4d");

            //Act
            var result = usersController.DeleteUser(userId).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        private static JwtService GetJwt()
        {
            IOptions<SecuritySettings> securitySettings = Options.Create(new SecuritySettings());
            securitySettings.Value.Secret = SECRET;
            return new(securitySettings);
        }

        private static FacebookAuthService GetFacebookService()
        {
            IOptions<FacebookAuthSettings> facebookAuthSettings = Options.Create(new FacebookAuthSettings());
            var mockFactory = new Mock<IHttpClientFactory>();
            return new(facebookAuthSettings, mockFactory.Object);
        }
    }
}
