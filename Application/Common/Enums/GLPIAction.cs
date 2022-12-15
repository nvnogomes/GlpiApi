namespace GLPIService.Application.Common.Enums {

    public class GLPIAction {
        public string Value { get; private set; }

        private GLPIAction(string value) { Value = value; }


        public static GLPIAction InitSession { get { return new GLPIAction("initSession"); } }
        public static GLPIAction Document { get { return new GLPIAction("Document"); } }
        public static GLPIAction Group { get { return new GLPIAction("group"); } }
        public static GLPIAction ITILSolution { get { return new GLPIAction("ITILSolution"); } }
        // 9.5.X version
        public static GLPIAction ITILFollowUp { get { return new GLPIAction("ITILFollowup"); } }
        public static GLPIAction KillSession { get { return new GLPIAction("killSession"); } }
        public static GLPIAction Ticket { get { return new GLPIAction("Ticket"); } }
        public static GLPIAction TicketFollowUp { get { return new GLPIAction("TicketFollowup"); } }
        public static GLPIAction User { get { return new GLPIAction("User"); } }


        public bool Equals(string action) {
            return Value == action;
        }

        public override bool Equals(object? obj) {
            return obj is not null
                   && obj is GLPIAction action
                   && Value == action.Value;
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        public override string? ToString() {
            return $"{Value}";
        }
    }
}
