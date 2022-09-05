using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GroceryPalAPI.Domain;

public class Base64EncodedGuidConverter : JsonConverter<Guid>
{
	public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (Guid.TryParse(
			    Encoding.UTF8.GetString(reader.GetBytesFromBase64()),
			    out Guid guidResult
			))
		{
			return guidResult;
		}
		
		throw new JsonException();
	}

	public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
	{
		writer.WriteBase64StringValue(Encoding.UTF8.GetBytes(value.ToString()));
	}
}