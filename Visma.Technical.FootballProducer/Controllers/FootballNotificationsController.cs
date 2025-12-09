using Microsoft.AspNetCore.Mvc;
using Visma.Technical.Core.Features;
using Visma.Technical.Core.Features.ProcessFootballEvent;

namespace Visma.Technical.FootballProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballNotificationsController(IProcessFootballEvent processFootballEvent) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostNotification(EventInput input)
        {
            await processFootballEvent.ProcessEventAsync(input);
            return Ok();
        }
    }
}
