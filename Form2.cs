using Cadastro_de_peças.Modelos;

namespace Cadastro_de_peças
{
    public partial class Form2 : Form
    {
        private Peca pecaSelecionada;
        public Form2(Peca? peca)
        {
            InitializeComponent();
            pecaSelecionada = peca;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pbxVisualizer.Image = Image.FromFile($"{Configuration.GetRootDirectory()}\\Imagens\\{pecaSelecionada.Id}.{pecaSelecionada.ExtensaoImagem}");
            valName.Text = pecaSelecionada.Nome;
            valType.Text = pecaSelecionada.Tipo;

            if (pecaSelecionada.PropriedadesDinamicas.Count > 0)
            {
                keyProp1.Text = $"{pecaSelecionada.PropriedadesDinamicas[0].Chave}:";
                valProp1.Text = pecaSelecionada.PropriedadesDinamicas[0].Valor;
                keyProp1.Visible = true;
                valProp1.Visible = true;
            }

            if (pecaSelecionada.PropriedadesDinamicas.Count > 1)
            {
                keyProp2.Text = $"{pecaSelecionada.PropriedadesDinamicas[1].Chave}:";
                valProp2.Text = pecaSelecionada.PropriedadesDinamicas[1].Valor;
                keyProp2.Visible = true;
                valProp2.Visible = true;
            }

            if (pecaSelecionada.PropriedadesDinamicas.Count > 2)
            {
                keyProp3.Text = $"{pecaSelecionada.PropriedadesDinamicas[2].Chave}:";
                valProp3.Text = pecaSelecionada.PropriedadesDinamicas[2].Valor;
                keyProp3.Visible = true;
                valProp3.Visible = true;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
