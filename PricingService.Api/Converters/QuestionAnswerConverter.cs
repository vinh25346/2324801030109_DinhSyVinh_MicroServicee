using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PricingService.Api.Commands.Dto;

namespace PricingService.Api.Converters;

internal class QuestionAnswerConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType.IsAssignableFrom(typeof(QuestionAnswer));

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var target = Create(jsonObject);
        serializer.Populate(jsonObject.CreateReader(), target);
        return target;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is not QuestionAnswer questionAnswer)
            return;

        writer.WriteStartObject();
        writer.WritePropertyName("questionCode");
        serializer.Serialize(writer, questionAnswer.QuestionCode);
        writer.WritePropertyName("questionType");
        serializer.Serialize(writer, questionAnswer.QuestionType);
        writer.WritePropertyName("answer");
        serializer.Serialize(writer, questionAnswer.GetAnswer());
        writer.WriteEndObject();
    }

    private static QuestionAnswer Create(JObject jsonObject)
    {
        var typeName = Enum.Parse<QuestionType>(jsonObject["questionType"]!.ToString());
        return typeName switch
        {
            QuestionType.Text => new TextQuestionAnswer
            {
                QuestionCode = jsonObject["questionCode"]!.ToString(),
                Answer = jsonObject["answer"]!.ToString()
            },
            QuestionType.Number => new NumericQuestionAnswer
            {
                QuestionCode = jsonObject["questionCode"]!.ToString(),
                Answer = jsonObject["answer"]!.Value<decimal>()
            },
            QuestionType.Choice => new ChoiceQuestionAnswer
            {
                QuestionCode = jsonObject["questionCode"]!.ToString(),
                Answer = jsonObject["answer"]!.ToString()
            },
            _ => throw new ApplicationException($"Unexpected question type {typeName}")
        };
    }
}
