using AutoMapper;
using MatchOdds.Api.Configuration;
using MatchOdds.Domain.AutoMapper;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy => policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();

//database 
builder.Services.AddDatabaseServices(builder.Configuration);

//Repositories
builder.Services.AddRepositoryServices();
//builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//Automapper
builder.Services.AddSingleton(p => new MapperConfiguration(config => config.AddMappings()).CreateMapper());

//Caching
builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Apply Migrations to database
app.UseDatabaseMigrate();

app.UseHttpsRedirection();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Match Odds API");
});

app.UseEndpoints(endpoints =>
{
    //endpoints.MapHealthChecks("/healthz");
    endpoints.MapControllers();
});

app.Run();


//https://www.c-sharpcorner.com/article/implement-unit-of-work-and-generic-repository-pattern-in-a-web-api-net-core-pro/
//https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application