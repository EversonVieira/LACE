using LACE.Core.Adapter;
using LACE.Core.Auth;
using LACE.Core.Business;
using LACE.Core.Helper;
using LACE.Core.Helper.Configuration;
using LACE.Core.Models;
using LACE.Core.Repository;
using LACE.Core.Validators;
using Microsoft.OpenApi.Models;
using Nedesk.Core;
using Nedesk.Core.DataBase.Factory;
using Nedesk.Core.Interfaces;
using Nedesk.Core.MiddleWare.Extensions;
using Nedesk.Core.Security.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("NDToken", new OpenApiSecurityScheme
    {
        Description = @"NDToken of the current logged user.",
        Name = "NDToken",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "NDToken"
                },
                Name = "NDToken",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

BuildCoreServices(builder);
BuildServices(builder);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseNDAuthentication<TokenPayload>();

app.MapControllers();

app.UseCors();

app.Run();


void BuildCoreServices(WebApplicationBuilder builder)
{
    builder.Services.AddLogging();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddCors(x => x.AddDefaultPolicy(s => s.SetIsOriginAllowedToAllowWildcardSubdomains()
                           .AllowAnyOrigin().AllowAnyMethod().
                           AllowAnyHeader().DisallowCredentials().
                           SetIsOriginAllowed((host) => true)));
}

void BuildServices(WebApplicationBuilder builder)
{
    // Configuration
    NDTokenConfiguration tokenConfiguration = new NDTokenConfiguration();
    builder.Configuration.GetSection("TokenConfiguration").Bind(tokenConfiguration);
    builder.Services.AddSingleton(x => tokenConfiguration);

    LoginHelperConfiguration helperConfiguration = new LoginHelperConfiguration();
    builder.Configuration.GetSection("LoginHelperConfiguration").Bind(helperConfiguration);
    builder.Services.AddSingleton(x => helperConfiguration);
    StartupConfiguration.AddNDAuthentication<AuthUser, TokenPayload>(builder.Services);
    builder.Services.AddScoped<NDIAuthenticationService<AuthUser, TokenPayload>, AuthService>();


    // Helpers
    builder.Services.AddSingleton<LoginHelper>();

    // SQL Connection
    builder.Services.AddScoped<NDIDBConnectionFactory, NDMySqlConnectionFactory>(x => new NDMySqlConnectionFactory(builder.Configuration.GetConnectionString("mysql")));

    // Repositories
    builder.Services.AddScoped<AuthUserRepository>();
    builder.Services.AddScoped<ExamReportRepository>();

    // Business 
    builder.Services.AddScoped<AuthUserBusiness>();
    builder.Services.AddScoped<ExamReportBusiness>();

    // Adapter
    builder.Services.AddScoped<AuthUserAdapter>();

    // Validators
    builder.Services.AddScoped<ExamReportValidator>();
    builder.Services.AddScoped<AuthUserValidator>();


}