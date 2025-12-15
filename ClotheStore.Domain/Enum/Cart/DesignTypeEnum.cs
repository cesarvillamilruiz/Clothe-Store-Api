using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ClotheStore.Domain.Enum.Cart
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CustomizationType
    {
        [EnumMember(Value = "text")]
        Text,

        [EnumMember(Value = "image")]
        Image
    }
}
