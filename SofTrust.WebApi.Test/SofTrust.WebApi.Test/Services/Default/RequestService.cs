using System.Text.RegularExpressions;
using SofTrust.WebApi.Test.Requests;
using SofTrust.WebApi.Test.Models;
using System.Net.Mail;

namespace SofTrust.WebApi.Test.Services.Default;

public class RequestService : IRequestService
{
    private readonly IMessageRepository messageRepository;
    private readonly IContactRepository contactRepository;

    public RequestService(IMessageRepository messageRepository, IContactRepository contactRepository)
    {
        this.messageRepository = messageRepository;
        this.contactRepository = contactRepository;
    }

    public async Task ProcessRequestAsync(FormRequest formRequest)
    {
        if(!IsCorrectPhone(formRequest.Phone) || !IsCorrectEmail(formRequest.Email))
        {
             throw new Exception("Телефон или Email некорректен");
        }

        var contactId = await contactRepository.GetContactIdAsync(new Contact
        { 
            Name = formRequest.Name,
            Email = formRequest.Email,
            Phone = formRequest.Phone
        });
        
        var messageThemeId = await messageRepository.GetMessageThemeIdAsync(formRequest.MessageTheme);
        await messageRepository.SaveMessageAsync(formRequest.Message, messageThemeId, contactId);
    }

    private bool IsCorrectPhone(string phoneNumber)
    {
        string pattern = @"^(?:\+?7|8)?[\s\-]?\(?[0-9]{3}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    private bool IsCorrectEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
