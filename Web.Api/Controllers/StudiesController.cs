using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Application;

namespace Web.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class StudiesController : ControllerBase
    {
        private readonly CommandBus _commandBus;

        public StudiesController(CommandBus commandBus)
        {
            _commandBus = commandBus;
        }
        
        [HttpPost]
        public async Task<ActionResult> StartStudy(StartStudy command)
        {
            await _commandBus.Send(command);
            return Created($"/studies/{command.Id}", null);
        }

        [HttpPost, Route("{id}/slides")]
        public async Task<ActionResult> IncludeSlide(IncludeSlideInStudy command)
        {
            await _commandBus.Send(command);
            return Created($"/studies/{command.StudyId}/slides/{command.SlideId}", null);
        }
    }
}