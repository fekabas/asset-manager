using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// We will use controllers to respond to requests
builder.Services.AddControllers();

// Add services to the container.

// Configure WebAPI and it's dependencies
builder.Services.ConfigureWebAPI(builder.Configuration, builder.Environment);

// app dependencies and services defined and configured, now let's arrange the pipeline before application starts.
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Add authentication handlers to pipeline
app.UseAuthentication();
// Authenticate and authorize requests
app.UseAuthorization();

// Map the http requests to the controllers of the application.
// Routing uses the defaults
app.MapControllers();

// Pipeline arranged. Let's start!
app.Run();