using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Application;

namespace Web.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<StudyHandlers>();

            services.AddSingleton(s =>
            {
                var commandBus = new CommandBus();

                var studyHandlers = s.GetRequiredService<StudyHandlers>();
                commandBus.RegisterHandler<StartStudy>(studyHandlers.Handle);
                commandBus.RegisterHandler<IncludeSlideInStudy>(c => studyHandlers.Handle(c, new SlideStore()));

                return commandBus;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
