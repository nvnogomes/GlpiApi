namespace GLPIService.Application.Common.Dtos {

    public class SessionTokenDto {

        public string Session_token {
            set {
                Token = value;
            }
        }

        public string Token { get; private set; } = "";
    }
}
