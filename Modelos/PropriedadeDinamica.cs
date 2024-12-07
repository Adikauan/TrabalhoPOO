namespace Cadastro_de_maquinas.Modelos
{
    public class PropriedadeDinamica
    {
        public string Chave { get; set; }
        public string Valor { get; set; }

        public PropriedadeDinamica(string chave, string valor)
        {
            Chave = chave;
            Valor = valor;
        }
    }
}
