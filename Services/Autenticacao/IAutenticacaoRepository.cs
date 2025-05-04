namespace LibraryManagement.Services.Autenticacao
{
    public interface IAutenticacaoRepository
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}
