using SofTrust.WebApi.Test.Models;

namespace SofTrust.WebApi.Test.Services;

public interface IContactRepository
{
    Task<int> GetContactIdAsync(Contact contact);
}
