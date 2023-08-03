using WebDisassembler.Core.Identity;
using WebDisassembler.Core.Mapping;
using WebDisassembler.Core.Services;
using WebDisassembler.DataStorage.Options;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.DataStorage.Repositories.Impl;
using WebDisassembler.DataStorage.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddOptions<DataStorageOptions>()
    .Bind(builder.Configuration.GetSection("DataStorage"))
    .ValidateDataAnnotations();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<ProjectService>();

builder.Services.AddScoped<IdentityDetails>();


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();