using Ceyehat.Application;
using Ceyehat.Infrastructure;
using ceyehat.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    // Problem Details response approach
    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddTransient<CeyehatExceptionHandlingMiddleware>();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Middleware approach for exception handling
    //app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<CeyehatExceptionHandlingMiddleware>();

    app.MapControllers();

    app.Run();
}