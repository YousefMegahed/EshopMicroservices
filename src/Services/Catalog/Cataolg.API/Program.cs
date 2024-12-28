


var builder = WebApplication.CreateBuilder(args);

//add services to container.

var assemply = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assemply);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        }
    );
builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddCarter();
builder.Services.AddMarten(opt =>
  opt.Connection(builder.Configuration.GetConnectionString("Database")!)
).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(option => { });
app.Run();
