using WebAPI.Authentication;
using WebAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// We will use controllers to respond to requests
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure authentication to be used
builder.Services.AddAuthentication(AuthentucationConfigurer.Configure);

// app dependencies and services defined and configured, now let's arrange the pipeline before application starts.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add authentication handlers to pipeline
app.UseAuthentication();
// Authenticate and authorize requests
app.UseAuthorization();

// Map the http requests to the controllers of the application.
// Routing uses the defaults
app.MapControllers();

// Pipeline arranged. Let's start!
app.Run();