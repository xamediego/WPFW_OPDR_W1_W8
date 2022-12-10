using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WebApplication2.Models;


//lel name
public class GenderConverter : JsonConverter<Gender>
{
    public override Gender ReadJson(JsonReader reader, Type objectType, Gender existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var token = reader.Value as string ?? reader.Value.ToString();
        var stripped = Regex.Replace(token, @"<[^>]+>", string.Empty);
        if (Enum.TryParse<Gender>(stripped, out var result))
        {
            return result;
        }
        return default(Gender);
    }

    public override void WriteJson(JsonWriter writer, Gender value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }  
}