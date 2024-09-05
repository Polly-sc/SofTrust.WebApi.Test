namespace SofTrust.WebApi.Test.Services;

public interface IMessageRepository
{
    Task SaveMessageAsync(string message, int messageThemeId, int contactId);
    Task<int> GetMessageThemeIdAsync(string theme);
}
