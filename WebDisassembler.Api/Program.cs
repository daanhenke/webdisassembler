using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Identity;
using WebDisassembler.Core.Mapping;
using WebDisassembler.DataStorage.Extensions;
using WebDisassembler.FileStorage;
using WebDisassembler.FileStorage.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataStorage(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<BinaryService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<IdentityDetails>();
builder.Services.AddScoped<IFileStorage, BlobStorageFileStorage>();


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