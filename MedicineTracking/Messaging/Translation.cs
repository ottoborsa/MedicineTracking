
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

using MedicineTracking.Messaging.Const;

#nullable enable
namespace MedicineTracking.Messaging
{

    using Arguments = Dictionary<string, string?>;

    public class Translation
    {

        private const string UnknownErrorTranslationKey = "UnexpectedError";


        [JsonProperty(nameof(TranslationKey), NullValueHandling = NullValueHandling.Ignore)]
        public string TranslationKey { get; set; }

        [JsonProperty(nameof(Args), NullValueHandling = NullValueHandling.Ignore)]
        private readonly Arguments? Args;

        [JsonProperty(nameof(Messages), NullValueHandling = NullValueHandling.Ignore)]
        public List<Translation>? Messages { get; set; }



        public Translation(string translationKey, Arguments? arguments = null, List<Translation>? recursionObjectList = null)
        {
            TranslationKey = translationKey;
            Args = arguments;
            Messages = recursionObjectList;
        }





        public static Translation ParseException(Exception exception)
        {
            if (exception.GetType() == typeof(SerializedException))
            {
                return ParseException((SerializedException)exception);
            }
            else
            {
                return new Translation(
                    UnknownErrorTranslationKey,
                    new(){ { Keys.ErrorMessage, exception.Message } },
                    exception.InnerException != null ? new List<Translation>{ { ParseException(exception.InnerException) } } : null
                );
            }
        }

        private static Translation ParseException(SerializedException serializedException)
        {
            SystemError error = SystemError.ParseException(serializedException);

            return new Translation(
                error.Translation != null ? error.Translation.TranslationKey : UnknownErrorTranslationKey,
                error.Translation != null ? error.Translation.Args : new(){ { Keys.ErrorMessage, serializedException.Message } },
                null
            );
        }
    }
}
#nullable disable