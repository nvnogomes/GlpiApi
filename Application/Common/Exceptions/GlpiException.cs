namespace GLPIService.Application.Common.Exceptions {
    public class GlpiException : Exception {

        public GlpiException()
            : base() {
        }

        public GlpiException(string message)
            : base(message) {
        }

        public GlpiException(string message, Exception innerException)
            : base(message, innerException) {
        }

    }
}
