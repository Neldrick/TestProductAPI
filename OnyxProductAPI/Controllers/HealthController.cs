using Microsoft.AspNetCore.Mvc;

namespace OnyxProductAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class HealthController : ControllerBase
{
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
