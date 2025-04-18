using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VotaE_API.Data;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.Repository;
using VotaE_API.Services;
using VotaE_API.ViewModel.Sugestao;
using VotaE_API.ViewModel.Usuario;
using VotaE_API.ViewModel.Projeto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configuracao DB
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DataBaseContext>(

    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)

    );
#endregion


#region ServiceCollection

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<ISugestaoRepository, SugestaoRepository>();
builder.Services.AddScoped<ISugestaoService, SugestaoService>();

builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
builder.Services.AddScoped<IProjetoService, ProjetoService>();
#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c =>
{
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;

    //mapeamento Usuário
    c.CreateMap<UsuarioModel, UsuarioViewModel>();
    c.CreateMap<UsuarioViewModel, UsuarioModel>();

    //mapeamento Sugestão
    c.CreateMap<SugestaoModel, SugestaoViewModel>();
    c.CreateMap<SugestaoViewModel, SugestaoModel>();

    //mapeamento Projeto
    c.CreateMap<ProjetoModel, ProjetoViewModel>();
    c.CreateMap<ProjetoViewModel, ProjetoModel>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Autenticação

builder.Services.AddScoped<IAutenticacao, AutenticacaoService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


