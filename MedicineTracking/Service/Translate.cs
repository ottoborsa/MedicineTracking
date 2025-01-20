
using MedicineTracking.Messaging;

namespace MedicineTracking.Service
{
    internal static class Translate
    {
        public static string Get(Translation translationObject)
        {
            return translationObject.TranslationKey;
        }
    }
}
