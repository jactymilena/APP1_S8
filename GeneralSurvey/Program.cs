using GeneralSurvey;
using GeneralSurvey.Database;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://localhost:5244");

// string connectionString = builder.Configuration.GetConnectionString("SurveyConnectionString");

// builder.Services.AddDbContext<SurveyDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
// builder.Services.AddSingleton<IFormResponseService, FormResponseService>();
builder.Services.AddSingleton<DataBaseHelper>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new() { Title = "GeneralSurvey", Version = "v1" });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

var db = app.Services.GetRequiredService<DataBaseHelper>();
db.CreateTables();
// db.PostUser(new User() { Username = "admin", Password = "admin" });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
