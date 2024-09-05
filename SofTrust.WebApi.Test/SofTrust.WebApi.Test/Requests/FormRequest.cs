namespace SofTrust.WebApi.Test.Requests;

public class FormRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public string MessageTheme {get;set;} = "";
}
