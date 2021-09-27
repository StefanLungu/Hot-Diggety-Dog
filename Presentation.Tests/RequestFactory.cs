using Domain.Dtos.Account;
using System.Collections.Generic;
using System.Web.Helpers;

namespace Presentation.Tests
{
    public static class RequestFactory
    {
        public static List<RegisterRequest> Get10000RegisterDtos()
        {
            List<RegisterRequest> registerRequests = new();
            for (int i = 0; i < 10000; i++)
            {
                RegisterRequest request = new();
                request.Username = $"UserName{i}";
                request.Email = $"{request.Username}@gmail.com";
                request.Password = Crypto.SHA256(request.Username);
                registerRequests.Add(request);
            }

            return registerRequests;
        }

        public static List<AuthenticateRequest> Get10000AuthenticateDtos()
        {
            List<AuthenticateRequest> authenticateRequests = new();
            for (int i = 0; i < 10000; i++)
            {
                AuthenticateRequest request = new();
                request.Username = "customer";
                request.Password = "customer";
                authenticateRequests.Add(request);
            }

            return authenticateRequests;
        }
    }
}
