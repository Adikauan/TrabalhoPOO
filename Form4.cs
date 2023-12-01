using Cadastro_de_peças.Modelos;
using Newtonsoft.Json;

namespace Cadastro_de_peças
{
    public partial class Form4 : Form
    {
        private Peca pecaSelecionada;
        private string jsonListaPecas;
        private string insertFilePath = string.Empty;
        public Form4(Peca? peca)
        {
            InitializeComponent();
            this.pecaSelecionada = peca;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            pbxUpdate.Image = Image.FromFile($"{Configuration.GetRootDirectory()}\\Imagens\\{pecaSelecionada.Id}{pecaSelecionada.ExtensaoImagem}");
            textName.Text = pecaSelecionada.Nome;
            textType.Text = pecaSelecionada.Tipo;

            if (pecaSelecionada.PropriedadesDinamicas.Count > 0)
            {
                txtDynProp1_key.Text = pecaSelecionada.PropriedadesDinamicas[0].Chave;
                txtDynProp1_val.Text = pecaSelecionada.PropriedadesDinamicas[0].Chave;
                txtDynProp1_key.Visible = true;
                txtDynProp1_val.Visible = true;
                btnAdd2.Visible = true;
                btnDel2.Visible = true;
            }

            if (pecaSelecionada.PropriedadesDinamicas.Count > 1)
            {
                txtDynProp2_key.Text = pecaSelecionada.PropriedadesDinamicas[1].Chave;
                txtDynProp2_val.Text = pecaSelecionada.PropriedadesDinamicas[1].Chave;
                txtDynProp2_key.Visible = true;
                txtDynProp2_val.Visible = true;
                btnAdd3.Visible = true;
                btnDel3.Visible = true;
            }

            if (pecaSelecionada.PropriedadesDinamicas.Count > 2)
            {
                txtDynProp3_key.Text = pecaSelecionada.PropriedadesDinamicas[2].Chave;
                txtDynProp3_val.Text = pecaSelecionada.PropriedadesDinamicas[2].Chave;
                txtDynProp3_key.Visible = true;
                txtDynProp3_val.Visible = true;
                btnDel4.Visible = true;
            }


        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            btnAdd1.Visible = false;
            txtDynProp1_key.Visible = true;
            txtDynProp1_val.Visible = true;
            btnAdd2.Visible = true;
            btnDel2.Visible = true;
            txtDynProp2_key.Visible = false;
            txtDynProp2_val.Visible = false;
            btnAdd3.Visible = false;
            btnDel3.Visible = false;
            txtDynProp3_key.Visible = false;
            txtDynProp3_val.Visible = false;
            btnDel4.Visible = false;

            txtDynProp1_key.Text = string.Empty;
            txtDynProp1_val.Text = string.Empty;
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            btnAdd1.Visible = false;
            txtDynProp1_key.Visible = true;
            txtDynProp1_val.Visible = true;
            btnAdd2.Visible = false;
            btnDel2.Visible = false;
            txtDynProp2_key.Visible = true;
            txtDynProp2_val.Visible = true;
            btnAdd3.Visible = true;
            btnDel3.Visible = true;
            txtDynProp3_key.Visible = false;
            txtDynProp3_val.Visible = false;
            btnDel4.Visible = false;
        }

        private void btnAdd3_Click(object sender, EventArgs e)
        {
            btnAdd1.Visible = false;
            txtDynProp1_key.Visible = true;
            txtDynProp1_val.Visible = true;
            btnAdd2.Visible = false;
            btnDel2.Visible = false;
            txtDynProp2_key.Visible = true;
            txtDynProp2_val.Visible = true;
            btnAdd3.Visible = false;
            btnDel3.Visible = false;
            txtDynProp3_key.Visible = true;
            txtDynProp3_val.Visible = true;
            btnDel4.Visible = true;
        }

        private void btnDel2_Click(object sender, EventArgs e)
        {
            btnAdd1.Visible = true;
            txtDynProp1_key.Visible = false;
            txtDynProp1_val.Visible = false;
            btnAdd2.Visible = false;
            btnDel2.Visible = false;
            txtDynProp2_key.Visible = false;
            txtDynProp2_val.Visible = false;
            btnAdd3.Visible = false;
            btnDel3.Visible = false;
            txtDynProp3_key.Visible = false;
            txtDynProp3_val.Visible = false;
            btnDel4.Visible = false;

            txtDynProp1_key.Text = string.Empty;
            txtDynProp1_val.Text = string.Empty;
        }

