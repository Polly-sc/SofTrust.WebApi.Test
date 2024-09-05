using Microsoft.AspNetCore.Mvc;
using SofTrust.WebApi.Test.Requests;
using SofTrust.WebApi.Test.Services;

namespace SofTrust.WebApi.Test.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormController : ControllerBase
{
    private readonly IRequestService requestService;
    public FormController(IRequestService requestService)
    {
        this.requestService = requestService;
    }

    [HttpPost]
    public async Task<IActionResult> PostReqeustAsync([FromBody]FormRequest formRequest)
    {
        await requestService.ProcessRequestAsync(formRequest);
        return Ok();
    }
}
