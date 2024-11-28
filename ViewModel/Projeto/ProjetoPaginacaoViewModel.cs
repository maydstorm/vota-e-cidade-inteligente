namespace VotaE_API.ViewModel.Projeto
{
    public class ProjetoPaginacaoViewModel
    {
        public IEnumerable<ProjetoViewModel> Projetos { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }

        // URL para a página anterior, ou null se for a primeira página
        public string? PreviousPageUrl => Ref > 0
         ? $"/projeto?referencia={Ref - PageSize}&tamanho={PageSize}"
         : null;

        // URL para a próxima página, ou null se não houver próxima página
        public string? NextPageUrl => Projetos.Any()
            ? $"/projeto?referencia={NextRef}&tamanho={PageSize}"
            : null;
    }
}
