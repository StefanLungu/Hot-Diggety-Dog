using Domain.Dtos.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Repository.v1;
using Presentation.Tests;
using Security.Services;
using Security.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers.v1;
using Xunit;

namespace HotDiggetyDogTests
{
    public class ScalabilityTests : DatabaseBaseTest
    {
        private readonly ProductsController _productsController;
        private readonly UsersController _usersController;
        private const string SECRET = "JWT SECRET LONG KEY";

        public ScalabilityTests()
        {
            UsersRepository userRepository = new(dataContext);
            IOptions<SecuritySettings> securitySettings = Options.Create(new SecuritySettings());
            securitySettings.Value.Secret = SECRET;
            JwtService jwtService = new(securitySettings);
            _usersController = new UsersController(userRepository, jwtService);
            Repository<Product> productRepository = new(dataContext);
            _productsController = new ProductsController(productRepository);
        }

        [Fact]
        public async void Register10000CustomersInBatchesOf100_ShouldReturn_CreatedAt()
        {
            List<RegisterRequest> registerRequests = RequestFactory.Get10000RegisterDtos();

            int batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)10000 / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentRequests = registerRequests.Skip(i * batchSize).Take(batchSize);
                var tasks = currentRequests.Select(req => _usersController.Register(req));

                ActionResult<User>[] actionResult = await Task.WhenAll(tasks);

                Assert.IsType<CreatedAtActionResult>(actionResult[0].Result);
            }
        }

        [Fact]
        public async void Authenticate10000CustomersInBatchesOf100_ShouldReturn_Ok()
        { 
            List<AuthenticateRequest> authenticateRequests = RequestFactory.Get10000AuthenticateDtos();

            int batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)10000 / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentRequests = authenticateRequests.Skip(i * batchSize).Take(batchSize);
                var tasks = currentRequests.Select(req => _usersController.Authenticate(req));

                ActionResult[] actionResult = await Task.WhenAll(tasks);

                Assert.IsType<OkObjectResult>(actionResult[0]);
            }
        }

        [Fact]
        public async void GetProductsFor10000CustomersInBatchesOf100_ShouldReturn_Ok()
        {
            int batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)10000 / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentRequests = Enumerable.Repeat(1, 100).ToList();
                var tasks = currentRequests.Select(req => _productsController.GetProducts());

                ActionResult<IEnumerable<Product>>[] actionResult = await Task.WhenAll(tasks);

                Assert.IsType<OkObjectResult>(actionResult[0].Result);               
            }
        }
    }
}