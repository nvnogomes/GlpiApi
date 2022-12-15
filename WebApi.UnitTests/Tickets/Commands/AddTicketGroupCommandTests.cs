using FluentAssertions;
using GLPIService.Application.Common.Enums;
using GLPIService.Application.Tickets.Commands.AddTicketGroup;

namespace WebApi.UnitTests.Tickets.Commands {

    using static Testing;

    public class AddTicketGroupCommandTests : TestBase {

        [Test]
        public async Task Success_TicketGroupAdded() {
            // arrange

            var command = new AddTicketGroupCommand {
                TicketId = 15916,
                GroupId = 256,
                TypeId = UserType.Requester.Value
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().BePositive();
            response.Should().BeGreaterThanOrEqualTo(0);
        }

    }
}
