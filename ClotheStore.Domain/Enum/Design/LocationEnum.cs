using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ClotheStore.Domain.Enum.Design
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Location
    {
        [EnumMember(Value = "front")]
        front,

        [EnumMember(Value = "back")]
        back
    }
}
