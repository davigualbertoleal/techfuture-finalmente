using Microsoft.EntityFrameworkCore;
using TechFutureApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configura a Conexão com MySQL (Entity Framework)
// Certifique-se de que a ConnectionString está no appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TechFutureContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Adiciona serviços básicos do .NET
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Configuração do CORS ("Porteira Aberta" para o HTML funcionar)
builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberarGeral",
        policy =>
        {
            policy.AllowAnyOrigin()   // Aceita qualquer origem (localhost, ip, etc)
                  .AllowAnyMethod()   // Aceita GET, POST, etc
                  .AllowAnyHeader();  // Aceita qualquer cabeçalho
        });
});

var app = builder.Build();

// 4. Configurações de execução (Pipeline)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 5. Ativa o CORS (Importante: Tem que ser ANTES de MapControllers e Authorization)
app.UseCors("LiberarGeral");

app.UseAuthorization();
app.MapControllers();

app.Run();