        private void btnDel3_Click(object sender, EventArgs e)
        {
            btnAdd1.Visible = false;
            txtDynProp1_key.Visible = true;
            txtDynProp1_val.Visible = true;
            btnAdd2.Visible = true;
            btnDel2.Visible = true;
            txtDynProp2_key.Visible = false;
            txtDynProp2_val.Visible = false;
            btnAdd3.Visible = false;
            btnDel3.Visible = false;
            txtDynProp3_key.Visible = false;
            txtDynProp3_val.Visible = false;
            btnDel4.Visible = false;

            txtDynProp2_key.Text = string.Empty;
            txtDynProp2_val.Text = string.Empty;
        }

        private void btnDel4_Click(object sender, EventArgs e)
        {
            btnAdd1.Visible = false;
            txtDynProp1_key.Visible = true;
            txtDynProp1_val.Visible = true;
            btnAdd2.Visible = false;
            btnDel2.Visible = false;
            txtDynProp2_key.Visible = true;
            txtDynProp2_val.Visible = true;
            btnAdd3.Visible = true;
            btnDel3.Visible = true;
            txtDynProp3_key.Visible = false;
            txtDynProp3_val.Visible = false;
            btnDel4.Visible = false;

            txtDynProp3_key.Text = string.Empty;
            txtDynProp3_val.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string imageRootDirectory = $"{Configuration.GetRootDirectory()}\\imagens";

            if (string.IsNullOrEmpty(textName.Text) || string.IsNullOrEmpty(textType.Text))
            {
                MessageBox.Show("A imagem, nome e tipo são valores obrigatórios");
                return;
            }

            FileInfo file; 

            string valueName = textName.Text;
            string valueType = textType.Text;

            Peca peca;

            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                file = new(openFileDialog1.FileName);
                peca = new Peca(valueName, valueType, new List<PropriedadeDinamica>(), file.Extension);
                file.CopyTo($"{imageRootDirectory}\\{peca.Id}{file.Extension}");
            }
            else
            {
                peca = new Peca(pecaSelecionada.Id, valueName, valueType, new List<PropriedadeDinamica>(), pecaSelecionada.ExtensaoImagem);
            }

            using (StreamReader r = new StreamReader(Configuration.GetListDataPath()))
            {
                string json = r.ReadToEnd();
                Data? listapecas = JsonConvert.DeserializeObject<Data>(json);

                listapecas.Pecas.RemoveAll(peca => peca.Id == pecaSelecionada.Id);

                peca.PropriedadesDinamicas.AddRange(CriaPropriedadesDinamicas());

                listapecas.Pecas.Add(peca);

                string serializedObject = JsonConvert.SerializeObject(listapecas, Formatting.Indented);

                jsonListaPecas = serializedObject;
            }

            File.WriteAllText(Configuration.GetListDataPath(), jsonListaPecas);

            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void pbxUpdate_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de imagem|*.png;*.jpg;*.jpeg;|*.*|";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                pbxUpdate.Image = Image.FromFile(filePath);
                btnUpdateImage.Visible = true;
                insertFilePath = filePath;
            }
        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de imagem|*.png;*.jpg;*.jpeg;|*.*|";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                pbxUpdate.Image = Image.FromFile(filePath);
                insertFilePath = filePath;
            }
        }
        private List<PropriedadeDinamica> CriaPropriedadesDinamicas()
        {
            List<PropriedadeDinamica> propriedadesDinamicas = new();

            if (txtDynProp1_key.Visible)
                propriedadesDinamicas.Add(new PropriedadeDinamica(txtDynProp1_key.Text, txtDynProp1_val.Text));

            if (txtDynProp2_key.Visible)
                propriedadesDinamicas.Add(new PropriedadeDinamica(txtDynProp2_key.Text, txtDynProp2_val.Text));

            if (txtDynProp3_key.Visible)
                propriedadesDinamicas.Add(new PropriedadeDinamica(txtDynProp3_key.Text, txtDynProp3_val.Text));

            return propriedadesDinamicas;
        }
    }
}
