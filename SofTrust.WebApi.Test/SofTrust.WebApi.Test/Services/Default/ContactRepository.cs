using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SofTrust.WebApi.Test.Models;

namespace SofTrust.WebApi.Test.Services.Default;

public class ContactRepository : IContactRepository
{
    private readonly AppOptions options;

    public ContactRepository(IOptions<AppOptions> options)
    {
        this.options = options.Value;
    }

    public async Task<int> GetContactIdAsync(Contact contact)
    {
        using (IDbConnection db = new SqlConnection(options.ConnectionString))
        {
            // Поиск существующего контакта
            var existingContact = await db.QueryFirstOrDefaultAsync<int>(@"
                SELECT Id
                FROM Contact
                WHERE Email = @Email AND Phone = @Phone", new { Email = contact.Email, Phone = contact.Phone });

            if (existingContact != 0)
            {
                return existingContact; // Контакт найден, возвращаем его ID
            }

            // Создание нового контакта
            var newContactId = db.QueryFirstOrDefault<int>(@"
                INSERT INTO Contacts (Name, Email, Phone)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Email, @Phone)",
                new { Email = contact.Email, Phone = contact.Phone, Name = contact.Name });

            return newContactId;
        }
    }
}
