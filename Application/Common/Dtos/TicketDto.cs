namespace GLPIService.Application.Common.Dtos {

    public class TicketDto {

        public int Id { get; set; }

        public string Name { get; set; } = "";

        public DateTime Date { get; set; }

        public DateTime? CloseDate { get; set; }

        public DateTime? SolveDate { get; set; }

        public DateTime? Date_Mod { get; set; }

        public int Status { get; set; }

        public string Content { get; set; } = "";

        public int Urgency { get; set; }

        public int Impact { get; set; }

        public int Priority { get; set; }

        public int Type { get; set; }

        public int Users_id_lastupdater { get; set; }

        public int Is_Deleted { get; set; }

        public DateTime Date_Creation { get; set; }

        public List<LinksDto> Links { get; set; } = new List<LinksDto>();

    }
}
