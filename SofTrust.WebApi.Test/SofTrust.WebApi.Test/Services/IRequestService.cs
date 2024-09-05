using SofTrust.WebApi.Test.Requests;

namespace SofTrust.WebApi.Test.Services;

public interface IRequestService
{
    /// <summary>
    /// Обработать сообщение из формы обратной связи
    /// </summary>
    Task ProcessRequestAsync(FormRequest formRequest);
}
