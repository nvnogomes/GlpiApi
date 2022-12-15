using FluentAssertions;
using GLPIService.Application.Common.Enums;
using GLPIService.Application.Tickets.Commands.AddTicketUser;

namespace WebApi.UnitTests.Tickets.Commands {

    using static Testing;

    public class AddTicketUserCommandTests : TestBase {


        [Test]
        public async Task Success_TicketUserAdded() {
            // arrange

            var command = new AddTicketUserCommand {
                TicketId = 15916,
                UserId = 966,
                TypeId = UserType.Watcher.Value
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().BePositive();
        }



    }
}
