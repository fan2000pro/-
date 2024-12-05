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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string query = "SELECT Role FROM Users WHERE UserName = @username AND Password = @password";
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@username", txtUsername.Text),
        new SqlParameter("@password", txtPassword.Text)
            };

            database db = new database();

            DataTable result = db.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                string role = result.Rows[0]["Role"].ToString();
                MessageBox.Show($"Добро пожаловать, {txtUsername.Text}!");

                MainForm mainForm = new MainForm(role);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                lblError.Text = "Неверный логин или пароль!";
                lblError.Visible = true;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
        }
    }
}
