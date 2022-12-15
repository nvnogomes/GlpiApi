namespace GLPIService.Application.Common.Dtos {

    public class DocumentDto {

        public int Id { get; set; } = 0;

        public string Filename { get; set; } = "";

        public string Mime { get; set; } = "";

        public DateTime Date_Creation { get; set; }

        public int Users_Id { get; set; }

        public UserDto User { get; set; } = new();

        public List<LinksDto> Links { get; set; } = new();


    }
}
