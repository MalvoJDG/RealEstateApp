using Swashbuckle.AspNetCore.SwaggerUI;

namespace RealEstateApp.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurante Api");
                options.DefaultModelRendering(ModelRendering.Model);
            });

        }
    }
}
