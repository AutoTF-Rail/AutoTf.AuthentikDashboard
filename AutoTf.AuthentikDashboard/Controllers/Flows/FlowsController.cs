using System.ComponentModel.DataAnnotations;
using AutoTf.AuthentikDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoTf.AuthentikDashboard.Controllers.Flows;

[ApiController]
[Route("/flows")]
public class FlowsController : AuthentikController
{
    public FlowsController(UserManager userManager) : base(userManager) { }

    [HttpGet("-/configure/{flowId}")]
    public ActionResult RedirectToFlow(string flowId)
    {
        return RedirectPermanent(Statics.AuthUrl + $"/flows/-/configure/{flowId}/");
    }

    [HttpGet("executor/{flowName}")]
    public async Task<ActionResult<string>> ExecuteFlow(string flowName, [FromQuery] string query = "")
    {
        return await HttpHelper.SendGetString($"/api/v3/flows/executor/{flowName}/?query={query}");
    }

    [HttpGet("-/default/{slug}")]
    public IActionResult RedirectToDefault(string slug, [FromQuery] string next)
    {
        return RedirectPermanent(Statics.AuthUrl + $"/flows/-/default/{slug}/?next={next}/");
    }
}