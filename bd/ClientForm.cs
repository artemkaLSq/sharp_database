using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bd
{
    public partial class ClientForm : Form
    {
        private int client;
        private string cs;
        public ClientForm(int _client, string _cs)
        {
            client = _client;
            cs = _cs;
            InitializeComponent();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT tariffname, price, megabytesamount, minutesamount, smsamount FROM public." + '"' + "Tariff" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TariffGridView.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2) / 1024, reader.GetInt32(3), reader.GetInt32(4));
                                TariffComboBox.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                sql = "SELECT lastname, firstname, patronymic FROM public." + '"' + "Client" + '"' + $"WHERE client_id = {client}";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                label7.Text = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2);
                            }
                        }
                    }
                }
                sql = "SELECT cityname FROM public." + '"' + "City" + '"' + $"WHERE city_id = (SELECT city_id FROM public." + '"' + "Client" + '"' + $"WHERE client_id = {client})";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                label8.Text = reader.GetString(0);
                            }
                        }
                    }
                }
                con.Close();
                ShowMyAdds();
                ShowAdds();
                if (ShowTariff() == -1)
                {
                    new LoginForm(cs).Show();
                    Hide();
                }
                else
                {
                    SumPrice();
                }
            }
        }
        private int ShowTariff()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT tariffname, price, megabytesamount, minutesamount, smsamount FROM public." + '"' + "Tariff" + '"' + "WHERE tariff_id = (SELECT tariff_id FROM public." + '"' + "Contract" + '"' + $"WHERE client_id = {client})";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                NameTextBox.Text = $"{reader.GetString(0)}";
                                PriceTextBox.Text = $"{reader.GetInt32(1)}";
                                GBTextBox.Text = $"{reader.GetInt32(2) / 1024}";
                                MinutesTextBox.Text = $"{reader.GetInt32(3)}";
                                SMSTextBox.Text = $"{reader.GetInt32(4)}";
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Приходите в выбранное отделение для оформления контракта!", "Сообщение", MessageBoxButtons.OK);
                            con.Close();
                            return -1;
                        }
                    }
                }
                TariffComboBox.SelectedIndex = 0;
                con.Close();
                return 1;
            }

        }
        private void ChangeTariff(string tariff)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = $"UPDATE public." + '"' + "Contract" + '"' + "SET tariff_id = (SELECT tariff_id from public." + '"' + "Tariff" + '"' + $"where tariffname = (@p)) WHERE client_id = {client};";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("p", tariff);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        private void ChangeAdds()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = $"INSERT INTO public." + '"' + "AddServices" + '"' + "(addservices_id, addmegabytes_id, addminutes_id, addsms_id) values (default, (SELECT addmegabytes_id from public." + '"' + "AddMegabytes" + '"' + $" where servicename = (@g)), (SELECT addminutes_id from public." + '"' + "AddMinutes" + '"' + $" where servicename = (@m)),(SELECT addsms_id from public." + '"' + "AddSMS" + '"' + $" where servicename = (@s)))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("g", comboBox1.SelectedItem);
                    cmd.Parameters.AddWithValue("m", comboBox2.SelectedItem);
                    cmd.Parameters.AddWithValue("s", comboBox3.SelectedItem);
                    cmd.ExecuteNonQuery();
                }
                sql = $"UPDATE public." + '"' + "Contract" + '"' + " SET addservices_id = (SELECT addservices_id from public." + '"' + "AddServices" + '"' + $" where (addmegabytes_id = (SELECT addmegabytes_id from public." + '"' + "AddMegabytes" + '"' + $" where servicename = (@g)) AND addminutes_id = (SELECT addminutes_id from public." + '"' + "AddMinutes" + '"' + $" where servicename = (@m)) AND addsms_id = (SELECT addsms_id from public." + '"' + "AddSMS" + '"' + $" where servicename = (@s)))limit 1) where client_id= (@i)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("g", comboBox1.SelectedItem);
                    cmd.Parameters.AddWithValue("m", comboBox2.SelectedItem);
                    cmd.Parameters.AddWithValue("s", comboBox3.SelectedItem);
                    cmd.Parameters.AddWithValue("i", client);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        private void ShowAdds()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT servicename, amount, price FROM public." + '"' + "AddMinutes" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                comboBox2.Items.Add(reader.GetString(0));
                                dataGridView2.Rows.Add(reader.GetString(0), reader.GetInt32(2), reader.GetInt32(1));
                            }
                        }
                    }
                }
                comboBox2.SelectedIndex = 0;
                sql = "SELECT servicename, amount, price FROM public." + '"' + "AddMegabytes" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader.GetString(0));
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetInt32(2), reader.GetInt32(1));
                            }
                        }
                    }
                }
                comboBox1.SelectedIndex = 0;
                sql = "SELECT servicename, amount, price FROM public." + '"' + "AddSMS" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                comboBox3.Items.Add(reader.GetString(0));
                                dataGridView3.Rows.Add(reader.GetString(0), reader.GetInt32(2), reader.GetInt32(1));
                            }
                        }
                    }
                }
                comboBox3.SelectedIndex = 0;
                con.Close();
            }
        }
        private void ShowMyAdds()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT servicename, amount, price FROM public."+'"'+"AddMinutes"+'"'+ "where addminutes_id =(SELECT addminutes_id FROM public." + '"' + "AddServices" + '"' + "WHERE addservices_id = (SELECT addservices_id FROM public." + '"' + "Contract" + '"' + $"WHERE client_id = {client}))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBox2.Text = $"{reader.GetString(0)}";
                                textBox5.Text = $"{reader.GetInt32(2)}";
                                textBox8.Text = $"{reader.GetInt32(1)}";
                            }
                        }
                    }
                }
                sql = "SELECT servicename, amount, price FROM public." + '"' + "AddMegabytes" + '"' + "where addmegabytes_id =(SELECT addmegabytes_id FROM public." + '"' + "AddServices" + '"' + "WHERE addservices_id = (SELECT addservices_id FROM public." + '"' + "Contract" + '"' + $"WHERE client_id = {client}))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBox1.Text = $"{reader.GetString(0)}";
                                textBox4.Text = $"{reader.GetInt32(2)}";
                                textBox7.Text = $"{reader.GetInt32(1)}";
                            }
                        }
                    }
                }
                sql = "SELECT servicename, amount, price FROM public." + '"' + "AddSMS" + '"' + "where addsms_id =(SELECT addsms_id FROM public." + '"' + "AddServices" + '"' + "WHERE addservices_id = (SELECT addservices_id FROM public." + '"' + "Contract" + '"' + $"WHERE client_id = {client}))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBox3.Text = $"{reader.GetString(0)}";
                                textBox6.Text = $"{reader.GetInt32(2)}";
                                textBox9.Text = $"{reader.GetInt32(1)}";
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void SumPrice()
        {
            int s = string.IsNullOrWhiteSpace(textBox6.Text) ? 0 : Convert.ToInt32(textBox6.Text);
            int min = string.IsNullOrWhiteSpace(textBox5.Text) ? 0 : Convert.ToInt32(textBox5.Text);
            int meg = string.IsNullOrWhiteSpace(textBox4.Text) ? 0 : Convert.ToInt32(textBox4.Text);
            textBox10.Text = (meg+min+s+Convert.ToInt32(PriceTextBox.Text)).ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            ChangeTariff(TariffComboBox.SelectedItem.ToString());
            ShowTariff();
            SumPrice();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            ChangeAdds();
            ShowMyAdds();
            SumPrice();
        }
    }
}
