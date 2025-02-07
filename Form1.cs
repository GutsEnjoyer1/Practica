using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace Practica
{
    public partial class Form1 : Form
    {



        private bool isAdminButtonVisible = false;
        private int GetUserId(string numeUtilizator)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id_Utilizator FROM utilizator WHERE NumeUtilizator = @NumeUtilizator";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeUtilizator", numeUtilizator);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return (int)result;
                    }
                    else
                    {
                        
                        return -1; 
                    }
                }
            }
        }

        private string connectionString = "Data Source=DANIEL;Initial Catalog=GaraAuto;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;

            ParolaU.GotFocus += ParolaU_GotFocus;
            ParolaU.LostFocus += ParolaU_LostFocus;
            this.StartPosition = FormStartPosition.CenterScreen;

            ParolaU.Text = "*******";
            ParolaU.PasswordChar = '*';





            admin.Visible = false;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Space))
            {
                isAdminButtonVisible = !isAdminButtonVisible; // Inversează starea variabilei globale
                admin.Visible = isAdminButtonVisible; // Afișează sau ascunde butonul admin în funcție de starea variabilei globale
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void admin_Click(object sender, EventArgs e)
        {
            if (PromptForAdminPassword())
            {
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Parola de administrator este incorectă.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool PromptForAdminPassword()
        {
            const string correctAdminPassword = "1111";
            var prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Autentificare administrator",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };
            var label = new Label() { Left = 50, Top = 20, Text = "Introduceți parola de administrator:" };
            var textBox = new TextBox() { Left = 50, Top = 50, Width = 200, PasswordChar = '*' };
            var confirmation = new Button() { Text = "OK", Left = 150, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(label);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return textBox.Text == correctAdminPassword;
            }
            return false;
        }



        private void utilizator_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void conect_Click(object sender, EventArgs e)
        {
            string numeUtilizator = NumeU.Text;
            string parolaUtilizator = ParolaU.Text;

            
            int userId = GetUserId(numeUtilizator);

            if (userId != -1)
            {
                if (VerificaUtilizator(numeUtilizator, parolaUtilizator))
                {
                    Form3 form3 = new Form3(numeUtilizator, userId);
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Numele de utilizator sau parola sunt incorecte. Te rugăm să încerci din nou.");
                }
            }
            else
            {
                MessageBox.Show("Numele de utilizator nu există. Te rugăm să încerci din nou sau să te înregistrezi.");
            }
        }



        private bool VerificaUtilizator(string numeUtilizator, string parolaUtilizator)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Parola FROM utilizator WHERE NumeUtilizator = @NumeUtilizator";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeUtilizator", numeUtilizator);

                    string storedHash = cmd.ExecuteScalar() as string;

                    if (storedHash == null)
                    {
                        return false;
                    }

                    return PasswordHelper.VerifyPassword(parolaUtilizator, storedHash);
                }
            }
        }

        private void Inregistrare_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            

        }

        private void NumeU_TextChanged(object sender, EventArgs e)
        {

        }

        private void ParolaU_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ParolaU_GotFocus(object sender, EventArgs e)
        {
            if (ParolaU.Text == "*******")
            {
                ParolaU.Text = "";
                ParolaU.PasswordChar = '*';
            }
        }

        private void ParolaU_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ParolaU.Text))
            {
                ParolaU.Text = "*******";
                ParolaU.PasswordChar = '\0';
            }
        }

        private void inapoi_Click(object sender, EventArgs e)
        {

        }

        private void Inapoi_Click_1(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form2>().Any())
            {
                Form2 form2 = Application.OpenForms.OfType<Form2>().First();
                form2.Close();
            }

            this.Show();
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void Inapoi2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void PrenumeI_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string numeUtilizator = textBox2.Text;
            string prenumeUtilizator = PrenumeI.Text;
            string telefon = textBox3.Text;
            int telefonInt;

            if (!int.TryParse(telefon, out telefonInt))
            {
                MessageBox.Show("Numărul de telefon trebuie să fie un număr valid.");
                return;
            }

            int parolaAleatorie = GenerateRandomPassword();
            InsertUserIntoDatabase(numeUtilizator, prenumeUtilizator, telefonInt, parolaAleatorie);

            DialogResult result = MessageBox.Show($"Utilizatorul a fost înregistrat cu succes. Parola ta este: {parolaAleatorie}. Vrei să mergi la pagina următoare?", "Înregistrare reușită", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                int userId = GetUserId(numeUtilizator); 

                if (userId != -1)
                {
                    Form3 form3 = new Form3( numeUtilizator,userId); 
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Eroare la obținerea ID-ului utilizatorului. Te rugăm să încerci din nou.");
                }
            }

            textBox2.Text = "";
            PrenumeI.Text = "";
            textBox3.Text = "";
        }


        private int GenerateRandomPassword()
        {
            Random random = new Random();
            return random.Next(1000, 10000);
        }

        private void InsertUserIntoDatabase(string numeUtilizator, string prenumeUtilizator, int telefon, int parola)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string hashedPassword = PasswordHelper.HashPassword(parola.ToString());

                string query = "INSERT INTO utilizator (NumeUtilizator, PrenumeUtilizator, Telefon, Parola) " +
                               "VALUES (@NumeUtilizator, @PrenumeUtilizator, @Telefon, @Parola)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeUtilizator", numeUtilizator);
                    cmd.Parameters.AddWithValue("@PrenumeUtilizator", prenumeUtilizator);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Parola", hashedPassword);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
           
        }

        private void Nume_TextChanged(object sender, EventArgs e)
        {

        }

        private void Penume_TextChanged(object sender, EventArgs e)
        {

        }

        private void parolanoua_TextChanged(object sender, EventArgs e)
        {

        }

        private void repetatiparola_TextChanged(object sender, EventArgs e)
        {

        }

        private void SchimbaParola(string numeUtilizator, int telefon, string parolaNoua)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE utilizator SET Parola = @Parola WHERE NumeUtilizator = @NumeUtilizator AND Telefon = @Telefon";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NumeUtilizator", numeUtilizator);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Parola", PasswordHelper.HashPassword(parolaNoua));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Parola a fost schimbată cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nu s-a putut schimba parola. Verifică numele de utilizator și numărul de telefon și încearcă din nou.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void conectareparola_Click(object sender, EventArgs e)
        {
            string numeUtilizator = Nume.Text;
            int telefon;

            if (!int.TryParse(Penume.Text, out telefon))
            {
                MessageBox.Show("Numărul de telefon trebuie să fie un număr valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string parolaNoua = parolanoua.Text;
            string repetareParola = repetatiparola.Text;
            int userId = GetUserId(numeUtilizator);

            if (parolaNoua != repetareParola)
            {
                MessageBox.Show("Parola nouă și repetarea parolei nu coincid. Te rugăm să reîncerci.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SchimbaParola(numeUtilizator, telefon, parolaNoua);

            Nume.Text = "";
            Penume.Text = "";
            parolanoua.Text = "";
            repetatiparola.Text = "";

            Form3 form3 = new Form3(numeUtilizator, userId);
            form3.Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public static class PasswordHelper
    {
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
    }
}
