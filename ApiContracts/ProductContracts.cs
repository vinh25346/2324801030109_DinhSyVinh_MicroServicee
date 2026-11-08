using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProductService.Api.Queries.Dtos;

public class ProductDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<ProductCoverDto> Covers { get; set; } = new();
    public List<ProductQuestionDto> Questions { get; set; } = new();
}

public class ProductCoverDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal SumInsured { get; set; }
}

[JsonConverter(typeof(ProductQuestionDtoConverter))]
public abstract class ProductQuestionDto
{
    public int Index { get; set; }
    public string QuestionCode { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

public sealed class ChoiceQuestionDto : ProductQuestionDto
{
    public List<ChoiceDto> Choices { get; set; } = new();
}

public sealed class NumericQuestionDto : ProductQuestionDto
{
}

public class ChoiceDto
{
    public string Code { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}

public sealed class ProductQuestionDtoConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(ProductQuestionDto);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var json = JObject.Load(reader);
        ProductQuestionDto question = json.Value<string>("questionType") switch
        {
            "Choice" => new ChoiceQuestionDto(),
            "Numeric" => new NumericQuestionDto(),
            _ => throw new JsonSerializationException("Unsupported product question type.")
        };

        serializer.Populate(json.CreateReader(), question);
        return question;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
