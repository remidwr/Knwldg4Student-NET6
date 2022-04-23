using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilogConfiguration(configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();