namespace EntegroConnect.Common.Models;

public class BaseResponse
{
    public bool Success { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;
}

public class BaseDataResponse : BaseResponse
{
    public object Data { get; set; } = new();
}

public class NarVarApiResponse
{
    public string Status { get; set; } = string.Empty;
    public List<NarVarResponseMessage> Messages { get; set; } = new();
}

public class NarVarResponseMessage
{
    public string Level { get; set; } = string.Empty;
    public string Field { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}