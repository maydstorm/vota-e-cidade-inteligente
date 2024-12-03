namespace VotaE_API.Models
{
    public class SugestaoModel
    {
        public int SugestaoId { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string Localizacao { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
