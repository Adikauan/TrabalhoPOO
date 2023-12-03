using Cadastro_de_peças.Modelos;
using Cadastro_de_peças.Modelos.Interfaces;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Cadastro_de_peças
{
    public partial class Form1 : Form, IAcoesGerais
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
            Data? listaPecas = LerLista();

            AtualizarLista(dataGridView1, listaPecas.Pecas);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRow = dataGridView1.SelectedRows;

            if (selectedRow.Count == 1)
            {
                string? selectedRowId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();


                Data? listaPecas = LerLista();
                Peca? findPeca = listaPecas.Pecas.FirstOrDefault(peca => peca.Id == Guid.Parse(selectedRowId));

                this.Hide();
                Form2 form2 = new Form2(findPeca ?? new Peca());
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


                Data? listaPecas = LerLista();
                Peca? findPeca = listaPecas.Pecas.FirstOrDefault(peca => peca.Id == Guid.Parse(selectedRowId));

                this.Hide();
                Form4 form4 = new Form4(findPeca ?? new Peca());
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

                    Data? listaPecas = LerLista();

                    listaPecas?.Pecas?.RemoveAll(peca => peca.Id == Guid.Parse(selectedRowId));

                    serializedObject = JsonConvert.SerializeObject(listaPecas, Formatting.Indented);

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
            Data? listaPecas;
            using (StreamReader r = new StreamReader(Configuration.GetListDataPath()))
            {
                string json = r.ReadToEnd();
                listaPecas = JsonConvert.DeserializeObject<Data>(json);
            }

            foreach (Peca peca in listaPecas.Pecas)
            {
                int YCalculated = baseValueY * (listaPecas.Pecas.IndexOf(peca) + 1 + (listaPecas.Pecas.IndexOf(peca) * 6));
                e.Graphics.DrawImage(Image.FromFile($"{baseUrlImages}\\{peca.Id}{peca.ExtensaoImagem}"), new Rectangle(480, YCalculated, 50, 50));
                e.Graphics.DrawString($"Nome: {peca.Nome}", new Font("arial", 8), Brushes.Black, new Point(baseValueX, YCalculated));
                e.Graphics.DrawString($"Tipo: {peca.Tipo}", new Font("arial", 8), Brushes.Black, new Point(baseValueX, 20 + YCalculated));
                e.Graphics.DrawString($"Última movimentação: {peca.DataUltimaModificacao}", new Font("arial", 8), Brushes.Black, new Point(baseValueX, 40 + YCalculated));

                if (peca.PropriedadesDinamicas.Count > 0)
                    e.Graphics.DrawString($"{peca.PropriedadesDinamicas[0].Chave}: {peca.PropriedadesDinamicas[0].Valor}", new Font("arial", 8), Brushes.Black, new Point(300 + baseValueX, YCalculated));

                if (peca.PropriedadesDinamicas.Count > 1)
                    e.Graphics.DrawString($"{peca.PropriedadesDinamicas[1].Chave}: {peca.PropriedadesDinamicas[1].Valor}", new Font("arial", 8), Brushes.Black, new Point(300 + baseValueX, 20 + YCalculated));

                if (peca.PropriedadesDinamicas.Count > 2)
                    e.Graphics.DrawString($"{peca.PropriedadesDinamicas[2].Chave}: {peca.PropriedadesDinamicas[2].Valor}", new Font("arial", 8), Brushes.Black, new Point(300 + baseValueX, 40 + YCalculated));
            }
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;

            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Imprimir peca", 600, 1000);

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

            List<Peca> pecas;

            Data? listaPecas = LerLista();

            pecas = listaPecas.Pecas;

            List<Peca> pecasFiltradas = new List<Peca>();

            if (filtrarPor.Equals("Nome"))
                pecasFiltradas.AddRange(pecas.Where(peca => peca.Nome == valor));

            if (filtrarPor.Equals("Tipo"))
                pecasFiltradas.AddRange(pecas.Where(peca => peca.Tipo == valor));

            if (string.IsNullOrEmpty(valor))
                pecasFiltradas.AddRange(pecas);

            AtualizarLista(dataGridView1, pecasFiltradas);
        }
        public void AtualizarLista(DataGridView gridView, List<Peca> pecas)
        {
            gridView.DataSource = pecas;

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
                Data? listaPecas = JsonConvert.DeserializeObject<Data>(json);

                return listaPecas;
            }
        }
    }
}
