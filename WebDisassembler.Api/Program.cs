using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebDisassembler.Api.Authentication;
using WebDisassembler.Core.Application.Extensions;
using WebDisassembler.Core.Identity;
using WebDisassembler.DataStorage.Extensions;
using WebDisassembler.FileStorage.Extensions;
using WebDisassembler.Search.Client.Extensions;
using WebDisassembler.Core.ServiceProtocol.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataStorage(builder.Configuration);
builder.Services.AddFileStorage(builder.Configuration);
builder.Services.AddServiceBus(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddElasticClients();

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
        c.EnableAnnotations();
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

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.MapControllers()
    .RequireAuthorization();

app.Services.ExecuteMigrations();

app.Run();