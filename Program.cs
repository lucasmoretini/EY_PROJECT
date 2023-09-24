using EY_Project.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IMongoRepository, MongoRepository>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
.AllowAnyMethod()
.WithOrigins("http://localhost:3000"));

app.UseAuthorization();

app.MapControllers();
app.Run();