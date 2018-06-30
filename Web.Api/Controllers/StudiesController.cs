using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Application;

namespace Web.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class StudiesController : ControllerBase
    {
        private readonly StudyHandlers _handlers;

        public StudiesController(StudyHandlers handlers)
        {
            _handlers = handlers;
        }
        
        [HttpPost]
        public async Task<ActionResult> StartStudy(StartStudy command)
        {
            await _handlers.Handle(command);
            return Created($"/studies/{command.Id}", null);
        }

        [HttpPost, Route("{id}/slides")]
        public async Task<ActionResult> IncludeSlide(IncludeSlideInStudy command)
        {
            await _handlers.Handle(command);
            return Created($"/studies/{command.StudyId}/slides/{command.SlideId}", null);
        }
    }
}