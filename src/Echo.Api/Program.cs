using Echo.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.Services.AddRin(options =>
{
    options.RequestRecorder.AllowRunningOnProduction = true;
    options.RequestRecorder.EnableBodyCapturing = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRin();
app.UseRinDiagnosticsHandler();

app.MapPost(
        "api/v1/echo",
        (object body, HttpContext context, IConfiguration configuration) =>
        {
            var sections = configuration.GetChildren().Where(c => c.Key.StartsWith("ECHOAPI_", StringComparison.OrdinalIgnoreCase));
            foreach (var section in sections)
            {
                context.Response.Headers.Add(section.Key.NormalizeToHttpHeaderName(), section.Value);
            }

            return Results.Ok(body);
        })
    .WithName("Echo")
    .WithDescription("Echoes the request body")
    .Accepts(typeof(object), "application/json")
    .Produces(200)
    .WithOpenApi();

app.Run();
