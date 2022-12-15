namespace GLPIService.Application.Common.Exceptions {
    public class NotFoundException : Exception {
        public NotFoundException()
            : base() {
        }

        public NotFoundException(string message)
            : base(message) {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) {
        }

        public NotFoundException(string name, object key)
            : base($@"Entity <{name}> Id({key}) was not found.") {
        }

        public NotFoundException(string name, object key1, object key2)
            : base($@"Entity <{name}> Id({key1},{key2}) was not found.") {
        }
    }
}
