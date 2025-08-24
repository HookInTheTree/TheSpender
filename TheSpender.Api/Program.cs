using TheSpender.BLL;
using TheSpender.DAL;
using TheSpender.TGL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogic(builder.Configuration);
builder.Services.AddTelegramLayer(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
