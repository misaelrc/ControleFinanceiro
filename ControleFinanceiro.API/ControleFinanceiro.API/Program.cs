using ControleFinanceiro.API;
using ControleFinanceiro.API.Extensions;
using ControleFinanceiro.API.Validations;
using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.DAL.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoBD")));

builder.Services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<Context>();
builder.Services.ConfigurarSenhaUsuario();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITipoRepository, TipoRepository>();
builder.Services.AddScoped<IFuncaoRepository, FuncaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IValidator<Categoria>, CategoriaValidator>();
builder.Services.AddTransient<IValidator<FuncoesViewModel>, FuncoesViewModelValidator>();
builder.Services.AddTransient<IValidator<RegistroViewModel>, RegistroViewModelValidator>();
builder.Services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();

builder.Services.AddCors(); //Permite compartilhar recursos entre front end e backend
builder.Services.AddSpaStaticFiles(directory =>
{
    directory.RootPath = "ControleFinanceiro-UI";//caminho do front angular
});
var key = Encoding.ASCII.GetBytes(Settings.ChaveSecreta);

builder.Services.AddAuthentication(opcoes =>
{
    opcoes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opcoes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opcoes =>
    {
        opcoes.RequireHttpsMetadata = false;
        opcoes.SaveToken = true;
        opcoes.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers()
    .AddFluentValidation()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
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

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseSpaStaticFiles();

app.MapControllers();

/*app.UseSpa(spa =>
{
    spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "ControleFinanceiro-UI");

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
    }
});*/
app.Run();
