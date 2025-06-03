using BazingaStore.Data;
using BazingaStore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BazingaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuariosController(ApiDbContext context)
        {
            _context = context;

        }


        // Endpoint para listar todos os usuarios do Identity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsuarios()
        {
            var usuarios = await _context.Users.ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }
            return Ok(usuarios);
        }

        // Endpoint para pegar o Id do Identity User (usuário logado) através do token recebido no Header da Request
        [HttpGet("meu-id")]
        public ActionResult<string> GetMeuId()
        {
            var userId = User?.Identity?.IsAuthenticated == true ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não autenticado.");
            }
            return Ok(userId);
        }


    }
}
