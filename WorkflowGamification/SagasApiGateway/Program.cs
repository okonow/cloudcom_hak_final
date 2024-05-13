using Dependencies;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var cors = "allow credentials";
builder.Services.AddCors(options =>
{
    options.AddPolicy(cors, policy =>
    {
        policy.WithOrigins("http//5062")
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod();
    });
});

builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, config) =>
    {
        config.Host(RabbitMqConnection.HOST, "/", h =>
        {
            h.Username(RabbitMqConnection.USERNAME);
            h.Password(RabbitMqConnection.PASSWORD);
        });

        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(cors);
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
