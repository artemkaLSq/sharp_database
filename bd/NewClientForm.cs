using System;
using System.Collections.Generic;
using System.ComponentModel;
using Npgsql;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bd
{
    public partial class NewClientForm : Form
    {
        private string lastname;
        private string firstname;
        private string cs;
        public NewClientForm(string _name, string _lastname, string _cs)
        {
            cs = _cs;
            firstname = _name;
            lastname = _lastname;
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
                    TariffComboBox.SelectedIndex = 0;
                }
                label7.Text = firstname + " " + lastname;

                sql = "SELECT cityname FROM public." + '"' + "City" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader.GetString(0));
                                comboBox1.SelectedIndex = 0;
                            }
                        }
                    }
                }
                con.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT adress FROM public." + '"' + "Branch" + '"' + " where city_id = (SELECT city_id from public." + '"' + "City" + '"' + $" where cityname = '{comboBox1.SelectedItem.ToString()}')";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                comboBox2.Items.Add(reader.GetString(0));
                                comboBox2.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            var res = 0;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите отчество", "Сообщение", MessageBoxButtons.OK);
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text) || !(long.TryParse(textBox2.Text, out _)) || !(textBox2.Text.Length == 10))
            {
                MessageBox.Show("Введите корректный номер паспорта", "Сообщение", MessageBoxButtons.OK);
            }
            else
            {
                using (var con = new NpgsqlConnection(cs))
                {
                    con.Open();
                    var sql = "INSERT INTO public." + '"' + "Client" + '"' + $"(client_id, lastname, firstname, patronymic, passport, city_id) values (default, '{lastname}', '{firstname}', '{textBox1.Text}', '{textBox2.Text}', (SELECT city_id from public." + '"' + "City" + '"' + $" where cityname = '{comboBox1.SelectedItem.ToString()}') )";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    sql = $"INSERT INTO public." + '"' + "Request" + '"' + "(request_id, client_id, employee_id) values (default, (SELECT client_id from public." + '"' + "Client" + '"' + $" where lastname = '{lastname}' limit 1), (SELECT employee_id from public." + '"' + "Employee" + '"' + $" where branch_id = (SELECT branch_id from public." + '"' + "Branch" + '"' + $" where adress = '{comboBox2.SelectedItem.ToString()}') order by Random() limit 1))";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        res = cmd.ExecuteNonQuery();
                    }
                }
                if (res == 1) MessageBox.Show($"Запрос отправлен. Приходите в салон по адресу {comboBox2.SelectedItem.ToString()}", "Сообщение", MessageBoxButtons.OK);
                else MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                Hide();
            }
        }
    }
}
