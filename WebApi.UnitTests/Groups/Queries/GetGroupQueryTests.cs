using FluentAssertions;
using GLPIService.Application.Groups.Queries.GetGroup;

namespace WebApi.UnitTests.Groups.Queries {

    using static Testing;

    internal class GetGroupQueryTests : TestBase {


        [Test]
        public async Task Success_GroupReceived() {

            // arrange
            var command = new GetGroupQuery {
                GroupId = 256
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().NotBeNull();
            response.Name.Should().Be("Tecnologias de Informação");
        }


    }
}
