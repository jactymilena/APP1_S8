using GeneralSurvey.Database;
using GeneralSurvey.Helpers;
using GeneralSurvey.Models;
using GeneralSurvey.Services;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace GeneralSurvey
{
    [ExcludeFromCodeCoverage]
    public class SurveyApplication
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();

            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ISurveyService, SurveyService>();
            builder.Services.AddSingleton<DataBaseHelper>();
            builder.Services.AddSingleton<AuthentificationHelper>();
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

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5001, listenOptions =>
                {
                    listenOptions.UseHttps("./cert/server.pfx", "T6#kL9w@Pz3Q#s2!Xm93inv8vevg7");
                });
            });
        }

        private static void Configure(WebApplication app)
        {
            var db = app.Services.GetRequiredService<DataBaseHelper>();
            db.ConnectToDataBase();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}