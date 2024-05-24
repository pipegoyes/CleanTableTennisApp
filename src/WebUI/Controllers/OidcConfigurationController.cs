using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class OidcConfigurationController : Controller
{
    private readonly ILogger<OidcConfigurationController> _logger;

    public OidcConfigurationController(ILogger<OidcConfigurationController> logger)
    {
        _logger = logger;
    }

    [HttpGet("_configuration/{clientId}")]
    public IActionResult GetClientRequestParameters([FromRoute] string clientId)
    {
        // todo to be remove?
        //var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
        return Ok();
    }
}
