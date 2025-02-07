using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

public class IncarcaLocalitati
{
    private string connectionString;

    public IncarcaLocalitati(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<string> GetLocalitatiByTraseu(string traseu)
    {
        List<string> localitatiList = new List<string>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT NumeLocalitate
                    FROM LocalitatiTraseu
                    WHERE ID_TrasaAuto = (
                        SELECT ID_TrasaAuto
                        FROM TraseeAuto
                        WHERE Pornire + ' - ' + Oprire = @traseu
                    )";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@traseu", traseu);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        localitatiList.Add(reader["NumeLocalitate"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Eroare la încărcarea localităților: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return localitatiList;
    }
}
