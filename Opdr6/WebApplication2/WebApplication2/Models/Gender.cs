using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApplication2.Models;

[JsonConverter(typeof(GenderConverter))]
public enum Gender
{
    Man,
    Vrouw,
    Anders,
    Geheim
}