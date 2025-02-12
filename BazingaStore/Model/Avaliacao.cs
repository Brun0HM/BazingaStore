using System.ComponentModel.DataAnnotations;

namespace BazingaStore.Model
{
    public class Avaliacao
    {
        public Guid AvaliacaoId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
        [Range(1, 5)]
        public int Nota { get; set; }
        public string? Comentario { get; set; }
        public DateTime Data { get; set; }
    }
}
