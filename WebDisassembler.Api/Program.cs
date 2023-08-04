using WebDisassembler.Core.Extensions;
using WebDisassembler.DataStorage.Extensions;
using WebDisassembler.FileStorage.Extensions;
using WebDisassembler.ServiceProtocol.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataStorage(builder.Configuration);
builder.Services.AddFileStorage(builder.Configuration);
builder.Services.AddServiceBus(builder.Configuration);
builder.Services.AddCoreServices();

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