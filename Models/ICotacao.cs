namespace DesafioBTG.Models
{
    public interface ICotacao
    {
        public void GravarCotacao(Cotacao cotacao);
        public void AtualizarCotacao(Cotacao cotacao);
        public void EnviarAtualizacao(string campo);
        public void AdicionarCotacaoNaLista(decimal bitcoin, decimal ethereum);
        public bool GetAtualizarBitCoin();
        public bool GetAtualizarEthereum();
        public List<Cotacao> GetCotacoes();
    }
}
