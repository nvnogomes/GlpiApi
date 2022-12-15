using GLPIService.Common.Extensions;

namespace GLPIService.Application.Common.Dtos {
    
    public class FollowUpDto {

        public int Id { get; set; }
        public int Tickets_Id { get; set; }
        public DateTime Date { get; set; }
        public int Users_id { get; set; }

        private string RawContent = "content";
        public string Content {
            get {
                return RawContent.RemoveHtml();
            }
            set {
                RawContent = value;
            }
        }
        public bool Is_Private { get; set; }

        public UserDto? User { get; set; } 
        public List<LinksDto> Links { get; set; } = new List<LinksDto>();

    }
}
