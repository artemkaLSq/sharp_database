using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bd
{
    public partial class LoginForm : Form
    {
        private string cs;
        public LoginForm(string _cs)
        {
            InitializeComponent();
            UserNotFound.Hide();
            cs = _cs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                switch (UserComboBox.SelectedItem)
                {
                    case "Клиент":
                        int client = -1;
                        var sql = "SELECT client_id FROM public." + '"' + "Client" + '"' + $"WHERE firstname = (@f) AND lastname = (@l)";
                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("f", textBox1.Text);
                            cmd.Parameters.AddWithValue("l", textBox2.Text);
                            using (var reader = cmd.ExecuteReader())
                            {

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        client = reader.GetInt32(0);
                                    }
                                }
                            }
                        }
                        if (client != -1)
                        {
                            new ClientForm(client, cs).Show();
                            
                        }
                        else
                        {
                            new NewClientForm(textBox1.Text, textBox2.Text, cs).Show();
                        }
                        break;
                    case "Продавец":
                        int empl = -1;
                        using (var cmd = new NpgsqlCommand("SELECT employee_id FROM public." + '"' + "Employee" + '"' + $"WHERE firstname = (@f) AND lastname = (@l)", con))
                        {
                            cmd.Parameters.AddWithValue("f", textBox1.Text);
                            cmd.Parameters.AddWithValue("l", textBox2.Text);
                            using (var reader = cmd.ExecuteReader())
                            {

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        empl = reader.GetInt32(0);
                                    }
                                }
                                else
                                {
                                    UserNotFound.Show();
                                }
                            }
                        }
                        if (empl != -1) new EmployeeForm(empl, cs).ShowDialog();
                        break;
                    case "Админ":
                        if (textBox1.Text == "admin" && textBox2.Text == "admin")
                        {
                            new AdminForm(cs).ShowDialog();
                        }
                        else
                        {
                            UserNotFound.Show();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
