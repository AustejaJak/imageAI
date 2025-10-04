using DotNetEnv;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using serviceAI;

var builder = WebApplication.CreateBuilder(args);

Env.Load(@"C:\Users\auste\RiderProjects\imageAI\serviceAI\.env");

var key = Environment.GetEnvironmentVariable("VISION_KEY");
var endpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT");

builder.Services.AddSingleton(new VisionService(endpoint, key));
builder.Services.AddControllers();  

var app = builder.Build();
app.MapControllers();
app.Run();