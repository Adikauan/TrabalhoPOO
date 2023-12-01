using Cadastro_de_peças.Modelos;
using Newtonsoft.Json;

namespace Cadastro_de_peças
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public void EnvioDataGrid(object peca)
        {
            dataGridView1.DataSource = peca;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            List<Peca> pecas;
            using (StreamReader r = new StreamReader(Configuration.GetListDataPath()))
            {
                string json = r.ReadToEnd();
                Data? listaPecas = JsonConvert.DeserializeObject<Data>(json);
                pecas = listaPecas.Pecas;
            }
            dataGridView1.DataSource = pecas;

            dataGridView1.Columns["DataUltimaModificacao"].HeaderText = "Última Modificação";

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["ExtensaoImagem"].Visible = false;
            dataGridView1.Columns["Nome"].Width = 500;
            dataGridView1.Columns["Tipo"].Width = 400;
            dataGridView1.Columns["DataUltimaModificacao"].Width = 217;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRow = dataGridView1.SelectedRows;

            if (selectedRow.Count == 1)
            {
                string? selectedRowId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                using (StreamReader r = new StreamReader(Configuration.GetListDataPath()))
                {
                    string json = r.ReadToEnd();
                    Data? listaPecas = JsonConvert.DeserializeObject<Data>(json);
                    Peca? findPeca = listaPecas.Pecas.FirstOrDefault(peca => peca.Id == Guid.Parse(selectedRowId));

                    this.Hide();
                    Form2 form2 = new Form2(findPeca ?? new Peca());
                    form2.Show();
                }
            }
            else
            {
                MessageBox.Show("Selecione um dado para alterar");
            }


            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRow = dataGridView1.SelectedRows;

            if (selectedRow.Count == 1)
            {
                string? selectedRowId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                using (StreamReader r = new StreamReader(Configuration.GetListDataPath()))
                {
                    string json = r.ReadToEnd();
                    Data? listaPecas = JsonConvert.DeserializeObject<Data>(json);
                    Peca? findPeca = listaPecas.Pecas.FirstOrDefault(peca => peca.Id == Guid.Parse(selectedRowId));

                    this.Hide();
                    Form4 form4 = new Form4(findPeca ?? new Peca());
                    form4.Show();
                }
            }
            else
            {
                MessageBox.Show("Selecione um dado para alterar");
            }
        }
    }
}
