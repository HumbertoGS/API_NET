using Microsoft.EntityFrameworkCore;
using AppContext = API_en_NET.Context.AppContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Create variable for connection
var connectionString = builder.Configuration.GetConnectionString("Connections");
//Register services for connection
builder.Services.AddDbContext<AppContext>(
    options => options.UseSqlServer(connectionString)
);

//CORS
var myRulesCors = "ReglasCors";

builder.Services.AddCors(
    cors => cors.AddPolicy
    (
        name: myRulesCors, builder => {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    )
);

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

app.UseCors(myRulesCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
