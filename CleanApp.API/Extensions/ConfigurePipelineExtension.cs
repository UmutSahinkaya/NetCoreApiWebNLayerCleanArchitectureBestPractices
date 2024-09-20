namespace CleanApp.API.Extensions
{
    public static class ConfigurePipelineExtension
    {
        public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app)
        {
            app.UseExceptionHandler(x => { });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExt();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            return app;
        }
    }
}
