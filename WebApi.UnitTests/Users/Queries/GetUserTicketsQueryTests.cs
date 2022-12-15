using FluentAssertions;
using GLPIService.Application.Users.Queries.GetUserTickets;

namespace WebApi.UnitTests.Users.Queries {

    using static Testing;

    public class GetUserTicketsQueryTests : TestBase {

        [Test]
        public async Task Success_UserTicketsReceived() {

            // arrange
            var command = new GetUserTicketsQuery {
                UserId = 954
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().NotBeNull();
            response.Tickets.Count().Should().BeGreaterThan(0);
        }

    }
}
