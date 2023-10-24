namespace DesafioBTG.Models
{
    public interface ICotacao
    {
        public void GravarCotacao(Cotacao cotacao, int posicaoGrafico);
        public void AtualizarCotacao(Cotacao cotacao, int posicaoGrafico);
        public void EnviarAtualizacao(string campo);
        public void AdicionarCotacaoNaLista();
        public bool GetAtualizarBitCoin();
        public bool GetAtualizarEthereum();
        public List<Cotacao> GetCotacoes();
    }
}
