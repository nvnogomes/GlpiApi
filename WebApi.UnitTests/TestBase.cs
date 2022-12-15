using Microsoft.AspNetCore.Http;
using Moq;

namespace WebApi.UnitTests {
    public class TestBase {

        public IFormFile FileMOQ_Pdf { get; set; }
        public IFormFile FileMOQ_Csv { get; set; }
        public IFormFile FileMOQ_Docx { get; set; }
        public IFormFile FileMOQ_Xlsx { get; set; }
        public IFormFile FileMOQ_Empty { get; set; }


        private static async Task<IFormFile> MOQFile(string content, string filename) {
            var fileMock = new Mock<IFormFile>();

            // setting content
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            await writer.WriteAsync(content);
            writer.Flush();
            ms.Position = 0;

            // setting moq
            fileMock.Setup(_ => _.FileName).Returns(filename);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock.Object;
        }


        [SetUp]
        public async Task SetupSnapshot() {

            FileMOQ_Pdf = await MOQFile("8% of 25 is the same as 25% of 8 and one of them is much easier to do in your head.", "Cameralism.pdf");
            FileMOQ_Csv = await MOQFile("He uses onomatopoeia as a weapon of mental destruction.", "Alvine.pdf");
            FileMOQ_Docx = await MOQFile("He had concluded that pigs must be able to fly in Hog Heaven.", "Milliad.Docx");
            FileMOQ_Xlsx = await MOQFile("Three generations with six decades of life experience.", "Ogive.xslx");
            FileMOQ_Empty = await MOQFile("", "Theurgy.pdf");
        }


        [TearDown]
        public void TearDown() {

        }
    }
}
