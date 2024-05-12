using Fiap.CleanArchitecture.Api;
using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.
builder.Services.AddScoped<IDatabaseClient>(provider 
    => { return new SQLDatabaseClient(configuration); });

builder.Services.AddScoped<IUsuarioGateway, UsuarioGateway>();
builder.Services.AddScoped<ITarefaGateway, TarefaGateway>();

builder.Services.AddScoped(typeof(IControladorFactory<>), typeof(ControladorFactory<>));
builder.Services.AddScoped<IUsuarioControlador, UsuarioControlador>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Authentication:Secret"));
builder.Services.AddSwaggerGen(c =>
{
    var nomeXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, nomeXml);


    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fiap CleanArchitecture", Version = "v1.0" });
  
    c.IncludeXmlComments(caminhoArquivo);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

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
