using Capital_Placement_Test.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Capital_Placement_Test.Filters
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = ApiConstants.API_KEY_HEADER,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "string" },
                Required = true,
                Description = "Service authorization key"
            });
        }
    }
}