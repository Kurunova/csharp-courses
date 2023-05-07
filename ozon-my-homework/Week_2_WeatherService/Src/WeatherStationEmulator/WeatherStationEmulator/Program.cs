using WeatherStationEmulator.GrpcServices;
using WeatherStationEmulator.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddGrpc();
//builder.Services.AddGrpc(options => options.Interceptors.Add<DemoInterceptor>());

builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddSingleton<IEventStore, EventStore>();
builder.Services.AddMvcCore();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseRouting();

//app.UseGrpsWeb(new GrpsWebOptiojns { DefaultEnabled = true });
app.UseEndpoints(b =>
	{
		b.MapControllers();
		b.MapGrpcService<GeneratorService>();
	});

app.Run();