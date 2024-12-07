using Cadastro_de_maquinas.Modelos;

namespace Cadastro_de_maquinas
{
    public partial class Form2 : Form
    {
        private Maquina maquinaselecionada;
        public Form2(Maquina? maquina)
        {
            InitializeComponent();
            maquinaselecionada = maquina;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pbxVisualizer.Image = Image.FromFile($"{Configuration.GetRootDirectory()}\\Imagens\\{maquinaselecionada.Id}{maquinaselecionada.ExtensaoImagem}");
            valName.Text = maquinaselecionada.Nome;
            valType.Text = maquinaselecionada.Tipo;

            if (maquinaselecionada.PropriedadesDinamicas.Count > 0)
            {
                keyProp1.Text = $"{maquinaselecionada.PropriedadesDinamicas[0].Chave}:";
                valProp1.Text = maquinaselecionada.PropriedadesDinamicas[0].Valor;
                keyProp1.Visible = true;
                valProp1.Visible = true;
            }

            if (maquinaselecionada.PropriedadesDinamicas.Count > 1)
            {
                keyProp2.Text = $"{maquinaselecionada.PropriedadesDinamicas[1].Chave}:";
                valProp2.Text = maquinaselecionada.PropriedadesDinamicas[1].Valor;
                keyProp2.Visible = true;
                valProp2.Visible = true;
            }

            if (maquinaselecionada.PropriedadesDinamicas.Count > 2)
            {
                keyProp3.Text = $"{maquinaselecionada.PropriedadesDinamicas[2].Chave}:";
                valProp3.Text = maquinaselecionada.PropriedadesDinamicas[2].Valor;
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
