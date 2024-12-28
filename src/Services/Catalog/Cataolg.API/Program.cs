
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//add services to container.

var assemply = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assemply);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        }
    );
builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddCarter();
builder.Services.AddMarten(opt =>
  opt.Connection(builder.Configuration.GetConnectionString("Database")!)
).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(option => { });
app.Run();
