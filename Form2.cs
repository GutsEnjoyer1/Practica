using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica
{

    public partial class Form2 : Form
    {
        private string connectionString = "Data Source=DANIEL;Initial Catalog=GaraAuto;Integrated Security=True";


        private void Form2_Click(object sender, EventArgs e)
        {

            // IncarcaTraseu();
        }



        public Form2()
        {
            InitializeComponent();
            comboBox1.MaxDropDownItems = 21;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            this.FormClosed += Form2_FormClosed;
            this.Click += Form2_Click;
            IncarcaTrasee();
            IncarcaTraseu();
            drum.Text = "Cautare";
            drum.ForeColor = Color.Gray;
            textBox1.Text = "Cautare";
            textBox1.ForeColor = Color.Gray;
            IncarcaTraseu();
            IncarcaTraseu();
            Incarcalocalitat();

            drum.KeyDown += drum_KeyDown;

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            IncarcaPornireOprire();
            comboBox1.MaxDropDownItems = 21;

            panel2.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;


            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.AutoCompleteMode = AutoCompleteMode.None;


            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;




            Incarcacurse();
            Incarcatransport();
            IncarcatransportA();
            Incarcatrase();
            IncarcaDateUtilizator();
            IncarcaDateRezervari();







        }
        private void AscundeToateTaburile()
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                tabPage.Parent = null;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            panel4.Location = new Point(262, 0);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                dataGridView1.DataSource = null;


                IncarcaTraseu();
            }
            catch (Exception ex)
            {
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void IncarcaTrasee()
        {

        }
        private void IncarcaTraseu()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM TraseeAuto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    dataGridView1.DataSource = dataTable;


                    if (dataGridView1.Columns["Info"] == null)
                    {
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.Name = "Info";
                        buttonColumn.HeaderText = "Info";
                        buttonColumn.Text = "Info";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(buttonColumn);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int traseuId = Convert.ToInt32(row.Cells["ID_TrasaAuto"].Value);
                        string pornire = row.Cells["Pornire"].Value.ToString();
                        string oprire = row.Cells["Oprire"].Value.ToString();
                        int numarCurse = Convert.ToInt32(row.Cells["NumarCurse"].Value);
                        string distanta = row.Cells["Distanta"].Value.ToString();
                        string localitati = row.Cells["Localitati"].Value.ToString();
                        string destinatieTrasaAuto = row.Cells["Destinatie_TrasaAuto"].Value.ToString();

                        string query = "UPDATE TraseeAuto SET Pornire = @Pornire, Oprire = @Oprire, NumarCurse = @NumarCurse, Distanta = @Distanta, Localitati = @Localitati, Destinatie_TrasaAuto = @Destinatie_TrasaAuto WHERE ID_TrasaAuto = @TraseuId";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Pornire", pornire);
                        command.Parameters.AddWithValue("@Oprire", oprire);
                        command.Parameters.AddWithValue("@NumarCurse", numarCurse);
                        command.Parameters.AddWithValue("@Distanta", distanta);
                        command.Parameters.AddWithValue("@Localitati", localitati);
                        command.Parameters.AddWithValue("@Destinatie_TrasaAuto", destinatieTrasaAuto);
                        command.Parameters.AddWithValue("@TraseuId", traseuId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Actualizare realizată cu succes.");


                IncarcaTraseu();
            }
            catch (Exception ex)
            {
            }
        }


        private bool primaApasare = true;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (primaApasare)
                {
                    MessageBox.Show("Această acțiune adaugă un nou traseu în baza de date.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    primaApasare = false;
                }
                string pornire = PromptInput("Introduceți ora de pornire:");
                string oprire = PromptInput("Introduceti ora de oprire:");
                int numarCurse = Convert.ToInt32(PromptInput("Introduceti numarul de curse:"));
                string distanta = PromptInput("Introduceti distanta:");
                string localitati = PromptInput("Introduceti localitatile traversate:");
                string destinatieTrasaAuto = PromptInput("Introduceti destinatia traseului:");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO TraseeAuto (Pornire, Oprire, NumarCurse, Distanta, Localitati, Destinatie_TrasaAuto) VALUES (@Pornire, @Oprire, @NumarCurse, @Distanta, @Localitati, @Destinatie_TrasaAuto)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Pornire", pornire);
                    command.Parameters.AddWithValue("@Oprire", oprire);
                    command.Parameters.AddWithValue("@NumarCurse", numarCurse);
                    command.Parameters.AddWithValue("@Distanta", distanta);
                    command.Parameters.AddWithValue("@Localitati", localitati);
                    command.Parameters.AddWithValue("@Destinatie_TrasaAuto", destinatieTrasaAuto);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Traseul a fost adaugat cu succes.");
                }



                IncarcaTrasee();
                IncarcaTraseu();
            }
            catch (Exception ex)
            {
            }
        }

        private string PromptInput(string message)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(message, "Adaugare Traseu", "");
        }






        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Sterge_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.SelectedRows.Count > 0)
                {

                    DialogResult result = MessageBox.Show("Sunteti sigur ca doriti sa stergeti randul selectat?", "Confirmare stergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        int traseuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_TrasaAuto"].Value);


                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM TraseeAuto WHERE ID_TrasaAuto = @TraseuId";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@TraseuId", traseuId);
                            command.ExecuteNonQuery();
                        }


                        IncarcaTraseu();
                    }
                }
                else
                {
                    MessageBox.Show("Va rugam sa selectati un rand pentru stergere.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
            }
        }














        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Localitati_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.SelectedRows.Count > 0)
                {

                    int traseuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_TrasaAuto"].Value);


                    string query = "SELECT NumeLocalitate FROM LocalitatiTraseu WHERE ID_TrasaAuto = @TraseuId";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@TraseuId", traseuId);

                        StringBuilder localitati = new StringBuilder();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                localitati.AppendLine(reader["NumeLocalitate"].ToString());
                            }
                        }


                        MessageBox.Show("Localitatile pentru acest traseu sunt:\n\n" + localitati.ToString(), "Localitati Traseu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Va rugam sa selectati un traseu din lista de mai sus.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void drum_Enter(object sender, EventArgs e)
        {
            if (drum.Text == "Cautare")
            {
                drum.Text = "";
                drum.ForeColor = Color.Black;
            }
        }

        private void drum_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(drum.Text))
            {
                drum.Text = "Cautare";
                drum.ForeColor = Color.Gray;
                IncarcaTraseu();
            }
        }

        private void drum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = drum.Text.Trim();
                if (filter == "" || filter == "Cautare")
                {

                    IncarcaTraseu();
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT * FROM TraseeAuto WHERE Pornire LIKE @filter OR Oprire LIKE @filter OR Localitati LIKE @filter OR Destinatie_TrasaAuto LIKE @filter";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@filter", "%" + filter + "%");
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Info"].Index && e.RowIndex >= 0)
            {

                int traseuId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID_TrasaAuto"].Value);


                AfiseazaLocalitati(traseuId);
            }
        }

        private void drum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                drum.Text = "";
            }
        }

        private void Adaugalocalitati_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.SelectedRows.Count > 0)
                {

                    int traseuId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_TrasaAuto"].Value);


                    DialogResult result;
                    do
                    {
                        string localitate = PromptInput("Introduceți o localitate:");
                        if (!string.IsNullOrWhiteSpace(localitate))
                        {

                            AdaugaLocalitate(traseuId, localitate);
                            result = MessageBox.Show("Localitatea a fost adăugată cu succes. Doriți să adăugați încă o localitate?", "Adăugare Localitate", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }
                        else
                        {
                            MessageBox.Show("Vă rugăm să introduceți o localitate validă.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            result = DialogResult.Yes;
                        }
                    } while (result == DialogResult.Yes);
                }
                else
                {
                    MessageBox.Show("Vă rugăm să selectați un traseu din lista de mai sus.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AdaugaLocalitate(int traseuId, string localitate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO LocalitatiTraseu (ID_TrasaAuto, NumeLocalitate) VALUES (@TraseuId, @Localitate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TraseuId", traseuId);
                    command.Parameters.AddWithValue("@Localitate", localitate);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la adaugarea localitatii în baza de date: " + ex.Message);
            }
        }

        private void Traseu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Curse_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string selectedValue = comboBox1.SelectedItem.ToString();
                        string[] splitValues = selectedValue.Split(new string[] { " - " }, StringSplitOptions.None);
                        string pornire = splitValues[0];
                        string oprire = splitValues[1];

                        string query = "SELECT ID_TrasaAuto FROM TraseeAuto WHERE Pornire = @Pornire AND Oprire = @Oprire";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Pornire", pornire);
                        command.Parameters.AddWithValue("@Oprire", oprire);
                        int traseuAutoID = (int)command.ExecuteScalar();


                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void IncarcaPornireOprire()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Pornire + ' - ' + Oprire AS PornireOprire FROM TraseeAuto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    List<string> pornireOprireList = new List<string>();
                    while (reader.Read())
                    {
                        pornireOprireList.Add(reader["PornireOprire"].ToString());
                    }



                    comboBox1.DataSource = pornireOprireList;


                    comboBox1.DataSource = pornireOprireList;
                    comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception ex)
            {
            }
        }








        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void Bilete_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void raport_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Pasageri;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = textBox1.Text.Trim();
                if (filter == "" || filter == "Cautare")
                {
                    Incarcacurse();
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = @"
                    SELECT c.ID_Cursa, c.Nume_Cursa, c.Capacitate, c.Tip_Transport,
                           t.Pornire + ' - ' + t.Oprire AS Traseu, c.ID_TrasaAuto, c.Id_Localitate
                    FROM Curse c
                    JOIN TraseeAuto t ON c.ID_TrasaAuto = t.ID_TrasaAuto
                    WHERE c.Nume_Cursa LIKE @filter OR c.Capacitate LIKE @filter OR c.Tip_Transport LIKE @filter OR t.Pornire LIKE @filter OR t.Oprire LIKE @filter";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@filter", "%" + filter + "%");
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView2.DataSource = dataTable;

                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        dataGridView2.Columns["ID_Cursa"].Visible = false;
                        dataGridView2.Columns["ID_TrasaAuto"].Visible = false;
                        dataGridView2.Columns["Id_Localitate"].Visible = false;

                        if (dataGridView2.Columns["Info"] == null)
                        {
                            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                            buttonColumn.Name = "Info";
                            buttonColumn.HeaderText = "Localitati";
                            buttonColumn.Text = "Info";
                            buttonColumn.UseColumnTextForButtonValue = true;
                            dataGridView2.Columns.Add(buttonColumn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Incarcacurse()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
        SELECT c.ID_Cursa, c.Nume_Cursa, c.Capacitate, c.Tip_Transport,
               t.Pornire + ' - ' + t.Oprire AS Traseu, c.ID_TrasaAuto, c.Id_Localitate,
               dc.DataCalatorie, oc.OraCalatorie
        FROM Curse c
        JOIN TraseeAuto t ON c.ID_TrasaAuto = t.ID_TrasaAuto
        JOIN DataCalatorie dc ON c.ID_DataCalatorie = dc.ID_DataCalatorie
        JOIN OraCalatorie oc ON c.ID_OraCalatorie = oc.ID_OraCalatorie";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView2.DataSource = dataTable;

                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dataGridView2.Columns["ID_Cursa"].Visible = false;
                    dataGridView2.Columns["ID_TrasaAuto"].Visible = false;
                    dataGridView2.Columns["Id_Localitate"].Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }






        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["Info"].Index && e.RowIndex >= 0)
            {
                int traseuId = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["ID_TrasaAuto"].Value);
                AfiseazaLocalitati(traseuId);
            }
        }

        private void AfiseazaLocalitati(int traseuId)
        {
            try
            {
                StringBuilder localitati = new StringBuilder();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT NumeLocalitate FROM LocalitatiTraseu WHERE ID_TrasaAuto = @TraseuId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TraseuId", traseuId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            localitati.AppendLine(reader["NumeLocalitate"].ToString());
                        }
                    }
                }

                MessageBox.Show("Localitatile pentru acest traseu sunt:\n\n" + localitati.ToString(), "Localitati Traseu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
            }
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Cautare")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Cautare";
                textBox1.ForeColor = Color.Gray;
                Incarcacurse();
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void sterge2_Click(object sender, EventArgs e)
        {

        }


        private void actualizare2_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView2.SelectedRows[0];


                string numeCursa = row.Cells["Nume_Cursa"].Value.ToString();
                string capacitate = row.Cells["Capacitate"].Value.ToString();
                string tipTransport = row.Cells["Tip_Transport"].Value.ToString();
                string traseu = row.Cells["Traseu"].Value.ToString();


                textBox5.Text = numeCursa;
                capacitateaA.Text = capacitate;
                comboTA.Text = tipTransport;
                combotraseuA.Text = traseu;
            }
            else
            {
                MessageBox.Show("Selectați mai întâi o cursă din lista de curse.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void adaugare2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void inapoi_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void comboBox1_Click(object sender, EventArgs e)
        {

        }

        private void inapoicurse_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;

            Incarcacurse();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void inapoi3_Click(object sender, EventArgs e)
        {
            

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //combotransport sa afiseze tip_transport distinct 
        }

        private void combotransport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Incarcatransport()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Tip_Transport FROM Curse";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> transportList = new List<string>();
                        while (reader.Read())
                        {
                            transportList.Add(reader["Tip_Transport"].ToString());
                        }

                        combotransport.DataSource = transportList;
                        combotransport.DropDownStyle = ComboBoxStyle.DropDown;
                        combotransport.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        combotransport.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void numec_TextChanged(object sender, EventArgs e)
        {

        }

        private void capacitatec_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimec_ValueChanged(object sender, EventArgs e)
        {

        }

        private void combolocalitati_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void adaugare3_Click(object sender, EventArgs e)
        {
            string numeCursa = numec.Text;
            int capacitate = int.Parse(capacitatec.Text);
            string tipTransport = combotransport.SelectedItem.ToString();
           

            string traseuAutoText = comboBox1.SelectedItem.ToString();

            int traseuAutoID = GetTraseuAutoID(traseuAutoText);

            if (traseuAutoID != -1)
            {
                string query = "INSERT INTO Curse (Nume_Cursa, Capacitate, Tip_Transport, ID_TrasaAuto) " +
                               "VALUES (@NumeCursa, @Capacitate, @TipTransport, @TraseuAutoID)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NumeCursa", numeCursa);
                    command.Parameters.AddWithValue("@Capacitate", capacitate);
                    command.Parameters.AddWithValue("@TipTransport", tipTransport);
                   
                    command.Parameters.AddWithValue("@TraseuAutoID", traseuAutoID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected + " cursa a fost adăugată cu succes!");

                        numec.Text = "";
                        capacitatec.Text = "";
                        combotransport.SelectedIndex = -1;
                        
                        Incarcacurse();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare la adăugarea cursei: " + ex.Message);
                    }
                }
            }
            else
            {
            }
        }


        private int GetTraseuAutoID(string selectedText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string[] splitValues = selectedText.Split(new string[] { " - " }, StringSplitOptions.None);
                    string pornire = splitValues[0];
                    string oprire = splitValues[1];

                    string query = "SELECT ID_TrasaAuto FROM TraseeAuto WHERE Pornire = @Pornire AND Oprire = @Oprire";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Pornire", pornire);
                    command.Parameters.AddWithValue("@Oprire", oprire);
                    object result = command.ExecuteScalar();


                    if (result != null)
                    {

                        return Convert.ToInt32(result);
                    }
                    else
                    {

                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la obținerea identificatorului traseului auto: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }




        private void combolocalitati_SelectedIndexChanged_1(object sender, EventArgs e)
        {


        }
        private void Incarcalocalitat()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT NumeLocalitate FROM LocalitatiTraseu";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> localitatiList = new List<string>();
                        while (reader.Read())
                        {
                            localitatiList.Add(reader["NumeLocalitate"].ToString());
                        }

                        combolocalitati.DataSource = localitatiList;
                        combolocalitati.DropDownStyle = ComboBoxStyle.DropDown;
                        combolocalitati.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        combolocalitati.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void capacitateaA_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboTA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void datecursaA_ValueChanged(object sender, EventArgs e)
        {

        }

        private void combotraseuA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void IncarcatransportA()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Tip_Transport FROM Curse";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> transportList = new List<string>();
                        while (reader.Read())
                        {
                            transportList.Add(reader["Tip_Transport"].ToString());
                        }

                        comboTA.DataSource = transportList;
                        comboTA.DropDownStyle = ComboBoxStyle.DropDown;
                        comboTA.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboTA.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Incarcatrase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Pornire + ' - ' + Oprire AS Traseu FROM TraseeAuto";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> traseuList = new List<string>();
                        while (reader.Read())
                        {
                            traseuList.Add(reader["Traseu"].ToString());
                        }

                        combotraseuA.DataSource = traseuList;
                        combotraseuA.DropDownStyle = ComboBoxStyle.DropDown;
                        combotraseuA.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        combotraseuA.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Incarcatrasea()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Pornire + ' - ' + Oprire AS PornireOprire FROM TraseeAuto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    List<string> pornireOprireList = new List<string>();
                    while (reader.Read())
                    {
                        pornireOprireList.Add(reader["PornireOprire"].ToString());
                    }



                    combotraseuA.DataSource = pornireOprireList;


                    combotraseuA.DataSource = pornireOprireList;
                    combotraseuA.DropDownStyle = ComboBoxStyle.DropDown;
                    combotraseuA.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    combotraseuA.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void Actualizarecursa_Click(object sender, EventArgs e)
        {
            string numeCursa = numec.Text;
            int capacitate;
            if (!int.TryParse(capacitateaA.Text, out capacitate))
            {
                MessageBox.Show("Capacitatea trebuie să fie un număr întreg.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string tipTransport = combotransport.SelectedItem?.ToString();
            

            string traseuAutoText = comboBox1.SelectedItem?.ToString();

            int traseuAutoID = GetTraseuAutoID(traseuAutoText);

            if (traseuAutoID != -1)
            {
                string query = "INSERT INTO Curse (Nume_Cursa, Capacitate, Tip_Transport, ID_TrasaAuto) " +
                               "VALUES (@NumeCursa, @Capacitate, @TipTransport, @TraseuAutoID)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NumeCursa", numeCursa);
                    command.Parameters.AddWithValue("@Capacitate", capacitate);
                    command.Parameters.AddWithValue("@TipTransport", tipTransport);
                    
                    command.Parameters.AddWithValue("@TraseuAutoID", traseuAutoID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected + " cursa a fost adăugată cu succes!");

                        numec.Text = "";
                        capacitatec.Text = "";
                        combotransport.SelectedIndex = -1;
                       
                        Incarcacurse();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare la adăugarea cursei: " + ex.Message);
                    }
                }
            }
            else
            {
            }
        }

        private int GetTraseuAuto(string selectedText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string[] splitValues = selectedText.Split(new string[] { " - " }, StringSplitOptions.None);
                    string pornire = splitValues[0];
                    string oprire = splitValues[1];

                    string query = "SELECT ID_TrasaAuto FROM TraseeAuto WHERE Pornire = @Pornire AND Oprire = @Oprire";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Pornire", pornire);
                    command.Parameters.AddWithValue("@Oprire", oprire);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la obținerea identificatorului traseului auto: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TOP 1 Mesaj, Poza FROM mesaj ORDER BY Id_Mesaj DESC";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string mesaj = reader["Mesaj"].ToString();
                        label14.Text = mesaj;
                        label14.Visible = true;

                        if (!reader.IsDBNull(reader.GetOrdinal("Poza")))
                        {
                            byte[] imageData = (byte[])reader["Poza"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                pictureBox5.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            pictureBox5.Image = null;
                        }
                    }
                    else
                    {
                        label14.Visible = false;
                        pictureBox5.Image = null;
                        MessageBox.Show("Nu există mesaje în baza de date.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea mesajului și a pozei: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /* private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {

             if (e.ColumnIndex == dataGridView2.Columns["Bilet"].Index && e.RowIndex >= 0)
             {
                 int traseuId = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Id_Utilizator"].Value);
                 AfiseazaLocalitati(traseuId);
             }
         }


         */


        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView3.Columns["Bilet"].Index && e.RowIndex >= 0)
            {
                int idUtilizator = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells["Id_Utilizator"].Value);
                Afiseazanumarbiele(idUtilizator);
            }
        }


        private void IncarcaDateUtilizator()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
