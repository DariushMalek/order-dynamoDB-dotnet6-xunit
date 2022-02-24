using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TechLand.Shop.Test.IntegrationTest;

public static class Extensions
{
    public static string ToJsonString<T>(this T @object)
    {
        if (@object == null)
        {
            throw new ArgumentNullException(nameof(@object));
        }

        return JsonConvert.SerializeObject(
            @object,
            Formatting.None,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
    }

    public static T FromJsonString<T>(this string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentNullException(nameof(json));
        }

        return JsonConvert.DeserializeObject<T>(json);
    }

    public static async Task<T> ReadAsJson<T>(this HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        return FromJsonString<T>(json);
    }

    public static HttpRequestMessage Configure(
        this HttpRequestMessage request,
        string acceptValue = "application/json"
    )
    {
        request.Headers.Add("Accept", acceptValue);
        return request;
    }

    public static HttpRequestMessage WithJsonBody(this HttpRequestMessage request, object payload)
    {
        request.Content = new StringContent(payload.ToJsonString(), Encoding.UTF8, "application/json");
        return request;
    }
}