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
using System.Diagnostics;

namespace bd
{
    public partial class AdminForm : Form
    {
        string cs;
        public AdminForm(string _cs)
        {
            cs = _cs;
            InitializeComponent();
            ShowStory();
            ShowBranch();
            ShowCity();
            ShowTariffs();
            ShowClients();
            ShowEmployees();
            ShowServices();
            ShowContracts();
            ShowSMS();
            ShowMinutes();
            ShowMegabytes();
            ShowRequests();
        }
        private void ShowStory()
        {
            dataGridView3.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT id, change_type, tablename, change_time, table_id FROM public.story";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView3.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetInt32(4));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void ShowTariffs()
        {
            dataGridView1.Rows.Clear();
            dataGridView5.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT tariffname, price, megabytesamount, minutesamount, smsamount, tariff_id FROM public." + '"' + "Tariff" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2) / 1024, reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                sql = "SELECT tariffname, price, megabytesamount, minutesamount, smsamount, tariff_id FROM public.tariff_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView5.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2) / 1024, reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddTariff()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "Tariff" + '"' + "(tariffname, price, megabytesamount, minutesamount, smsamount, tariff_id) VALUES ((@n),(@p),(@mg),(@min),(@sms),default)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", textBox1.Text);
                        cmd.Parameters.AddWithValue("p", Int32.Parse(textBox2.Text));
                        cmd.Parameters.AddWithValue("mg", Int32.Parse(textBox3.Text));
                        cmd.Parameters.AddWithValue("min", Int32.Parse(textBox4.Text));
                        cmd.Parameters.AddWithValue("sms", Int32.Parse(textBox5.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                       MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Тариф добавлен", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }

        }
        private void DeleteTariff()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "Tariff" + '"' + "WHERE tariff_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox6.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Тариф удалён", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowCity()
        {
            dataGridView2.Rows.Clear();
            dataGridView4.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT cityname, city_id FROM public." + '"' + "City" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView2.Rows.Add(reader.GetString(0), reader.GetInt32(1));
                            }
                        }
                    }
                }
                sql = "SELECT cityname, city_id FROM public.city_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView4.Rows.Add(reader.GetString(0), reader.GetInt32(1));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddCity()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "City" + '"' + "(cityname, city_id) VALUES ((@n), default)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", textBox7.Text);
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                   
                }
                if (res > 0)
                {
                    MessageBox.Show("Город добавлен", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }

        }
        private void DeleteCity()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "City" + '"' + "WHERE cityname = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", textBox8.Text);
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Город удалён", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowBranch()
        {
            dataGridView6.Rows.Clear();
            dataGridView7.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT adress, city_id, phonenumber, branch_id FROM public." + '"' + "Branch" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView6.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
                            }
                        }
                    }
                }
                sql = "SELECT adress, city_id, phonenumber, branch_id FROM public.branch_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView7.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddBranch()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "Branch" + '"' + "(adress, city_id, phonenumber, branch_id) VALUES ((@a), (@c), (@p), default)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("a", textBox11.Text);
                        cmd.Parameters.AddWithValue("c", Convert.ToInt32(textBox12.Text));
                        cmd.Parameters.AddWithValue("p", textBox13.Text);
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Филиал добавлен", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }

        }
        private void DeleteBranch()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "Branch" + '"' + "WHERE branch_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox10.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Филиал удалён", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowClients()
        {
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT lastname, firstname, patronymic, passport, city_id, client_id FROM public." + '"' + "Client" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                sql = "SELECT lastname, firstname, patronymic, passport, city_id,client_id FROM public.client_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView9.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddClient()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "Client" + '"' + "(client_id, lastname, firstname, patronymic, passport, city_id) VALUES (default, (@l), (@f), (@p),(@pass),(@c))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("l", textBox14.Text);
                        cmd.Parameters.AddWithValue("f", textBox15.Text);
                        cmd.Parameters.AddWithValue("p", textBox16.Text);
                        cmd.Parameters.AddWithValue("pass", textBox17.Text);
                        cmd.Parameters.AddWithValue("c", Convert.ToInt32(textBox18.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Клиент добавлен", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteClient()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "Client" + '"' + "WHERE client_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox19.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Клиент удалён", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowEmployees()
        {
            dataGridView11.Rows.Clear();
            dataGridView10.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT lastname, firstname, patronymic, passport, branch_id, employee_id FROM public." + '"' + "Employee" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView11.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                sql = "SELECT lastname, firstname, patronymic, passport, branch_id, employee_id FROM public.employee_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView10.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddEmployee()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "Employee" + '"' + "(employee_id, lastname, firstname, patronymic, passport, branch_id) VALUES (default, (@l), (@f), (@p),(@pass),(@c))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("l", textBox25.Text);
                        cmd.Parameters.AddWithValue("f", textBox24.Text);
                        cmd.Parameters.AddWithValue("p", textBox23.Text);
                        cmd.Parameters.AddWithValue("pass", textBox22.Text);
                        cmd.Parameters.AddWithValue("c", Convert.ToInt32(textBox21.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Продавец добавлен", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteEmployee()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "Employee" + '"' + "WHERE employee_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox20.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Продавец удалён", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowContracts()
        {
            dataGridView13.Rows.Clear();
            dataGridView12.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT tariff_id, client_id,  employee_id, phonenumber, addservices_id, contract_id FROM public." + '"' + "Contract" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView13.Rows.Add(reader.GetInt32(0), reader.GetInt32(1), reader.IsDBNull(2) ? 0 : reader.GetInt32(2), reader.GetString(3), reader.IsDBNull(4) ? 0 : reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                sql = "SELECT tariff_id, client_id,  employee_id, phonenumber, addservices_id, contract_id FROM public.contract_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView12.Rows.Add(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.IsDBNull(4) ? 0 : reader.GetInt32(4), reader.GetInt32(5));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddContract()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "Contract" + '"' + "(tariff_id, client_id,  employee_id, phonenumber, addservices_id, contract_id) VALUES ((@t), (@c), (@e),(@p),(@a),default)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("t", Convert.ToInt32(textBox31.Text));
                        cmd.Parameters.AddWithValue("c", Convert.ToInt32(textBox30.Text));
                        cmd.Parameters.AddWithValue("e", Convert.ToInt32(textBox29.Text));
                        cmd.Parameters.AddWithValue("p", textBox28.Text);
                        cmd.Parameters.AddWithValue("a", Convert.ToInt32(textBox27.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Продавец добавлен", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteContract()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "Contract" + '"' + "WHERE contract_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox26.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Контракт удалён", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowServices()
        {
            dataGridView15.Rows.Clear();
            dataGridView14.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT addservices_id, addminutes_id, addmegabytes_id, addsms_id FROM public." + '"' + "AddServices" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView15.Rows.Add(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                sql = "SELECT addservices_id, addminutes_id, addmegabytes_id, addsms_id FROM public.addservices_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView14.Rows.Add(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddServices()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "AddServices" + '"' + "(addservices_id, addminutes_id, addmegabytes_id, addsms_id) VALUES (default, (@min), (@meg), (@sms))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("min", Convert.ToInt32(textBox35.Text));
                        cmd.Parameters.AddWithValue("sms", Convert.ToInt32(textBox32.Text));
                        cmd.Parameters.AddWithValue("meg", Convert.ToInt32(textBox33.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Услуги добавлены", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteServices()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "AddServices" + '"' + "WHERE addservices_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox34.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Услуги удалены", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowSMS()
        {
            dataGridView16.Rows.Clear();
            dataGridView17.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT addsms_id, servicename, amount, price FROM public." + '"' + "AddSMS" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView17.Rows.Add(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                sql = "SELECT addsms_id, servicename, amount, price FROM public.addsms_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView16.Rows.Add(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddSMS()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "AddSMS" + '"' + "(addsms_id, servicename, amount, price) VALUES (default, (@n), (@a), (@p))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", textBox38.Text);
                        cmd.Parameters.AddWithValue("a", Convert.ToInt32(textBox37.Text));
                        cmd.Parameters.AddWithValue("p", Convert.ToInt32(textBox36.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Услуга добавлена", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteSMS()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "AddSMS" + '"' + "WHERE addsms_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox39.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Услуга удалена", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowMinutes()
        {
            dataGridView19.Rows.Clear();
            dataGridView18.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT addminutes_id, servicename, amount, price FROM public." + '"' + "AddMinutes" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView19.Rows.Add(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                sql = "SELECT addminutes_id, servicename, amount, price FROM public.addminutes_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView18.Rows.Add(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddMinutes()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "AddMinutes" + '"' + "(addminutes_id, servicename, amount, price) VALUES (default, (@n), (@a), (@p))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", textBox42.Text);
                        cmd.Parameters.AddWithValue("a", Convert.ToInt32(textBox41.Text));
                        cmd.Parameters.AddWithValue("p", Convert.ToInt32(textBox40.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Услуга добавлена", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteMinutes()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "AddMinutes" + '"' + "WHERE addminutes_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox43.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Услуга удалена", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowMegabytes()
        {
            dataGridView21.Rows.Clear();
            dataGridView20.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT addmegabytes_id, servicename, amount, price FROM public." + '"' + "AddMegabytes" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView21.Rows.Add(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                sql = "SELECT addmegabytes_id, servicename, amount, price FROM public.addmegabytes_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView20.Rows.Add(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(0));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddMegabytes()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "AddMegabytes" + '"' + "(addmegabytes_id, servicename, amount, price) VALUES (default, (@n), (@a), (@p))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", textBox46.Text);
                        cmd.Parameters.AddWithValue("a", Convert.ToInt32(textBox45.Text));
                        cmd.Parameters.AddWithValue("p", Convert.ToInt32(textBox44.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Услуга добавлена", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteMegabytes()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "AddMegabytes" + '"' + "WHERE addmegabytes_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox47.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Услуга удалена", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void ShowRequests()
        {
            dataGridView23.Rows.Clear();
            dataGridView22.Rows.Clear();
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var sql = "SELECT request_id, client_id, employee_id FROM public." + '"' + "Request" + '"';
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView23.Rows.Add(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(0));
                            }
                        }
                    }
                }
                sql = "SELECT request_id, client_id, employee_id FROM public.request_temp";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView22.Rows.Add(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(0));
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void AddRequest()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "INSERT INTO public." + '"' + "Request" + '"' + "(request_id, client_id, employee_id) VALUES (default, (@c), (@e))";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("c", Convert.ToInt32(textBox49.Text));
                        cmd.Parameters.AddWithValue("e", Convert.ToInt32(textBox50.Text));
                        res = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }
                }
                if (res > 0)
                {
                    MessageBox.Show("Запрос добавлен", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteRequest()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public." + '"' + "Request" + '"' + "WHERE request_id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox48.Text));
                        res += cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                    }

                }
                if (res > 0)
                {
                    MessageBox.Show("Запрос удален", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteStory()
        {
            if (!string.IsNullOrWhiteSpace(textBox9.Text))
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var res = 0;
                var sql = "DELETE FROM public.story WHERE id = (@n)";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("n", Convert.ToInt32(textBox9.Text));
                        try
                        {
                            res += cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                           MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                        }
                    }
                if (res > 0)
                {
                    MessageBox.Show("Операция отмене" +
                        "на", "Сообщение", MessageBoxButtons.OK);
                }
                con.Close();
            }
        }
        private void DeleteStoryTime()
        {
            var time1 = dateTimePicker1.Value;
            var time2 = dateTimePicker2.Value;
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dataGridView3.Rows[i].Cells[3].Value.ToString()) <= time2 && Convert.ToDateTime(dataGridView3.Rows[i].Cells[3].Value.ToString()) >= time1)
                {
                    using (var con = new NpgsqlConnection(cs))
                    {
                        con.Open();
                        var res = 0;
                        var sql = "DELETE FROM public.story WHERE id = (@n)";
                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("n", Convert.ToInt32(dataGridView3.Rows[i].Cells[0].Value.ToString()));
                            try
                            {
                                res += cmd.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Ошибка", "Сообщение", MessageBoxButtons.OK);
                            }
                        }
                        if (res > 0)
                        {
                            MessageBox.Show($"Операция {Convert.ToInt32(dataGridView3.Rows[i].Cells[0].Value.ToString())} отменена", "Сообщение", MessageBoxButtons.OK);
                        }
                        con.Close();
                    }

                }
            }
        }
        private void Create_Copy()
        {
            string d = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString()
            + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"C:\Program Files\PostgreSQL\10\bin\";
            p.StartInfo.Arguments = "/k pg_dump.exe --file \"C:\\Users\\Stas\\Desktop\\" + d + ".sql\" --host \"localhost\" --port \"5432\" --username \"postgres\" --verbose --format=c --blobs --inserts \"kurs\"";
            p.Start();
            p.Close();
        }
        private void Load_Copy()
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "SQL file (*.sql)|*.sql|All files(*.*)|*.*";
            string pathdb = "";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            pathdb = openFileDialog1.FileName;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"C:\Program Files\PostgreSQL\10\bin\";
            p.StartInfo.Arguments = "/k pg_restore.exe --host \"localhost\" --port \"5432\" --username \"postgres\" --role \"postgres\" --dbname \"kurs\" --verbose \"" + pathdb + "\"";
            p.Start();
            p.Close();
        }
        private void button1_Click(object sender, EventArgs e) => ShowTariffs();
        private void button2_Click(object sender, EventArgs e) => AddTariff();
        private void button3_Click(object sender, EventArgs e) => DeleteTariff();
        private void button6_Click(object sender, EventArgs e) => ShowCity();
        private void button4_Click(object sender, EventArgs e) => AddCity();
        private void button5_Click(object sender, EventArgs e) => DeleteCity();
        private void button8_Click(object sender, EventArgs e) => DeleteStory();
        private void button7_Click(object sender, EventArgs e) => ShowStory();
        private void button9_Click(object sender, EventArgs e) => ShowBranch();
        private void button10_Click(object sender, EventArgs e) => DeleteBranch();
        private void button11_Click(object sender, EventArgs e) => AddBranch();
        private void button12_Click(object sender, EventArgs e) => ShowClients();
        private void button13_Click(object sender, EventArgs e) => AddClient();
        private void button14_Click(object sender, EventArgs e) => DeleteClient();
        private void button17_Click(object sender, EventArgs e) => ShowEmployees();
        private void button16_Click(object sender, EventArgs e) => AddEmployee();
        private void button15_Click(object sender, EventArgs e) => DeleteEmployee();
        private void button20_Click(object sender, EventArgs e) => ShowContracts();
        private void button18_Click(object sender, EventArgs e) => DeleteContract();
        private void button19_Click(object sender, EventArgs e) => AddContract();
        private void button21_Click(object sender, EventArgs e) => ShowServices();        
        private void button22_Click(object sender, EventArgs e) => DeleteServices();
        private void button23_Click(object sender, EventArgs e) => AddServices();
        private void button24_Click(object sender, EventArgs e) => ShowSMS();
        private void button26_Click(object sender, EventArgs e) => AddSMS();
        private void button25_Click(object sender, EventArgs e) => DeleteSMS();
        private void button27_Click(object sender, EventArgs e) => ShowMinutes();
        private void button29_Click_1(object sender, EventArgs e) => AddMinutes();
        private void button28_Click(object sender, EventArgs e) => DeleteMinutes();
        private void button30_Click(object sender, EventArgs e) => ShowMegabytes();
        private void button32_Click(object sender, EventArgs e) => AddMegabytes();
        private void button31_Click(object sender, EventArgs e) => DeleteMegabytes();
        private void button33_Click(object sender, EventArgs e) => ShowRequests();
        private void button35_Click(object sender, EventArgs e) => AddRequest();
        private void button34_Click(object sender, EventArgs e) => DeleteRequest();
        private void button36_Click(object sender, EventArgs e) => DeleteStoryTime();
        private void button37_Click(object sender, EventArgs e) => Create_Copy();
        private void button38_Click(object sender, EventArgs e) => Load_Copy();
    }
}
