using LACE.Core.Adapter;
using LACE.Core.Auth;
using LACE.Core.Business;
using LACE.Core.Repository;
using LACE.Core.Validators;
using Microsoft.OpenApi.Models;
using Nedesk.Core.DataBase.Factory;
using Nedesk.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Session", new OpenApiSecurityScheme
    {
        Description = @"Session of the current logged user.",
        Name = "Session",
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
                    Id = "Session"
                },
                Name = "Session",
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
    // SQL Connection
    builder.Services.AddTransient<IDBConnectionFactory, MySqlConnectionFactory>(x => new MySqlConnectionFactory(builder.Configuration.GetConnectionString("mysql")));

    // Repositories
    builder.Services.AddTransient<AuthUserRepository>();
    builder.Services.AddTransient<AuthSessionRepository>();
    builder.Services.AddTransient<ExamReportRepository>();

    // Business 
    builder.Services.AddTransient<AuthUserBusiness>();
    builder.Services.AddTransient<ExamReportBusiness>();

    // Adapter
    builder.Services.AddTransient<AuthUserAdapter>();


    // Services
    builder.Services.AddTransient<IAuth, AuthService>();

    // Validators
    builder.Services.AddTransient<ExamReportValidator>();


}