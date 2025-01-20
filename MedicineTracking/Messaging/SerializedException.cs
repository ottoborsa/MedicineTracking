
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

#nullable enable
namespace MedicineTracking.Messaging
{
    using Properties = Dictionary<string, string?>;


    [Serializable]
    public class SerializedException : Exception
    {
        private const string DefaultUnknownExceptionMessage = "Unknown error";


        public SerializedException(string errorMessage, int? httpStatusCode = null, string? errorCode = null, Properties? properties = null)
            : base(JsonConvert.SerializeObject(new SystemError(errorMessage, httpStatusCode, errorCode, properties)))
        {
        }

        public SerializedException(Translation translation, int? httpStatusCode = null, string? errorCode = null, Properties? properties = null)
            : base(JsonConvert.SerializeObject(new SystemError(translation, httpStatusCode, errorCode, properties)))
        {
        }

        public SerializedException(Translation translation, Properties properties)
            : base(JsonConvert.SerializeObject(new SystemError(translation, null, null, properties)))
        {
        }

        public SerializedException(string errorMessage, Properties properties)
            : base(JsonConvert.SerializeObject(new SystemError(errorMessage, null, null, properties)))
        {
        }




        public static SerializedException Parse(Exception exception)
        {
            if (exception.GetType() == typeof(SerializedException))
            {
                return (SerializedException)exception;
            }

            try
            {
                if (exception.InnerException != null)
                {
                    return Parse(new Exception(exception.ToString()));
                }
                else
                {
                    return Cast(exception);
                }
            }
            catch
            {
                return Cast(exception);
            }
        }

        public static SerializedException Cast(Exception exception)
        {
            if (exception.GetType() == typeof(SerializedException))
            {
                return (SerializedException)exception;
            }

            SystemError error = SystemError.ParseException(exception);

            return new SerializedException(
                error.ErrorMessage ?? DefaultUnknownExceptionMessage,
                SystemError.GetStatusCode(error.StatusCode),
                SystemError.GetErrorCode(error.ErrorCode),
                error.Properties
            );
        }
    }
}
#nullable disable