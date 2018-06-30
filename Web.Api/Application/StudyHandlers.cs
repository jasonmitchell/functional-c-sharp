using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Web.Api.Application
{
    public class StudyHandlers
    {
        private readonly ILogger<StudyHandlers> _logger;

        public StudyHandlers(ILogger<StudyHandlers> logger)
        {
            _logger = logger;
        }

        public Task Handle(StartStudy command)
        {
            _logger.LogInformation("Handling command {CommandName}", nameof(StartStudy));
            return Task.CompletedTask;
        }

        public Task Handle(IncludeSlideInStudy command)
        {
            _logger.LogInformation("Handling command {CommandName}", nameof(IncludeSlideInStudy));
            return Task.CompletedTask;
        }
    }
}