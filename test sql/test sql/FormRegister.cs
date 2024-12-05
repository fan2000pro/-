using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_sql
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Users (UserName, Password, Role) VALUES (@username, @password, 'User')";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@username", txtUsername.Text),
        new SqlParameter("@password", txtPassword.Text)
            };

            try
            {
                database db = new database();

                db.ExecuteQuery(query, parameters);

                MessageBox.Show("Пользователь успешно зарегистрирован!");

                FormLogin loginForm = new FormLogin();
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            }
        }
    }
    public class database
    {
        string connectionString = @"Server=dbsrv\GOR2024;Database=KulikoviaTest;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False";

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable resultTable = new DataTable();
                try
                {
                    adapter.Fill(resultTable);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка выполнения запроса: {ex.Message}");
                }
                return resultTable;
            }
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка выполнения запроса: {ex.Message}");
                }
            }
        }
    }
}
