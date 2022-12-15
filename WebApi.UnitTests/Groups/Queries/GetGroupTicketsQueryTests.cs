using FluentAssertions;
using GLPIService.Application.Groups.Queries.GetGroupTickets;

namespace WebApi.UnitTests.Groups.Queries {

    using static Testing;

    public class GetGroupTicketsQueryTests : TestBase {

        [Test]
        public async Task Success_GroupTicketsReceived() {

            // arrange
            var command = new GetGroupTicketsQuery {
                GroupId = 256
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().NotBeNull();
            response.Tickets.Count().Should().BeGreaterThan(0);
        }

    }
}
