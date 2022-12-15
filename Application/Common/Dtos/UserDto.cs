namespace GLPIService.Application.Common.Dtos {

    public class UserDto {

        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Realname { get; set; } = "";

        public string Firstname { get; set; } = "";

        public bool Is_active { get; set; }

        public DateTime Last_login { get; set; }


    }
}
