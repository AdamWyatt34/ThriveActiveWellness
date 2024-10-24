namespace ThriveActiveWellness.UI.Models.ApiModels;

public class ResultModel
{
    public bool IsSuccess { get; set; }
    public ErrorModel Error { get; set; }
}

public class ResultModel<TValue> : ResultModel
{
    public TValue Value { get; set; }
}

public class ErrorModel
{
    public string Code { get; set; }
    public string Name { get; set; }
}
