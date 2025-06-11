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
    }
}
