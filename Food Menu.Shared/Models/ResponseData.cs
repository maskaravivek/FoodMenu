using Food_Menu.Models.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Models
{
    public class ResponseData
    {
        public ResponseData()
        {

        }

        public ResponseData(string type)
        {
            ResponseType = type;
        }

        public ResponseData(string type, ErrorResponse errorMessage)
        {
            ResponseType = type;
            Payload = JObject.FromObject(errorMessage);
        }

        [JsonProperty("responseType")]
        public string ResponseType { get; set; }

        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public JObject Payload { get; set; }

        public static ResponseData GetNoInternetResponse()
        {
            return new ResponseData(Constants.ErrorString, new ErrorResponse("NetworkUnavailable", "Unable to connect to the internet"));
        }

    }

    public class ErrorResponse
    {
        [JsonProperty("errorType")]
        public string ErrorType { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        public ErrorResponse(string type, string message)
        {
            ErrorType = type;
            ErrorMessage = message;
        }
    }
}
