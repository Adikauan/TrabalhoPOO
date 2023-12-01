namespace Cadastro_de_peças.Modelos
{
    public class Peca
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string ExtensaoImagem { get; set; }
        public DateTime DataUltimaModificacao { get; set; }
        public List<PropriedadeDinamica> PropriedadesDinamicas { get; set; } = new();

        public Peca(string nome, string tipo, List<PropriedadeDinamica> propriedadesDinamicas, string fileExtension)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Tipo = tipo;
            DataUltimaModificacao = DateTime.Now;
            PropriedadesDinamicas = propriedadesDinamicas;
            ExtensaoImagem = fileExtension;
        }

        public Peca(Guid id, string nome, string tipo, List<PropriedadeDinamica> propriedadesDinamicas, string extensaoImagem)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            ExtensaoImagem = extensaoImagem;
            PropriedadesDinamicas = propriedadesDinamicas;
            DataUltimaModificacao = DateTime.Now;
        }

        public Peca()
        {
            
        }
    }
}
