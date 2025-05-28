using Microsoft.AspNetCore.Identity;

namespace BazingaStore.Model
{
    public class Usuario : IdentityUser
    {
        public Usuario() : base()
        {
        }
        public string NomeCompleto { get; set; }
    }
}


