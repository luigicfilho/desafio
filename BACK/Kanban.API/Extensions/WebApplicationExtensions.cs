using Kanban.API.Configuration;
using Kanban.API.Data;
using Kanban.API.Interfaces;
using Kanban.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace Kanban.API.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureKanbanApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", SwaggerData.GenSwaggerInfo());
            o.AddSecurityDefinition("Bearer", SwaggerData.GenSwaggerSecurityScheme());
            o.AddSecurityRequirement(SwaggerData.GenSwaggerSecurityRequirement());
        });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        var jwtConfig = builder.Configuration.GetSection("jwt");

        builder.Services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    if (builder.Environment.IsDevelopment())
                    {
                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                    return c.Response.WriteAsync("An error occurred processing your authentication.");
                }
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig["ValidIssuer"],
                ValidAudience = jwtConfig["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["key"])),
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<ILoginService, LoginService>();
        builder.Services.AddTransient<ICardsRepository, CardsRepository>();

        builder.Services.AddLogging();
        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<KanbanAppDbContext>(options =>
        {
            var dbtype = builder.Configuration.GetSection("ConnectionStrings:Databasetype");
            switch (dbtype.Value)
            {
                case "sqlite":
                    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteCS"), opt =>
                    {
                        opt.CommandTimeout((int)TimeSpan.FromSeconds(60).TotalSeconds);
                    });
                    break;
                case "sqlserver":
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerCS"));
                    break;
                default:
                    options.UseInMemoryDatabase($"data-{Guid.NewGuid()}");
                    break;
            }
        });

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
    }

    public static void RegisterKanbanApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        using (IServiceScope scope = app.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            KanbanAppDbContext context = services.GetRequiredService<KanbanAppDbContext>();
            if (!context.Database.IsInMemory())
            {
                context.Database.Migrate();
            }
            //DataSeeder.Seed(context);
        }

        IEnumerable<IEndpointDefinition> endpointDefinitions = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpointDef in endpointDefinitions)
        {
            endpointDef.RegisterEndpoints(app);
        }
    }
}
