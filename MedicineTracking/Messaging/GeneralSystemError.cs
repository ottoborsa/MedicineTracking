
using System.Collections.Generic;
using System;

using MedicineTracking.Messaging.Const;

namespace MedicineTracking.Messaging
{
    using Properties = Dictionary<string, string?>;

    public static class GeneralSystemError
    {

        public static SerializedException Exception(string errorType)
        {
            return GetResult(errorType, null, null);
        }

        public static SerializedException Exception(string errorType, Exception exception)
        {
            return GetResult(errorType, exception, null);
        }

        public static SerializedException Exception(string errorType, Exception? exception, Properties properties)
        {
            return GetResult(errorType, exception, properties);
        }



        private static SerializedException GetResult(string errorType, Exception? exception, Properties? properties)
        {
            Properties? resultProperties = null;

            if (exception != null || properties != null)
            {
                resultProperties = new();

                if (exception != null)
                {
                    resultProperties.Add(Keys.ErrorMessage, exception.Message);
                }
                if (properties != null && properties.Count > 0)
                {
                    foreach (KeyValuePair<string, string?> property in properties)
                    {
                        resultProperties.Add(property.Key, property.Value);
                    }
                }
            }

            return new SerializedException(
                new Translation(nameof(GeneralSystemError), new() { { Keys.ErrorType, $"ErrorType.{errorType}" } }), null, null, resultProperties
            );
        }
    }
}
