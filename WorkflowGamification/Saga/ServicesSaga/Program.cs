var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSagas(builder.Configuration);

var app = builder.Build();

app.Run();
