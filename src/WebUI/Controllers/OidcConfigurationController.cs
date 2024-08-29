using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class OidcConfigurationController : Controller
{
    [HttpGet("_configuration/{clientId}")]
    public IActionResult GetClientRequestParameters([FromRoute] string clientId)
    {
        // todo to be remove?
        //var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
        return Ok();
    }
}
