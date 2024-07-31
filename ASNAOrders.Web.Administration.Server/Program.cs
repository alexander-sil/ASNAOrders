
using ASNAOrders.Web.Administration.Server.DataAccess;
using ASNAOrders.Web.Administration.Server.LogicServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ASNAOrders.Web.Administration.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"), rollingInterval: RollingInterval.Hour).CreateLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddSerilog(dispose: true);

                // Add services to the container.
                builder.Services.AddDbContext<CustomDbContext>(options =>
                {
                    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(SecretGeneratorService.GetIssuerSigningKey(Properties.Resources.TokenSigningKeyFilePathString)),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

                builder.Services.AddAuthorization
                (
                    options =>
                    {
                        options.AddPolicy
                        (
                            "Operator",
                            po =>
                            {
                                po.RequireClaim("permissions", "RWO");
                            }
                        );
                        options.AddPolicy
                        (
                            "ReadEdit",
                            po =>
                            {
                                po.RequireClaim("permissions", new List<string> { "RW", "RWO" });
                            }
                        );
                        options.AddPolicy
                        (
                            "Read",
                            po =>
                            {
                                po.RequireClaim("permissions", new List<string> { "R", "RW", "RWO" });
                            }
                        );
                    }
                );

                builder.Services.AddSwaggerGen(option =>
                {
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.UseSwagger();
                app.UseSwaggerUI();

                app.Run();

                
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }

            Log.CloseAndFlush();
        }
    }
}
