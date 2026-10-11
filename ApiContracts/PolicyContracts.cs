namespace PolicyService.Api.Commands.Dtos;

public class PersonDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
}

public class AddressDto
{
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
}

public enum QuestionType
{
    Choice,
    Numeric
}

public class QuestionAnswer
{
    public string QuestionCode { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
}

public sealed class ChoiceQuestionAnswer : QuestionAnswer
{
    public ChoiceQuestionAnswer()
    {
        QuestionType = QuestionType.Choice;
    }

    public string Answer { get; set; } = string.Empty;
}

public sealed class NumericQuestionAnswer : QuestionAnswer
{
    public NumericQuestionAnswer()
    {
        QuestionType = QuestionType.Numeric;
    }

    public decimal Answer { get; set; }
}
