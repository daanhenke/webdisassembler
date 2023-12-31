using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using WebDisassembler.Api.Authentication;
using WebDisassembler.Core.Application.Extensions;
using WebDisassembler.Core.Identity;
using WebDisassembler.Core.Logging.Extensions;
using WebDisassembler.DataStorage.Extensions;
using WebDisassembler.FileStorage.Extensions;
using WebDisassembler.Search.Client.Extensions;
using WebDisassembler.Core.ServiceProtocol.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddDataStorage(builder.Configuration);
builder.Services.AddFileStorage(builder.Configuration);
builder.Services.AddServiceBus(builder.Configuration);
builder.Services.AddElasticClients(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddLogging(builder.Configuration);

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserIdentity, ClaimsUserIdentity>();

builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationSchemeHandler>(
        "Tokens",
        _ => {}
    );

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes("Tokens")
        .RequireAuthenticatedUser()
        .Build();
});

// TODO: Remove XD
if (builder.Environment.IsDevelopment() || true)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations(true, true);
    });

    builder.Services.AddCors();
}

var app = builder.Build();

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseCors(policyBuilder =>
    {
        policyBuilder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();

        if (app.Environment.IsDevelopment())
        {
            policyBuilder
                .SetIsOriginAllowed(origin => true);
        }
        else
        {
            policyBuilder
                .WithOrigins("http://webdisassembler.daan.vodka:3100");
        }
    }
);

app.MapControllers()
    .RequireAuthorization();

app.Services.ExecuteMigrations();

app.Run();