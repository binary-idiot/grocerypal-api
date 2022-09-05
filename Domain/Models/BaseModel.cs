using System.Text.Json.Serialization;
using GroceryPalAPI.Modules.Shared;

namespace GroceryPalAPI.Domain
{
    public abstract class BaseModel
    {
        [JsonConverter(typeof(Base64EncodedGuidConverter))]
        public Guid Id { get; set; }
    }
}
