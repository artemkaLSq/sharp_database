using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Npgsql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bd
{
    public partial class EmployeeForm : Form
    {
        private int empl;
        private string cs;
        public EmployeeForm(int _empl, string _cs)
        {
            empl = _empl;
            cs = _cs;
            InitializeComponent();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                ShowRequests();
                var sql = "SELECT tariffname, price, megabytesamount, minutesamount, smsamount FROM public." + '"' + "Tariff" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView2.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2) / 1024, reader.GetInt32(3), reader.GetInt32(4));
                                comboBox1.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void ShowRequests()
        {
            dataGridView1.Rows.Clear();
            comboBox2.Items.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT lastname, firstname, patronymic, passport, client_id FROM public." + '"' + "Client" + '"' + $"WHERE client_id IN (SELECT client_id FROM public." + '"' + "Request" + '"' +  $"WHERE employee_id = {empl})";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                                comboBox2.Items.Add(reader.GetInt32(4));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void ShowClients()
        {
            dataGridView3.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT lastname, firstname, patronymic, passport, client_id FROM public." + '"' + "Client" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView3.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text) || !(long.TryParse(textBox2.Text, out _)) || !(textBox2.Text.Length == 11))
                {
                    MessageBox.Show("Введите корректный номер телефона", "Сообщение", MessageBoxButtons.OK);
                }
                else
                {
                    con.Open();
                    var res = -1;
                    var sql = "INSERT INTO public." + '"' + "Contract" + '"' + "(client_id, tariff_id, employee_id, phonenumber) VALUES ((@c),(SELECT tariff_id FROM public." + '"' + "Tariff" + '"' + " WHERE tariffname = (@t)),(@e),(@p))";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("c", Int32.Parse(comboBox2.SelectedItem.ToString()));
                            cmd.Parameters.AddWithValue("t", comboBox1.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("e", empl);
                            cmd.Parameters.AddWithValue("p", textBox2.Text);
                            res = cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                        }
                    }
                    if (res == 1 && long.TryParse(textBox2.Text, out _) && textBox2.Text.Length == 11)
                    {
                        MessageBox.Show("Пользователь добавлен", "Сообщение", MessageBoxButtons.OK);
                        sql = "DELETE FROM public." + '"' + "Request" + '"' + " WHERE client_id = (@c)";
                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("c", Int32.Parse(comboBox2.SelectedItem.ToString()));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ShowRequests();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ShowClients();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = -1;
                var sql = "DELETE FROM public." + '"' + "Contract" + '"' + "WHERE client_id = (@c)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("c", Int32.Parse(textBox3.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                sql = "DELETE FROM public." + '"' + "Client" + '"' + "WHERE client_id = (@c)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("c", Int32.Parse(textBox3.Text));
                    res += cmd.ExecuteNonQuery();
                }
                
                if (res > -1)
                {
                    MessageBox.Show("Пользователь удалён", "Сообщение", MessageBoxButtons.OK);
                    sql = "DELETE FROM public." + '"' + "Request" + '"' + " WHERE client_id = (@c)";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("c", Int32.Parse(textBox3.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }

        }
    }
}
