namespace GLPIService.Application.Common.Enums {

    public class UserType {

        public string Label { get; private set; }
        public int Value { get; private set; }


        private UserType(int value, string label) {
            Value = value;
            Label = label;
        }


        public static UserType Requester { get { return new UserType(1, "Requester"); } }
        public static UserType AssignTo { get { return new UserType(2, "Assigned To"); } }
        public static UserType Watcher { get { return new UserType(3, "Watcher"); } }


        public static UserType GetByValue(int value) {
            return value switch {
                1 => Requester,
                2 => AssignTo,
                3 => Watcher,
                _ => throw new ArgumentException("Invalid argument")
            };
        }


    }
}