SELECT 
    u.Id_Utilizator, 
    u.NumeUtilizator, 
    u.PrenumeUtilizator, 
    u.Bagaj, 
    u.NumarBilete,
    MAX(c.Nume_Cursa) AS Nume_Cursa, 
    MAX(c.Capacitate) AS Capacitate, 
    MAX(c.Tip_Transport) AS Tip_Transport, 
    MAX(c.DataCalatorie) AS DataCalatorie
FROM 
    utilizator u
JOIN 
    Curse c ON u.Id_Cursa = c.ID_Cursa
LEFT JOIN 
    bilete b ON u.Id_Utilizator = b.Id_utilizator
GROUP BY 
    u.Id_Utilizator, 
    u.NumeUtilizator, 
    u.PrenumeUtilizator, 
    u.Bagaj, 
    u.NumarBilete;
";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView3.DataSource = dataTable;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dataGridView3.Columns["Bilet"] == null)
                    {
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.Name = "Bilet";
                        buttonColumn.HeaderText = "Bilet";
                        buttonColumn.Text = "biletele";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        dataGridView3.Columns.Add(buttonColumn);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            dataGridView3.CellContentClick += DataGridView3_CellContentClick;
        }

        private void Afiseazanumarbiele(int idutilizator)
        {
            try
            {
                StringBuilder bilete = new StringBuilder();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT NumarBilet FROM bilete WHERE Id_utilizator = @idutilizator"; 
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idutilizator", idutilizator); 

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bilete.AppendLine(reader["NumarBilet"].ToString());
                        }
                    }
                }

                if (bilete.Length > 0)
                {
                    MessageBox.Show("Utilizatorul are biletele cu numerele:\n\n" + bilete.ToString(), "Bilete Utilizator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Utilizatorul nu are bilete.", "Bilete Utilizator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /* private void Incarcacurse()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT c.ID_Cursa, c.Nume_Cursa, c.Capacitate, c.Tip_Transport,
                       t.Pornire + ' - ' + t.Oprire AS Traseu, c.ID_TrasaAuto, c.Id_Localitate,c.DataCalatorie
                FROM Curse c
                JOIN TraseeAuto t ON c.ID_TrasaAuto = t.ID_TrasaAuto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView2.DataSource = dataTable;


                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                    dataGridView2.Columns["ID_Cursa"].Visible = false;
                    dataGridView2.Columns["ID_TrasaAuto"].Visible = false;
                    dataGridView2.Columns["Id_Localitate"].Visible = false;


                    if (dataGridView2.Columns["Info"] == null)
                    {
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.Name = "Info";
                        buttonColumn.HeaderText = "Localitati";
                        buttonColumn.Text = "Info";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        dataGridView2.Columns.Add(buttonColumn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la conectarea la baza de date: " + ex.Message);
            }
        }*/

        private void label15_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {

        }

        private void stergebilete_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int idRezervare = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells["ID_Rezervare"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Rezervari WHERE ID_Rezervare = @IdRezervare";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@IdRezervare", idRezervare);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                }

                IncarcaDateRezervari();
            }
            else
            {
                MessageBox.Show("Nu a fost selectată nicio rezervare pentru ștergere.", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void IncarcaDateRezervari()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Rezervari"; 
                    SqlCommand command = new SqlCommand(query, connection);

                    DataTable rezervariTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(rezervariTable);
                    }

                    dataGridView3.DataSource = rezervariTable; 
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            //poti face ca cind selectez un rind si apas pe butonul de stergere sa se stearga acel rind din baza de date si sa se actualizeze datele in datagrid3
        }

        private void actualizarebilete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                string numeCursa = Microsoft.VisualBasic.Interaction.InputBox("Introduceți numele cursei:", "Introducere cursă", "");

                if (!string.IsNullOrWhiteSpace(numeCursa))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = @"
                    SELECT u.NumeUtilizator, u.PrenumeUtilizator, u.Bagaj, u.NumarBilete
                    FROM utilizator u
                    INNER JOIN Curse c ON u.Id_Cursa = c.ID_Cursa
                    WHERE c.Nume_Cursa = @NumeCursa";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NumeCursa", numeCursa);

                        DataTable pasageriTable = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(pasageriTable);
                        }

                        dataGridView4.DataSource = pasageriTable;


                        dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    }
                }
                else
                {
                    MessageBox.Show("Introduceți un nume de cursă valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la afișarea pasagerilor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Trasnporttip_Click(object sender, EventArgs e)
        {
            try
            {
                // Solicităm utilizatorului să introducă numele cursă
                string numeCursa = Microsoft.VisualBasic.Interaction.InputBox("Introduceți numele cursei:", "Introducere cursă", "");

                // Verificăm dacă utilizatorul a introdus un nume de cursă valid
                if (!string.IsNullOrWhiteSpace(numeCursa))
                {
                    // Interogăm baza de date pentru a obține tipul de transport și capacitatea pentru cursa specificată
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT Tip_Transport, Capacitate FROM Curse WHERE Nume_Cursa = @Nume_Cursa";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nume_Cursa", numeCursa);

                        SqlDataReader reader = command.ExecuteReader();

                        dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Verificăm dacă există rezultate pentru cursa specificată
                        if (reader.Read())
                        {
                            string tipTransport = reader["Tip_Transport"].ToString();
                            int capacitate = Convert.ToInt32(reader["Capacitate"]);

                            // Afișăm tipul de transport și capacitatea într-un MessageBox
                            MessageBox.Show($"Cursa {numeCursa} are tipul de transport: {tipTransport} și o capacitate de: {capacitate}", "Informații cursă", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Nu există informații pentru cursa specificată.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Introduceți un nume de cursă valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Eroare la afișarea informațiilor despre cursă: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Solicităm utilizatorului să introducă numele traseului auto
                string numeTraseuAuto = Microsoft.VisualBasic.Interaction.InputBox("Introduceți numele destinatie al traseului auto:", "Introducere traseu auto", "");

                // Verificăm dacă utilizatorul a introdus un nume de traseu auto valid
                if (!string.IsNullOrWhiteSpace(numeTraseuAuto))
                {
                    // Interogăm baza de date pentru a obține lista curselor care aparțin traseului auto specificat
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = @"SELECT C.Nume_Cursa,C.Capacitate,C.Tip_Transport,C.ID_TrasaAuto,C.ID_DataCalatorie
                                 FROM Curse C
                                 JOIN TraseeAuto T ON C.ID_TrasaAuto = T.ID_TrasaAuto
                                 WHERE T.Destinatie_TrasaAuto = @NumeTraseuAuto";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NumeTraseuAuto", numeTraseuAuto);

                        SqlDataReader reader = command.ExecuteReader();

                        dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                        if (reader.HasRows)
                        {
                            DataTable cursuriTable = new DataTable();
                            cursuriTable.Load(reader);
                            dataGridView4.DataSource = cursuriTable;
                        }
                        else
                        {
                            MessageBox.Show("Nu există curse pentru traseul auto specificat.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Introduceți un nume de traseu auto valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la afișarea listei de curse: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // Verificăm dacă pictureBox5 conține o imagine
            if (pictureBox5.Image != null)
            {
                // Deschidem o nouă fereastră pentru a afiaa imaginea în mărimea ei totală
                Form form = new Form();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Size = pictureBox5.Image.Size; 
                form.FormBorderStyle = FormBorderStyle.FixedDialog; 
                form.MaximizeBox = false; 

                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image = pictureBox5.Image;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom; 
                pictureBox.BorderStyle = BorderStyle.Fixed3D; 
                form.Controls.Add(pictureBox); 

                form.ShowDialog(); 
            }
        }


        private int currentIndex = 0;

        private void button5_Click(object sender, EventArgs e)
        {
            currentIndex++;
            LoadMessageAndImage();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Decrementăm indexul curent pentru a afișa mesajul și imaginea anterioare
            currentIndex--;
            LoadMessageAndImage();
        }

        private void LoadMessageAndImage()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Mesaj, Poza FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY Id_Mesaj) AS RowNum FROM mesaj) AS Temp WHERE RowNum = @CurrentIndex";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CurrentIndex", currentIndex);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string mesaj = reader["Mesaj"].ToString();
                        label14.Text = mesaj;
                        label14.Visible = true;

                        if (!reader.IsDBNull(reader.GetOrdinal("Poza")))
                        {
                            byte[] imageData = (byte[])reader["Poza"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                pictureBox5.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            pictureBox5.Image = null;
                        }
                    }
                    else
                    {
                        label14.Visible = false;
                        pictureBox5.Image = null;
                        MessageBox.Show("Nu există mesaje în baza de date.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea mesajului și a pozei: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }


        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2007 (*.xls)|*.xls";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = Excel.DataGridView_To_Datatable(dataGridView1);
                    dt.exportToExcel(openFileDialog.FileName);
                    MessageBox.Show("Data is exported!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        
//buton

//ex

   
}
    }

