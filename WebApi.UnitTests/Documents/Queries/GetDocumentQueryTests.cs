using FluentAssertions;
using GLPIService.Application.Documents.Queries.GetDocument;

namespace WebApi.UnitTests.Documents.Queries {

    using static Testing;

    public class GetDocumentQueryTests : TestBase {

        [Test]
        public async Task Success_DocumentReceived() {

            // arrange
            var command = new GetDocumentQuery {
                DocumentId = 44592
            };

            // act
            var response = await SendAsync(command);

            // assert
            response.Should().NotBeNull();
            response.FileName.Should().Be("dummy.pdf");
            response.Content.Should().NotBeNull();
        }


    }
}
