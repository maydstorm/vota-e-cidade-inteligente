namespace VotaE_API.ViewModel.Usuario
{
    public class UsuarioPaginacaoViewModel
    {
        public IEnumerable<UsuarioViewModel> Usuarios { get; set; }
        public int PageSize { get; set; }
        public int Ref {  get; set; }
        public int NextRef { get; set; }

        // URL para a página anterior, ou null se for a primeira página
        public string? PreviousPageUrl => Ref > 0
         ? $"/usuario?referencia={Ref - PageSize}&tamanho={PageSize}"
         : null;

        // URL para a próxima página, ou null se não houver próxima página
        public string? NextPageUrl => Usuarios.Any()
            ? $"/usuario?referencia={NextRef}&tamanho={PageSize}"
            : null;
    }
}
