
using Catalog.API.Data;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opt => {
    opt.Connection(builder.Configuration.GetConnectionString("DatabaseCon")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    //Seeding of data with the help of Marten
    builder.Services.InitializeMartenWith<CatalogInitialData>(); 
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("DatabaseCon"));
var app = builder.Build();

//Configure the HTTP request pipeline
app.UseExceptionHandler(options => { });
app.MapCarter();
app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
