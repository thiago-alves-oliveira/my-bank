using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace IOBBank.Api.Extensions;

public static class JwtExtensions
{
    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}
