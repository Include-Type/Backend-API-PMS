var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IncludeTypeBackend", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("IncludeTypeBackend", builder =>
    {
        builder
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<PostgreSqlContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlDatabase")));
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjectService>();

var app = builder.Build();


// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IncludeTypeBackend v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("IncludeTypeBackend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
