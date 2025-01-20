
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System;

using MedicineTracking.Messaging.Const;

#nullable enable
namespace MedicineTracking.Messaging
{
    using Properties = Dictionary<string, string?>;


    public class SystemError
    {

        private const string DefaultUnknownErrorCode = "unknown_error_code";

        private const int DefaultServerHttpStatusCode = (int)HttpStatusCode.InternalServerError;



        [JsonProperty(nameof(StatusCode), NullValueHandling = NullValueHandling.Ignore)]
        public int StatusCode { get; set; }

        [JsonProperty(nameof(ErrorCode), NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrorCode { get; set; }

        [JsonProperty(nameof(ErrorMessage), NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrorMessage { get; set; }

        [JsonProperty(nameof(Translation), NullValueHandling = NullValueHandling.Ignore)]
        public Translation? Translation { get; set; }

        [JsonProperty(nameof(Properties), NullValueHandling = NullValueHandling.Ignore)]
        public Properties? Properties { get; set; }



        public SystemError(string errorMessage, int? httpStatusCode, string? errorCode, Properties? properties)
        {
            StatusCode = GetStatusCode(httpStatusCode);
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Translation = null;
            Properties = properties;
        }

        public SystemError(Translation translation, int? httpStatusCode, string? errorCode, Properties? properties)
        {
            StatusCode = GetStatusCode(httpStatusCode);
            ErrorCode = errorCode;
            ErrorMessage = null;
            Translation = translation;
            Properties = properties;
        }

        [JsonConstructor]
        public SystemError(int? statusCode, string? errorCode, string? errorMessage, Translation? translation, Properties? properties)
        {
            StatusCode = GetStatusCode(statusCode);
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Translation = translation;
            Properties = properties;
        }




        public static SystemError ParseException(SerializedException serializedException)
        {
            return ParseException((Exception)serializedException);
        }

        public static SystemError ParseException(Exception exception)
        {
            try
            {
                SystemError? error = JsonConvert.DeserializeObject<SystemError>(exception.Message);

                return error != null
                    ? new SystemError(GetStatusCode(error.StatusCode), GetErrorCode(error.ErrorCode), exception.Message, error.Translation, error.Properties)
                    : GetUnknownError(exception);
            }
            catch
            {
                return GetUnknownError(exception);
            }
        }



        private static SystemError GetUnknownError(Exception exception)
        {
            return new SystemError(GetStatusCode(), GetErrorCode(), exception.Message, null, null);
        }

        public static int GetStatusCode(int? httpStatusCode = null)
        {
            if (httpStatusCode != null && (httpStatusCode < 100 || httpStatusCode > 599))
            {
                throw new SerializedException(new Translation("SystemError.InvalidStatusCode", new() { { Keys.StatusCode, httpStatusCode.ToString() } }));
            }

            return httpStatusCode ?? DefaultServerHttpStatusCode;
        }

        public static string GetErrorCode(string? errorCode = null)
        {
            return errorCode ?? DefaultUnknownErrorCode;
        }
    }
}
#nullable disable