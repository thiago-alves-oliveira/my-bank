using IOBBank.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace IOBBank.Core.Mvc;

public abstract class BaseController : Controller
{
    protected IActionResult HandleResult(IMediatorResult result)
    {
        if (result.Exception is not null)
        {            
            return BadRequest(result);         
        }

        return result.IsValid
            ? Ok(result)
            : BadRequest(new { result.IsValid, result.Errors });
    }
}