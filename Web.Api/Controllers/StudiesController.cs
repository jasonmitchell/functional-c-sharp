using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Application;

namespace Web.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class StudiesController : ControllerBase
    {
        private readonly CommandHandler<StartStudy> _startStudy;
        private readonly CommandHandler<IncludeSlideInStudy> _includeSlide;

        public StudiesController(CommandHandler<StartStudy> startStudy,
            CommandHandler<IncludeSlideInStudy> includeSlide)
        {
            _startStudy = startStudy;
            _includeSlide = includeSlide;
        }
        
        [HttpPost]
        public async Task<ActionResult> StartStudy(StartStudy command)
        {
            await _startStudy(command);
            return Created($"/studies/{command.Id}", null);
        }

        [HttpPost, Route("{id}/slides")]
        public async Task<ActionResult> IncludeSlide(IncludeSlideInStudy command)
        {
            await _includeSlide(command);
            return Created($"/studies/{command.StudyId}/slides/{command.SlideId}", null);
        }
    }
}