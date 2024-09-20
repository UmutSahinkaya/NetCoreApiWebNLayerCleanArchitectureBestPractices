namespace CleanApp.API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "CleanApp.API", Version = "v1" });
            });

            return services;
        }
        public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","CleanApp.API v1"));

            return app;
        }
    }
}
