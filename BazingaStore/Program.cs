using BazingaStore.Data;
using BazingaStore.Model; // não esquece esse using
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Esse using é importante para o Swagger
using System.Security.Claims; // Esse using é importante para Claims

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(connectionString));

// Use apenas UM dos AddIdentityApiEndpoints, preferencialmente com seu modelo User
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
})
.AddEntityFrameworkStores<ApiDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// ----------------------------------------------------------------------
// Configuração do Swagger/OpenAPI - ATIVA APENAS EM AMBIENTE DE DESENVOLVIMENTO
// ----------------------------------------------------------------------
if (builder.Environment.IsDevelopment()) // Verifica se o ambiente é "Development"
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description = "Entre com 'Bearer ' [espaço] e então seu token no campo abaixo.\n\nExemplo: \"Bearer seu_token_aqui\""
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });
}
// ----------------------------------------------------------------------

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// ----------------------------------------------------------------------
// Middleware do Swagger/OpenAPI - ATIVA APENAS EM AMBIENTE DE DESENVOLVIMENTO
// ----------------------------------------------------------------------
if (app.Environment.IsDevelopment()) // Verifica se o ambiente é "Development"
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// ----------------------------------------------------------------------

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();