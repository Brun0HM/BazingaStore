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


        // Endpoint para listar todos os usuários do Identity junto com suas roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarios(
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuarios = await _context.Users.ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            var usuariosComRoles = new List<object>();

            foreach (var usuario in usuarios)
            {
                var roles = await userManager.GetRolesAsync(usuario);
                usuariosComRoles.Add(new
                {
                    usuario.Id,
                    usuario.UserName,
                    usuario.Email,
                    Roles = roles
                });
            }

            return Ok(usuariosComRoles);
        }

        // Endpoint para pegar o Id do Identity User (usuário logado), email e role através do token recebido no Header da Request
        [HttpGet("meu-id")]
        public ActionResult<object> GetMeuId()
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            return Ok(new
            {
                Id = userId,
                Email = email,
                Role = role
            });
        }


    }
}
