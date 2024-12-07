namespace Cadastro_de_maquinas.Modelos.Interfaces
{
    public interface IAcoesGerais
    {
        public void AtualizarLista(DataGridView gridView, List<Maquina> maquinas);
        public Data LerLista();
    }
}
