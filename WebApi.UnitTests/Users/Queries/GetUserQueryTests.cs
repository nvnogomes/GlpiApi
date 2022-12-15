using FluentAssertions;
using GLPIService.Application.Users.Queries.GetUser;

namespace WebApi.UnitTests.Users.Queries {

    using static Testing;

    public class GetUserQueryTests : TestBase {

        [Test]
        public async Task Success_UserReceived() {

            // arrange
            var command = new GetUserQuery {
                UserId = 966
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().NotBeNull();
            response.Name.Should().Be("GLPI_USER");
        }

    }
}
