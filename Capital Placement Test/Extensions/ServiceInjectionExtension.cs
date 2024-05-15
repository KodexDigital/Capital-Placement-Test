using Capital_Placement_Test.Filters;
using Capital_Placement_Test.Services;
using Capital_Placement_Test.Services.Implementations;
using Capital_Placement_Test.Services.Interfaces;
using Capital_Placement_Test.Settings;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Capital_Placement_Test.ServiceInjections
{
    public static class ServiceInjectionExtension
    {
        public static IServiceCollection InjectService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HeaderAuthentication>(configuration.GetSection(nameof(HeaderAuthentication)));
            services.Configure<AzureCosmosDbConnectionSettgings>(configuration.GetSection(nameof(AzureCosmosDbConnectionSettgings)));

            SwaggerInjection(services);
            services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.TryAddScoped<IProgramService, ProgramService>();
            services.TryAddScoped<IQuestionService, QuestionService>();
            services.TryAddScoped<IApplicationFormService, ApplicationFormService>();
            services.TryAddScoped<IDatabaseConnection, DatabaseConnection>();
            services.TryAddScoped<IUserQuestionAndAnswerService, UserQuestionAndAnswerService>();
            services.TryAddScoped(typeof(ApiAuthenticationHeaderFilter));

            return services;
        }

        private static void SwaggerInjection(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Capital Placement User Application Service",
                    Version = "v1",
                    Description = "Service that is backed by Azure Cosmos Database"
                });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "capital_placement_service.xml"), true);
                c.OperationFilter<SwaggerHeaderFilter>();
            });
        }
    }
}