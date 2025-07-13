using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Bookstore.Controllers;
[Route("api/[controller]")]
[ApiController]
public abstract class APIControllerBase : ControllerBase
{
    protected string? GetControllerRoute()
    {
        var cad = ControllerContext.ActionDescriptor;
        string? baseRoute = cad.AttributeRouteInfo?.Template;

        return baseRoute;
    }
}