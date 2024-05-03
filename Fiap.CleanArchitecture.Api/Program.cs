using Fiap.CleanArchitecture.Api;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.
builder.Services.AddScoped<IDatabaseClient>(provider 
    => { return new SQLDatabaseClient(configuration); });

builder.Services.AddScoped<IUsuarioGateway, UsuarioGateway>();
builder.Services.AddScoped<ITarefaGateway, TarefaGateway>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => ApiConfig.Swagger(s));

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Authentication:Secret"));

builder.Services
    .AddAuthentication(a => ApiConfig.Authentication(a))
    .AddJwtBearer(j => ApiConfig.JwtBearer(j, key));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
