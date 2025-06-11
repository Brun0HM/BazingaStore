using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BazingaStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // ✅ Somente usuários autenticados podem deslogar
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Faz o logout removendo o cookie de autenticação
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return Ok(new
            {
                message = "Logout feito com sucesso!"
            });
        }

        // ✅ Somente admin pode alterar a role de um usuário
        [HttpPut("role/{userId}")]
        public async Task<IActionResult> UpdateUserRole(string userId, [FromBody] string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return BadRequest(new { message = "Erro ao remover roles antigas." });

            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addResult.Succeeded)
                return BadRequest(new { message = "Erro ao adicionar a nova role." });

            return Ok(new { message = $"Role do usuário atualizada para {newRole}" });
        }
    }
}
