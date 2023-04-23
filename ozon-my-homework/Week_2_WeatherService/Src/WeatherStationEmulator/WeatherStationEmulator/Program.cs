using WeatherStationEmulator.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddSingleton<IEventStore, EventStore>();
builder.Services.AddMvcCore();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseRouting();
app.UseEndpoints(b =>
	{
		b.MapControllers();
	});

app.Run();