using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Emit;
using System.Net.Mail;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Practica
{
    public partial class Form3 : Form
    {

        private string connectionString = "Data Source=DANIEL;Initial Catalog=GaraAuto;Integrated Security=True";
        private TabPage[] tabPages;
        private int currentTabIndex = 0;
        private int idUtilizator;
        private string numeUtilizator;

        
        private int textPosition = 0;
        private Timer timer;
        private int speed = 0;





        private string displayText = "";
        public Form3(string numeUtilizator, int idUtilizator)
        {
            InitializeAnimation();
            InitializeComponent();
            panel3.Visible = false;
            panel2.Visible = false;
            panel8.Visible = false;
            panel9.Visible = false;
            InitializeAnimation();

            panel3.Location = new Point(308, 275);



            this.numeUtilizator = numeUtilizator;
            this.idUtilizator = idUtilizator;


            LoadTrasee();
            label1.Text = $"Salut, Utilizatorul cu ID-ul {idUtilizator}, {numeUtilizator}!";
            panel2.Visible = false;
            combobilet.MaxDropDownItems = 10;
            combobilet.DropDownHeight = combobilet.ItemHeight * 10;

            combotraseu.MaxDropDownItems = 10;
            combotraseu.DropDownHeight = combotraseu.ItemHeight * 10;



            LoadBarcodesForCurrentUser();
        }

        private void LoadBarcodesForCurrentUser()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Barcode FROM Rezervari WHERE Id_Utilizator = @UtilizatorId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UtilizatorId", idUtilizator);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string barcode = reader["Barcode"].ToString();
                        combobarcode.Items.Add(barcode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea codurilor de bare: " + ex.Message);
            }
        }





        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form_Load(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel9.Visible = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string mesaj = textBox1.Text;
            if (string.IsNullOrWhiteSpace(mesaj))
            {
                MessageBox.Show("Te rog să introduci un mesaj.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO mesaj (Mesaj) VALUES (@Mesaj)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Mesaj", mesaj);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Mesajul a fost salvat cu succes în baza de date.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";

                        // Trimite mesajul pe e-mail

                    }
                    else
                    {
                        MessageBox.Show("Eroare la salvarea mesajului în baza de date.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvarea mesajului în baza de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool animationRunning = false; // Variabilă pentru a ține evidența dacă animația rulează sau nu

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (!animationRunning)
            {
                // Pornirea animației
                LoadDataFromDatabase();
                animationRunning = true;
            }
            else
            {
                // Oprește animația
                StopAnimation();
                animationRunning = false;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Pornire, Oprire FROM TraseeAuto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Concatenează informațiile într-un singur șir de caractere cu un anumit separator
                    StringBuilder sb = new StringBuilder();
                    while (reader.Read())
                    {
                        string pornire = reader.GetString(0);
                        string oprire = reader.GetString(1);
                        sb.Append($"{pornire} - {oprire}                         ");
                    }
                    displayText = sb.ToString();

                    // Pornește animația dacă nu rulează deja
                    if (!timer.Enabled)
                    {
                        InitializeAnimation();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la interogarea bazei de date: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool panelVisible = false; // Variabila pentru a urmari starea panelului

        private void button8_Click(object sender, EventArgs e)
        {
            if (panelVisible)
            {
                // Daca panelul este vizibil, îl ascunde
                panel2.Visible = false;
                panelVisible = false;
            }
            else
            {
                // Daca panelul nu este vizibil, il afisează
                panel2.Location = new Point(135, 283);
                panel2.Visible = true;
                panelVisible = true;
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            string traseu = combotraseu.Text;
            string cursa = combocurse.Text;
            string dataCursa = combodata.Text;
            string oraCursa = comboBox1.Text;
            int numarBilete = Convert.ToInt32(combobilet.SelectedItem);

            // Codul adăugat începe aici
            string pretText = pret.Text;
            int pretTotal = 0;

            if (pretText.Contains(":"))
            {
                string[] pretParts = pretText.Split(':');
                if (pretParts.Length > 1)
                {
                    string pretValue = pretParts[1].Trim().Split(' ')[0];
                    pretTotal = Convert.ToInt32(pretValue);
                }
            }
            // Codul adăugat se termină aici

            bool confirmat = checkBox1.Checked ? true : false;
            int barcodeValue = int.Parse(barcode.Text);
            DateTime dataCurenta = DateTime.ParseExact(datacurenta.Text, "HH:mm:ss dd/MM/yyyy", null);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Rezervari (Id_Utilizator, Traseu, Cursa, DataCursa, OraCursa, NumarBilete, PretTotal, Barcode, Data, Confirmat) " +
                                   "VALUES (@Id_Utilizator, @Traseu, @Cursa, @DataCursa, @OraCursa, @NumarBilete, @PretTotal, @Barcode, @Data, @Confirmat)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id_Utilizator", idUtilizator);
                    command.Parameters.AddWithValue("@Traseu", traseu);
                    command.Parameters.AddWithValue("@Cursa", cursa);
                    command.Parameters.AddWithValue("@DataCursa", dataCursa);
                    command.Parameters.AddWithValue("@OraCursa", oraCursa);
                    command.Parameters.AddWithValue("@NumarBilete", numarBilete);
                    command.Parameters.AddWithValue("@PretTotal", pretTotal);
                    command.Parameters.AddWithValue("@Barcode", barcodeValue);
                    command.Parameters.AddWithValue("@Data", dataCurenta);
                    command.Parameters.AddWithValue("@Confirmat", confirmat);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Rezervarea a fost salvată cu succes în baza de date.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (confirmat)
                        {
                            checkBox1.Checked = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Eroare la salvarea rezervării în baza de date.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvarea rezervării în baza de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            combotraseu.SelectedIndex = -1;
            combocurse.SelectedIndex = -1;
            combodata.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            combobilet.SelectedIndex = -1;
            checkBox1.Checked = false;
            pret.Text = "Pret";
        }


        private void combotraseu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combotraseu.SelectedIndex != -1)
            {

                int selectedTraseuId = Convert.ToInt32(combotraseu.SelectedValue);
                LoadCurse(selectedTraseuId);
            }
        }

        private void LoadCurse(int traseuId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT  ID_Cursa, Nume_Cursa FROM Curse WHERE ID_TrasaAuto = @TraseuId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TraseuId", traseuId);
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    combocurse.DisplayMember = "Nume_Cursa";
                    combocurse.ValueMember = "ID_Cursa";
                    combocurse.DataSource = dataTable;


                    combocurse.DropDownStyle = ComboBoxStyle.DropDown;
                    combocurse.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    combocurse.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea curselor: " + ex.Message);
            }
        }


        private void combocurse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combocurse.SelectedIndex != -1)
            {
                string selectedCursa = combocurse.SelectedValue.ToString();

                combodata.Items.Clear();
                comboBox1.Items.Clear();

                LoadDataSiOraCalatorie(selectedCursa);
                int cursaId = Convert.ToInt32(combocurse.SelectedValue);

                LoadBileteDisponibile(cursaId);
            }
        }

        private void LoadBileteDisponibile(int cursaId)
        {
            combobilet.Items.Clear();

            int capacitate = LoadCapacitateCursa(cursaId);
            int bileteRezervate = LoadBileteRezervate(cursaId);
            int bileteDisponibile = capacitate - bileteRezervate;

            for (int i = 1; i <= bileteDisponibile; i++)
            {
                combobilet.Items.Add(i);
            }
        }


        private int LoadCapacitateCursa(int cursaId)
        {
            int capacitate = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Capacitate FROM Curse WHERE ID_Cursa = @CursaId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CursaId", cursaId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        capacitate = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea capacității cursei: " + ex.Message);
            }
            return capacitate;
        }
        private int LoadBileteRezervate(int cursaId)
        {
            int bileteRezervate = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT SUM(NumarBilete) FROM Rezervari WHERE Cursa = (SELECT Nume_Cursa FROM Curse WHERE ID_Cursa = @CursaId)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CursaId", cursaId);
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        bileteRezervate = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea biletelor rezervate: " + ex.Message);
            }
            return bileteRezervate;
        }





        private void LoadDataSiOraCalatorie(string cursaId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string query = "SELECT DC.DataCalatorie, OC.OraCalatorie " +
                                   "FROM Curse C " +
                                   "INNER JOIN DataCalatorie DC ON C.ID_DataCalatorie = DC.ID_DataCalatorie " +
                                   "INNER JOIN OraCalatorie OC ON C.ID_OraCalatorie = OC.ID_OraCalatorie " +
                                   "WHERE C.ID_Cursa = @ID_Cursa";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_Cursa", cursaId);
                    SqlDataReader reader = command.ExecuteReader();


                    combodata.Items.Clear();
                    comboBox1.Items.Clear();

                    while (reader.Read())
                    {
                        string dataCalatorie = reader.GetString(0);
                        combodata.Items.Add(dataCalatorie);

                        string oraCalatorie = reader.GetString(1);
                        comboBox1.Items.Add(oraCalatorie);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea datelor și orelor de călătorie: " + ex.Message);
            }
        }









        private void combodata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combobilet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobilet.SelectedIndex != -1 && combocurse.SelectedIndex != -1)
            {
                int cursaId = Convert.ToInt32(combocurse.SelectedValue);

                int capacitate = LoadCapacitateCursa(cursaId);

                int pretPerBilet = LoadPretCursa(cursaId);

                int numarBilete = Convert.ToInt32(combobilet.SelectedItem);

                int pretTotal = pretPerBilet * numarBilete;

                pret.Text = $"{pretTotal} lei";




            }
        }

        private int LoadPretCursa(int cursaId)
        {
            int pret = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Pret FROM Curse WHERE ID_Cursa = @CursaId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CursaId", cursaId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        pret = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea prețului cursei: " + ex.Message);
            }
            return pret;
        }






        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadTrasee()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ID_TrasaAuto, Pornire + ' - ' + Oprire AS PornireOprire FROM TraseeAuto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    combotraseu.DisplayMember = "PornireOprire";
                    combotraseu.ValueMember = "ID_TrasaAuto";
                    combotraseu.DataSource = dataTable;


                    combotraseu.DropDownStyle = ComboBoxStyle.DropDown;
                    combotraseu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    combotraseu.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea traseelor: " + ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Generarea datelor pentru afișare în panoul panel7
            DateTime dataCurenta = DateTime.Now;
            datacurenta.Text = dataCurenta.ToString("HH:mm:ss dd/MM/yyyy");

            Random random = new Random();
            int cod = random.Next(10000000, 99999999);
            barcode.Text = cod.ToString();

            transport.Text = GetTransportTypeByCursa(combocurse.SelectedValue.ToString());
            traseu.Text = combotraseu.Text;
            cursa.Text = combocurse.Text;
            oracursa.Text = comboBox1.Text;
            datacursa.Text = combodata.Text;
            label19.Text = combobilet.SelectedItem.ToString();
            telefon.Text = GetTelefonByIdUtilizator(idUtilizator);
            label26.Text = pret.Text;

            SavePanelAsImage(panel7);

        }

        private void SavePanelAsImage(Panel panel)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Imagini JPEG|*.jpg|Imagini PNG|*.png|Toate fișierele|*.*";
            saveFileDialog1.Title = "Salvează imaginea";
            saveFileDialog1.FileName = "panel_image";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(panel.Width, panel.Height);

                panel.DrawToBitmap(bmp, new Rectangle(0, 0, panel.Width, panel.Height));

                bmp.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                bmp.Dispose();
            }
        }



        private string GetTransportTypeByCursa(string cursaId)
        {
            string transportType = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Tip_Transport FROM Curse WHERE ID_Cursa = @CursaId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CursaId", cursaId);
                    transportType = command.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea tipului de transport: " + ex.Message);
            }
            return transportType;
        }

        private string GetTelefonByIdUtilizator(int utilizatorId)
        {
            string telefon = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Telefon FROM utilizator WHERE Id_Utilizator = @UtilizatorId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UtilizatorId", utilizatorId);
                    telefon = command.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea telefonului utilizatorului: " + ex.Message);
            }
            return telefon;
        }




        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pret_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void progresbar_Click(object sender, EventArgs e)
        {

        }
        //Bon pentru bilet 
        private void datacurenta_Click(object sender, EventArgs e)
        {

        }

        private void transport_Click(object sender, EventArgs e)
        {

        }

        private void traseu_Click(object sender, EventArgs e)
        {

        }

        private void cursa_Click(object sender, EventArgs e)
        {

        }

        private void oracursa_Click(object sender, EventArgs e)
        {

        }

        private void datacursa_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void telefon_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void barcode_Click(object sender, EventArgs e)
        {


        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel7.Location = new Point(179, 238);
            panel7.Visible = true;
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            // panel7.Location = new Point(153, 238);

        }

        private void email_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            // Hide the panel
            panel8.Visible = false;

            // Clear all TextBox controls within panel8
            foreach (Control control in panel8.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
            }
        }


        private void numarcard_TextChanged(object sender, EventArgs e)
        {
            // Remove spaces to avoid counting them as digits
            string text = numarcard.Text.Replace(" ", "");

            if (text.Length > 0 && text.Length % 4 == 0 && text.Length <= 16)
            {
                numarcard.Text = FormatCardNumber(text);
                numarcard.SelectionStart = numarcard.Text.Length;
                numarcard.SelectionLength = 0;
            }
        }

        private string FormatCardNumber(string cardNumber)
        {
            // Add spaces every 4 digits
            for (int i = 4; i < cardNumber.Length; i += 5)
            {
                cardNumber = cardNumber.Insert(i, " ");
            }
            return cardNumber;
        }


        private void numeprenume_TextChanged(object sender, EventArgs e)
        {

        }

        private void cvc_TextChanged(object sender, EventArgs e)
        {
            // Limit the input to a maximum of 3 characters
            if (cvc.Text.Length > 3)
            {
                // If the text length exceeds 3 characters, truncate it
                cvc.Text = cvc.Text.Substring(0, 3);
                // Set the cursor to the end of the text
                cvc.SelectionStart = cvc.Text.Length;
                cvc.SelectionLength = 0;
            }
        }


        private void data_TextChanged(object sender, EventArgs e)
        {
            // Remove slashes to avoid counting them as digits
            string text = data.Text.Replace("/", "");

            if (text.Length > 0 && text.Length % 2 == 0 && text.Length <= 4)
            {
                data.Text = FormatDate(text);
                data.SelectionStart = data.Text.Length;
                data.SelectionLength = 0;
            }
        }

        private string FormatDate(string date)
        {
            // Add slash every 2 digits
            for (int i = 2; i < date.Length; i += 3)
            {
                date = date.Insert(i, "/");
            }
            return date;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                // Extract data from form controls
                string numarCard = numarcard.Text.Replace(" ", ""); // Remove spaces
                string numePrenume = numeprenume.Text;
                string cvv = cvc.Text;
                string data1 = data.Text.Replace("/", ""); // Remove slashes

                // Encrypt sensitive data
                string numarCardCriptat = HashPassword(numarCard);
                string cvcCriptat = HashPassword(cvv);
                string dataCriptata = HashPassword(data1);

                // Insert data into DetaliiCard table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO DetaliiCard (ID_Utilizator, NumarCardCriptat, NumePrenume, DataCriptata, CVCCriptat) 
                             VALUES (@ID_Utilizator, @NumarCardCriptat, @NumePrenume, @DataCriptata, @CVCCriptat)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_Utilizator", idUtilizator);
                    command.Parameters.AddWithValue("@NumarCardCriptat", numarCardCriptat);
                    command.Parameters.AddWithValue("@NumePrenume", numePrenume);
                    command.Parameters.AddWithValue("@DataCriptata", dataCriptata);
                    command.Parameters.AddWithValue("@CVCCriptat", cvcCriptat);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datele au fost salvate cu succes în tabelul DetaliiCard.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Eroare la salvarea datelor în tabelul DetaliiCard.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvarea datelor în tabelul DetaliiCard: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void combobarcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hashOfEnteredPassword = HashPassword(enteredPassword);
            return StringComparer.OrdinalIgnoreCase.Compare(hashOfEnteredPassword, storedHash) == 0;
        }

        private void StopAnimation()
        {
            // Oprește Timer-ul și resetează poziția textului
            timer.Stop();
            label35.Location = new System.Drawing.Point(-label35.Width, label35.Location.Y);
        }

        private void InitializeAnimation()
        {
            // Inițializează viteza cu valoarea implicită

            // Inițializează și configurează timer-ul pentru animație
            timer = new Timer();
            timer.Interval = 20; // Interval de 100 ms
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            speed = 1;
            textPosition += speed;

            if (textPosition > this.Width)
            {
                textPosition = -label35.Width;
            }

            // Deseneaza textul la noua poziție
            label35.Location = new System.Drawing.Point(textPosition, label35.Location.Y);
        }
        private void label35_Click(object sender, EventArgs e)
        {

        }
        private void LoadDataFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Pornire, Oprire FROM TraseeAuto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Concatenează informațiile într-un singur șir de caractere cu un anumit separator
                    StringBuilder sb = new StringBuilder();
                    while (reader.Read())
                    {
                        string pornire = reader.GetString(0);
                        string oprire = reader.GetString(1);
                        sb.Append($"{pornire} - {oprire}                         ");
                    }
                    displayText = sb.ToString();

                    // Setează textul etichetei cu datele din baza de date
                    label35.Text = displayText;

                    // Pornește animația dacă nu rulează deja
                    if (!timer.Enabled)
                    {
                        InitializeAnimation();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la interogarea bazei de date: " + ex.Message);
            }
        }

        private void trimitere_Click(object sender, EventArgs e)
        {
            string mesaj1 = mesaj.Text;
            if (string.IsNullOrWhiteSpace(mesaj1))
            {
                MessageBox.Show("Te rog să introduci un mesaj.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO mesaj (Mesaj, Poza) VALUES (@Mesaj, @Poza)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Mesaj", mesaj1);

                    // Convert the image to a byte array
                    if (picture.Image != null)
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            picture.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] imageData = stream.ToArray();
                            command.Parameters.AddWithValue("@Poza", imageData);
                        }
                    }
                    else
                    {
                        // If no image is selected, insert NULL into the database
                        command.Parameters.AddWithValue("@Poza", DBNull.Value);
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Mesajul și imaginea au fost salvate cu succes în baza de date.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mesaj.Text = "";
                        picture.Image = null; 
                    }
                    else
                    {
                        MessageBox.Show("Eroare la salvarea mesajului și imaginii în baza de date.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvarea mesajului și imaginii în baza de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cancel1_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            mesaj.Text = "";
        }

        private void Poza_Click(object sender, EventArgs e)
        {
           
        }

        private void picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Imagini (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|Toate fișierele (*.*)|*.*";
            openFileDialog.Title = "Alege o imagine";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Încarcă imaginea selectată în PictureBox
                    picture.Image = new Bitmap(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    // Afișează un mesaj în caz de eroare
                    MessageBox.Show("Eroare la încărcarea imaginii: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }

}


