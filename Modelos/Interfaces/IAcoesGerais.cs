namespace Cadastro_de_peças.Modelos.Interfaces
{
    public interface IAcoesGerais
    {
        public void AtualizarLista(DataGridView gridView, List<Peca> pecas);
        public Data LerLista();
    }
}
