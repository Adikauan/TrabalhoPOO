namespace Cadastro_de_peças.Modelos
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
