using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ClotheStore.Domain.Enum.Design
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Location
    {
        [EnumMember(Value = "front")]
        Front,

        [EnumMember(Value = "back")]
        Back
    }
}
