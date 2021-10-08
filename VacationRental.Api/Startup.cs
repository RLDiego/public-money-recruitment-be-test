using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using VacationRental.Api.Properties;
using VacationRental.Domain.Booking;
using VacationRental.Domain.Calendar;
using VacationRental.Domain.DTO.Booking;
using VacationRental.Domain.DTO.Rental;
using VacationRental.Domain.Rentals;
using VacationRental.Persistence;

namespace VacationRental.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigureSwagger(services);

            AddDependencies(services);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                                    new Info
                                    {
                                        Title = SwaggerConfiguration.DocInfoTitle,
                                        Version = SwaggerConfiguration.DocInfoVersion,
                                        Description = SwaggerConfiguration.DocInfoDescription,
                                        Contact = new Contact()
                                        {
                                            Name = SwaggerConfiguration.ContactName,
                                            Url = SwaggerConfiguration.Contact
                                        }
                                    });
            }
            );
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription));
        }

        private static void AddDependencies(IServiceCollection services)
        {
            services.AddSingleton<IDictionary<int, RentalViewModel>>(new Dictionary<int, RentalViewModel>());
            services.AddSingleton<IDictionary<int, BookingViewModel>>(new Dictionary<int, BookingViewModel>());
            services.AddSingleton<IBookingService, BookingService>();
            services.AddSingleton<ICalendarService, CalendarService>();
            services.AddSingleton<IRentalService, RentalService>();
            services.AddSingleton<IBookingRepository, BookingRepository>();
            services.AddSingleton<IRentalRepository, RentalRepository>();
        }
    }
}
