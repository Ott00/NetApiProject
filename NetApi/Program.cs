using Microsoft.EntityFrameworkCore;
using NetApi.Database;
using NetApi.Mappings;
using NetApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Connection
builder.Services.AddDbContext<ApplicationDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Inietto i miei servizi
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Automapper
builder.Services.AddAutoMapper(typeof(Automapper));

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
