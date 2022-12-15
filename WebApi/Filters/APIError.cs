using System.Collections.Generic;

namespace GLPIService.Filters {
    public class APIError {

        public string Message { get; set; }
        public IDictionary<string, string[]> ValidationErrors { get; set; }


        public APIError() {
        }

        public APIError(string message) {
            Message = message;
        }

        public APIError(IDictionary<string, string[]> errorList) {
            Message = "Please correct the specified validation errors and try again.";
            ValidationErrors = errorList;
        }

    }
}
