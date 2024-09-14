using Newtonsoft.Json;

namespace DotNet8WebApi.TwilioExample.Extensions
{
    public static class DevCode
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

        public static T ToObject<T>(this string jsonStr) => JsonConvert.DeserializeObject<T>(jsonStr)!;
    }
}
