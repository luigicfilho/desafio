using Microsoft.OpenApi.Models;

namespace Kanban.API.Configuration;

public static class SwaggerData
{
    public static OpenApiInfo GenSwaggerInfo()
    {
        var contact = new OpenApiContact()
        {
            Name = "Luigi C. Filho",
            Email = "luigicfilho@gmail.com",
            Url = new Uri("http://www.404.com")
        };

        var license = new OpenApiLicense()
        {
            Name = "Free License",
            Url = new Uri("http://www.404.com")
        };
        return new OpenApiInfo()
        {
            Version = "v1",
            Title = "Minimal API - Kanban App demo",
            Description = "Kanban App demo API",
            TermsOfService = new Uri("http://www.404.comm"),
            Contact = contact,
            License = license
        };
    }

    public static OpenApiSecurityScheme GenSwaggerSecurityScheme()
    {
        return new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JSON Web Token based security",
        };
    }

    public static OpenApiSecurityRequirement GenSwaggerSecurityRequirement()
    {
        return new OpenApiSecurityRequirement()
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
                new string[] {}
            }
        };
    }
}
