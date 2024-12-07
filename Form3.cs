using Cadastro_de_maquinas.Modelos;
using Cadastro_de_maquinas.Modelos.Interfaces;
using Newtonsoft.Json;

namespace Cadastro_de_maquinas
{
    public partial class Form3 : Form
    {
        private string jsonlistaMaquinas;
        private string insertFilePath = string.Empty;
        public Form3()
        {
            InitializeComponent();
        }

        private void pbxCreate_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de imagem|*.png;*.jpg;*.jpeg;|*.*|";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                pbxCreate.Image = Image.FromFile(filePath);
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
                pbxCreate.Image = Image.FromFile(filePath);
                insertFilePath = filePath;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string imageRootDirectory = $"{Configuration.GetRootDirectory()}\\imagens";

            if (string.IsNullOrEmpty(textName.Text) || string.IsNullOrEmpty(textType.Text) || string.IsNullOrEmpty(insertFilePath))
            {
                MessageBox.Show("A imagem, nome e tipo são valores obrigatórios");
                return;
            }

            FileInfo file = new(openFileDialog1.FileName);

            string valueName = textName.Text;
            string valueType = textType.Text;

            Maquina maquina = new Maquina(valueName, valueType, new List<PropriedadeDinamica>(), file.Extension);

            using (StreamReader r = new StreamReader(Configuration.GetListDataPath()))
            {
                string json = r.ReadToEnd();
                Data? listaMaquinas = JsonConvert.DeserializeObject<Data>(json);

                maquina.PropriedadesDinamicas.AddRange(CriaPropriedadesDinamicas());

                if (listaMaquinas.Maquinas is null)
                    listaMaquinas.Maquinas = new List<Maquina> { maquina };
                else
                    listaMaquinas.Maquinas.Add(maquina);

                string serializedObject = JsonConvert.SerializeObject(listaMaquinas, Formatting.Indented);

                jsonlistaMaquinas = serializedObject;
            }

            File.WriteAllText(Configuration.GetListDataPath(), jsonlistaMaquinas);

            file.CopyTo($"{imageRootDirectory}\\{maquina.Id}{file.Extension}");

            this.Close();
            Form1 form1 = new Form1();
            form1.Show();

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

        private void Form3_Load(object sender, EventArgs e)
        {
            textName.Text = string.Empty;
            textType.Text = string.Empty;
            txtDynProp1_key.Text = string.Empty;
            txtDynProp1_val.Text = string.Empty;
            txtDynProp2_key.Text = string.Empty;
            txtDynProp2_val.Text = string.Empty;
            txtDynProp3_key.Text = string.Empty;
            txtDynProp3_val.Text = string.Empty;

            txtDynProp1_key.Visible = false;
            txtDynProp1_val.Visible = false;
            txtDynProp2_key.Visible = false;
            txtDynProp2_val.Visible = false;
            txtDynProp3_key.Visible = false;
            txtDynProp3_val.Visible = false;

            btnAdd2.Visible = false;
            btnAdd3.Visible = false;

            btnDel2.Visible = false;
            btnDel3.Visible = false;
            btnDel4.Visible = false;

            pbxCreate.Image = null;
        }
    }
}
