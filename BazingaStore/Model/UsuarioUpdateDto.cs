namespace BazingaStore.Model
{
    public class UsuarioUpdateDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
    }
}
