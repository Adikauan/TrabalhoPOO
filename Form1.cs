using Cadastro_de_maquinas.Modelos;
using Cadastro_de_maquinas.Modelos.Interfaces;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Cadastro_de_maquinas
{
    public partial class Form1 : Form, IAcoesGerais
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void EnvioDataGrid(object maquina)
        {
            dataGridView1.DataSource = maquina;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Data? listaMaquinas = LerLista();

            AtualizarLista(dataGridView1, listaMaquinas.Maquinas);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRow = dataGridView1.SelectedRows;

            if (selectedRow.Count == 1)
            {
                string? selectedRowId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();


                Data? listaMaquinas = LerLista();
                Maquina? findMaquina = listaMaquinas.Maquinas.FirstOrDefault(maquina => maquina.Id == Guid.Parse(selectedRowId));

                this.Hide();
                Form2 form2 = new Form2(findMaquina ?? new Maquina());
                form2.Show();

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

                Data? listaMaquinas = LerLista();
                Maquina? findMaquinas = listaMaquinas.Maquinas.FirstOrDefault(maquina => maquina.Id == Guid.Parse(selectedRowId));

                this.Hide();
                Form4 form4 = new Form4(findMaquinas ?? new Maquina());
                form4.Show();

            }
            else
                MessageBox.Show("Selecione um dado para alterar");

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRow = dataGridView1.SelectedRows;
            string serializedObject;

            DialogResult res = MessageBox.Show("Você tem certeza que gostaria de excluir o dado selecionado?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res == DialogResult.OK)
            {
                if (selectedRow.Count == 1)
                {
                    string? selectedRowId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    Data? listaMaquinas = LerLista();
                    listaMaquinas?.Maquinas?.RemoveAll(maquina => maquina.Id == Guid.Parse(selectedRowId));

                    serializedObject = JsonConvert.SerializeObject(listaMaquinas, Formatting.Indented);

                    File.WriteAllText(Configuration.GetListDataPath(), serializedObject);

                    Form1_Load(sender, e);
                }
                else
                    MessageBox.Show("Selecione um dado para excluir");

            }
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            string baseUrlImages = $"{Configuration.GetRootDirectory()}\\Imagens";
            int baseValueX = 10;
            int baseValueY = 10;
            Data? listaMaquinas = LerLista();

            foreach (Maquina maquina in listaMaquinas.Maquinas)
            {
                int YCalculated = baseValueY * (listaMaquinas.Maquinas.IndexOf(maquina) + 1 + (listaMaquinas.Maquinas.IndexOf(maquina) * 6));
                e.Graphics.DrawImage(Image.FromFile($"{baseUrlImages}\\{maquina.Id}{maquina.ExtensaoImagem}"), new Rectangle(480, YCalculated, 50, 50));
                e.Graphics.DrawString($"Nome: {maquina.Nome}", new Font("arial", 8), Brushes.Black, new Point(baseValueX, YCalculated));
                e.Graphics.DrawString($"Tipo: {maquina.Tipo}", new Font("arial", 8), Brushes.Black, new Point(baseValueX, 20 + YCalculated));
                e.Graphics.DrawString($"Última movimentação: {maquina.DataUltimaModificacao}", new Font("arial", 8), Brushes.Black, new Point(baseValueX, 40 + YCalculated));

                if (maquina.PropriedadesDinamicas.Count > 0)
                    e.Graphics.DrawString($"{maquina.PropriedadesDinamicas[0].Chave}: {maquina.PropriedadesDinamicas[0].Valor}", new Font("arial", 8), Brushes.Black, new Point(300 + baseValueX, YCalculated));

                if (maquina.PropriedadesDinamicas.Count > 1)
                    e.Graphics.DrawString($"{maquina.PropriedadesDinamicas[1].Chave}: {maquina.PropriedadesDinamicas[1].Valor}", new Font("arial", 8), Brushes.Black, new Point(300 + baseValueX, 20 + YCalculated));

                if (maquina.PropriedadesDinamicas.Count > 2)
                    e.Graphics.DrawString($"{maquina.PropriedadesDinamicas[2].Chave}: {maquina.PropriedadesDinamicas[2].Valor}", new Font("arial", 8), Brushes.Black, new Point(300 + baseValueX, 40 + YCalculated));

            }
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;

            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Imprimir maquina", 600, 1000);

            printPreviewDialog1.ShowDialog();
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                string link = $"http://google.com/search?q={textPesquisar.Text}";
                Process.Start("cmd", $"/C start {link}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            textPesquisar.Text = string.Empty;
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string filtrarPor = cbFiltro.Text;
            string valor = textFiltro.Text;

            Data? listaMaquinas = LerLista();

            List<Maquina> maquinas = listaMaquinas.Maquinas;

            List<Maquina> maquinasFiltradas = new List<Maquina>();

            if (filtrarPor.Equals("Nome"))
                maquinasFiltradas.AddRange(maquinas.Where(maquina => maquina.Nome == valor));

            if (filtrarPor.Equals("Tipo"))
                maquinasFiltradas.AddRange(maquinas.Where(maquina => maquina.Tipo == valor));

            if (string.IsNullOrEmpty(valor))
                maquinasFiltradas.AddRange(maquinas);

            AtualizarLista(dataGridView1, maquinasFiltradas);
        }
        public void AtualizarLista(DataGridView gridView, List<Maquina> maquinas)
        {
            gridView.DataSource = maquinas;

            gridView.Columns["DataUltimaModificacao"].HeaderText = "Última Modificação";

            gridView.Columns["Id"].Visible = false;
            gridView.Columns["ExtensaoImagem"].Visible = false;
            gridView.Columns["Nome"].Width = 500;
            gridView.Columns["Tipo"].Width = 400;
            gridView.Columns["DataUltimaModificacao"].Width = 217;
        }
        public Data LerLista()
        {
            using (StreamReader r = new StreamReader(Configuration.GetListDataPath()))
            {
                string json = r.ReadToEnd();
                Data? listaMaquinas = JsonConvert.DeserializeObject<Data>(json);

                return listaMaquinas;
            }
        }
    }
}
