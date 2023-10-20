namespace DesafioBTG.Models
{
    public class Cotacao : ICotacao
    {
        public decimal bitcoin { get; set; }
        public decimal ethereum { get; set; }

        private bool atualizarBitCoin { get; set; }
        private bool atualizarEthereum { get; set; }
        private List<Cotacao> cotacoes { get; set; } = new List<Cotacao>();

        public void GravarCotacao(Cotacao cotacao)
        {
            this.bitcoin = cotacao.bitcoin;
            this.ethereum = cotacao.ethereum;

            AdicionarCotacaoNaLista(this.bitcoin, this.ethereum);
        }

        public void AtualizarCotacao(Cotacao cotacao)
        {
            atualizarBitCoin = atualizarEthereum = false;
            if (cotacao.bitcoin > 0 && this.bitcoin != cotacao.bitcoin)
            {
                this.bitcoin = cotacao.bitcoin;
                EnviarAtualizacao(nameof(bitcoin));
            }

            if (cotacao.ethereum > 0 && this.ethereum != cotacao.ethereum)
            {
                this.ethereum = cotacao.ethereum;
                EnviarAtualizacao(nameof(ethereum));
            }

            if (GetAtualizarBitCoin() || GetAtualizarEthereum())
                AdicionarCotacaoNaLista(this.bitcoin, this.ethereum);
        }

        public void EnviarAtualizacao(string campo)
        {
            if (campo == nameof(bitcoin))
                atualizarBitCoin = true;

            if (campo == nameof(ethereum))
                atualizarEthereum = true;
        }

        public void AdicionarCotacaoNaLista(decimal bitcoin, decimal ethereum)
        {
            cotacoes.Add(new Cotacao
            {
                bitcoin = this.bitcoin,
                ethereum = this.ethereum
            });
        }

        public bool GetAtualizarBitCoin()
        {
            return this.atualizarBitCoin;
        }
        public bool GetAtualizarEthereum()
        {
            return this.atualizarEthereum;
        }
        public List<Cotacao> GetCotacoes()
        {
            return this.cotacoes;
        }
    }
}
