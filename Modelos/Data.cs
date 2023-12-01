namespace Cadastro_de_peças.Modelos
{
    public class Data
    {
        public List<Peca>? Pecas {  get; set; } = new List<Peca>();
        public Data(List<Peca>? pecas)
        {
            Pecas = pecas;
        }
    }
}
