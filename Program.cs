using JiJiBotApp_Backend.Extensions;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        // Configure JWT Authentication
        //builder.Services.AddJwtAuthentication(builder.Configuration);

        // Register dependencies
        builder.Services.AddDataServices(builder.Configuration);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JiJiBotAPI v1"));
        }

        // Enable CORS
        app.UseCors("AllowAll");



        app.UseHttpsRedirection();
        app.UseAuthentication();   // <--- Important

       


        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}