using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// LỖI MÔI TRƯỜNG 1: Dùng Swagger cũ thay vì OpenAPI mới của .NET 10
using Microsoft.OpenApi.Models; 

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// LỖI MÔI TRƯỜNG 2: Quên đăng ký builder.Services.AddDbContext<AppDbContext> !!!

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
    // LỖI MÔI TRƯỜNG 3: Quên using Scalar.AspNetCore;
    // app.MapScalarApiReference(); 
}

app.MapControllers();
app.Run();

public partial class Program { }
