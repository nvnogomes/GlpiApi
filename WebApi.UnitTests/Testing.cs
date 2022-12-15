using GLPIService.Application;
using GLPIService.Application.Common.Interfaces;
using GLPIService.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace WebApi.UnitTests {

    [SetUpFixture]
    public class Testing {

        public static string TESTUSER => "Bonita Ballard";

        private static IConfiguration _configuration;

        private static IServiceScopeFactory _scopeFactory;


        [OneTimeSetUp]
        public void RunBeforeAnyTests() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            _configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddInfrastructure(_configuration);
            services.AddApplication();

            services.AddSingleton(_configuration);
            services.AddLogging();

            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor!);

            // Register testing version
            services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.Username == TESTUSER));

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>()!;
        }


        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>()!;

            return await mediator.Send(request);
        }



        [OneTimeTearDown]
        public void RunAfterAnyTests() {
        }
    }
}
