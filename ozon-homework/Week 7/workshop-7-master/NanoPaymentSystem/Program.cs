using NanoPaymentSystem.Application;
using NanoPaymentSystem.Database;
using NanoPaymentSystem.PaymentProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFakePaymentProvider();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
