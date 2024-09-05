using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SofTrust.WebApi.Test.Models;
using System.Data;

namespace SofTrust.WebApi.Test.Services.Default;

public class MessageRepository : IMessageRepository
{
    private readonly AppOptions options;

    public MessageRepository(IOptions<AppOptions> options)
    {
        this.options = options.Value;
    }

    public async Task<int> GetMessageThemeIdAsync(string theme)
    {
        using (IDbConnection db = new SqlConnection(options.ConnectionString))
        {
            // Поиск существующей темы
            var existingThemeId = await db.QueryFirstOrDefaultAsync<int>(@"
                SELECT id
                FROM messageTheme
                WHERE theme = @theme
            ", new { theme });

            if (existingThemeId != 0)
            {
                return existingThemeId; // Тема найдена, возвращаем ее ID
            }

            // Создание новой темы
            var newThemeId = await db.QueryFirstOrDefaultAsync<int>(@"
                INSERT INTO messageTheme (theme)
                OUTPUT INSERTED.Id
                VALUES (@theme)
            ", new { theme });

            return newThemeId;
        }
    }

    public async Task SaveMessageAsync(string message, int messageThemeId, int contactId)
    {
        using (var db = new SqlConnection(options.ConnectionString))
        {
            await db.ExecuteAsync(@"
                INSERT INTO Message (message, id_theme, id_contact)
                OUTPUT INSERTED.Id
                VALUES (@message, @messageThemeId, @contactId)
            ", new { message, messageThemeId, contactId });
        }
    }
}
