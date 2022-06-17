using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PlanningPoker.Controllers;

[ApiController]
public class ErrorHandlingController : Controller
{
    private readonly IHostEnvironment _environment;
    //private readonly ILogger<ErrorHandlingController> _logger;

    public ErrorHandlingController(IHostEnvironment environment, ILogger<ErrorHandlingController> logger)
    {
        _environment = environment;
        //_logger = logger;
    }
    
    //[ApiExplorerSettings(IgnoreApi = true)]  // remove this line in order to test the Throw method
    [HttpGet("Throw")]
    public IActionResult Throw() =>
        throw new Exception("Sample exception.");
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment()
    {
        if (!_environment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        
        //_logger.LogError("Unexpected error [{@Error}]", exceptionHandlerFeature.Error);

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult HandleError() =>
        Problem();
}