using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDo.Services.TaskAPI;
using ToDo.Services.TaskAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add database connection for AuthAPI project
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("TaskConnection"));
});

// configure MappingConfig
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
