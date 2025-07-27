using TheSpender.Api;

var builder = WebApplication.CreateBuilder(args);

ProgramSetup.AddDatabase(builder.Services, builder.Configuration);
ProgramSetup.AddTelegram(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
