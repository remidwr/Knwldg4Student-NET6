using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilogConfiguration(configuration);

// Add services to the container.
builder.Services.AddCorsConfiguration();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddCustomIntegrations();
builder.Services.AddApplication(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddMvcConfiguration();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();