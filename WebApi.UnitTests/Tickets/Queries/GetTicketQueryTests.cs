using FluentAssertions;
using GLPIService.Application.Tickets.Queries.GetTicket;

namespace WebApi.UnitTests.Tickets.Queries {

    using static Testing;

    public class GetTicketQueryTests : TestBase {


        [Test]
        public async Task Success_TicketReceived() {

            // arrange
            var command = new GetTicketQuery {
                TicketId = 15961
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().NotBeNull();
        }




    }
}
