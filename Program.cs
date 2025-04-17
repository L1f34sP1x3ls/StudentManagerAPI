// Create a WebApplication builder
using StudentManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controller support to the app
builder.Services.AddControllers();

// Register StudentService for dependency injection
// "AddSingleton" means a single instance is used throughout the application's lifetime
builder.Services.AddSingleton<StudentService>();

// Add Swagger for API documentation/testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application
var app = builder.Build();

// Enable Swagger UI in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Enable authorization if needed (not used in this basic example)
app.UseAuthorization();

// Map controller endpoints (e.g., api/students)
app.MapControllers();

// Run the application
app.Run();
