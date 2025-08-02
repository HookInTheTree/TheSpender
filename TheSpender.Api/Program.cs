using TheSpender.Api;

var builder = WebApplication.CreateBuilder(args);

ProgramSetup.AddDatabase(builder.Services, builder.Configuration);
ProgramSetup.AddTelegram(builder.Services, builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
