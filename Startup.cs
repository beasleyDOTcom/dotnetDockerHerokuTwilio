using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
      //"launchUrl": "{Scheme}://{ServiceHost}:{ServicePort}/weatherforecast", I changed this to sms in launch settings in properties dir for "docker profile" (command is docker)
namespace dotnetDockerHerokuTwilio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddDbContext
            // configure services allows you to register implementations of services.. say you want all your containers (files) to have access to a database --> this will register that dependency here and inject it into each of those for everyone downstream 
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            { 
    // request will inspect all the endpoints registered {controller}/{action}/{id} (using mvc)
                // this registers each endpoint url.com/food/details/3 so it knows where to send the request when it comes in (you can also set a default)
                // all the middleware after this will know what endpoint is being used     
                // around 32 min in https://www.youtube.com/watch?v=Pi46L7UYP8I
                // last database connection section is 1hr 10 min. 
                endpoints.MapControllers();
            });
        }
    }
}
