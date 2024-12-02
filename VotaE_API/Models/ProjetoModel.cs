namespace VotaE_API.Models
{
    public class ProjetoModel
    {
        public int ProjetoId { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataAprovacao { get; set; }
        public int SugestaoId { get; set; }
        public SugestaoModel Sugestao { get; set; }
    }
}
