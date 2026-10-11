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

public abstract class ProductQuestionDto
{
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
