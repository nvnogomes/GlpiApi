using GLPIService.Common.Extensions;

namespace GLPIService.Infrastructure.Services.Glpi {

    public class GlpiConfiguration {

        public string ApiUrl { get; set; } = "";
        public string WsUsername { get; set; } = "";
        public string WsPassword { get; set; } = "";
        public string AppToken { get; set; } = "";
        public string SessionToken { get; set; } = "";

        public string UserToken => $"{WsUsername}:{WsPassword}".ToBase64();
    }
}
