namespace Cadastro_de_maquinas.Modelos
{
    public class Data
    {
        public List<Maquina>? Maquinas {  get; set; } = new List<Maquina>();
        public Data(List<Maquina>? maquinas)
        {
            Maquinas = maquinas;
        }
    }
}
