using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        int i, j;
        double V,I,Q,T1,T2,A,d,k, kv;
        bool changed1, changed2, changed3, changed4, changed5, changedall;
        int[,] table = new int[6, 9];
        Random random = new Random();
        string connectString = "Data Source=Table.db;";
        private void LoadData()
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            T1 = trackBar3.Value;
            label9.Text = T1.ToString() + "°C";
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            SQLiteConnection myConnection = new SQLiteConnection(connectString);

            myConnection.Open();

            string query = "SELECT * FROM \"table\" ORDER BY id";

            SQLiteCommand command = new SQLiteCommand(query, myConnection);

            SQLiteDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[9]);
                data[data.Count - 1][0] = reader[1].ToString();
                data[data.Count - 1][1] = reader[2].ToString();
                data[data.Count - 1][2] = reader[3].ToString();
                data[data.Count - 1][3] = reader[4].ToString();
                data[data.Count - 1][4] = reader[5].ToString();
                data[data.Count - 1][5] = reader[6].ToString();
                data[data.Count - 1][6] = reader[7].ToString();
                data[data.Count - 1][7] = reader[8].ToString();
                data[data.Count - 1][8] = reader[9].ToString();
            }
            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SQLiteConnection myConnection = new SQLiteConnection(connectString);

            myConnection.Open();

            for (i = 0; i < 6; i++)
            {
                for (j = 1; j <= 8; j++)
                {
                    if (j == 1)
                    {
                        V = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    else if (j == 2)
                    {
                        I = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    else if (j == 3)
                    {
                        A = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    else if (j == 4)
                    {
                        d = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    else if (j == 5)
                    {
                        t = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    else if (j == 6)
                    {
                        T1 = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    else if (j == 7)
                    {
                        T2 = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                        if (T2 >= 0 || T2 < 0)
                        {
                            T2 = Math.Round(T2, 2);
                        }
                        else
                        {
                            T2 = 0;
                        }
                    }
                    else if (j == 8)
                    {
                        if(dataGridView1.Rows[i].Cells[j].Value is int || dataGridView1.Rows[i].Cells[j].Value is float || dataGridView1.Rows[i].Cells[j].Value is double)
                        {
                            kv = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                        }
                        else
                        {
                            kv = 0;
                        }

                    }
                }
                string sqlQuery = "UPDATE \"table\" SET napr = @V, sila = @I, ploshad = @A, tolshina = @d, time = @t, temp1 = @T1, temp2 = @T2, kf = @k WHERE id = @Index";
                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, myConnection))
                {
                    command.Parameters.AddWithValue("@V", V);
                    command.Parameters.AddWithValue("@I", I);
                    command.Parameters.AddWithValue("@A", A);
                    command.Parameters.AddWithValue("@d", d);
                    command.Parameters.AddWithValue("@t", t);
                    command.Parameters.AddWithValue("@T1", T1);
                    command.Parameters.AddWithValue("@T2", T2);
                    command.Parameters.AddWithValue("@k", kv);
                    command.Parameters.AddWithValue("@Index", i);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == '-')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked) {
                pictureBox3.Visible = true;
                pictureBox2.Visible = false;
                pictureBox5.Visible = true;
                pictureBox4.Visible = false;
            }
            else
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                pictureBox5.Visible = false;
                pictureBox4.Visible = true;
            }
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            t = trackBar5.Value;
            label12.Text = t.ToString() + " с";
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            changed1 = true;
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            changed3 = true;
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            changed5 = true;
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            changed4 = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            A = trackBar2.Value*0.001;
            label7.Text = A.ToString() + " м²";
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            changed2 = true ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (i = 0; i < 6; i++)
            {
                for (j = 1; j < 9; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = null;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0){
                k = 0.76;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                k = 221;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                k = 0.15;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                k = 1.4;
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                k = 0.24;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                k = 0.16;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            d = trackBar1.Value*0.0001;
            label8.Text = d.ToString() + " м";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(changed1 == true && changed2 == true && changed3 == true && changed4 == true && changed5 == true)
            {
                changedall = true;
            }
            else
            {
                changedall = false;
            }

            if (checkBox1.Checked == true && changedall == true){
                Q = I * V * t;
                T2 = T1 + Q * d / (t * k * A) * (random.NextDouble() * (1.04 - 0.96) + 0.96);
                i = comboBox1.SelectedIndex;
                for (j = 1; j <= 8; j++)
                {
                    if (j == 1)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = V;
                    }
                    else if (j == 2)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = I;
                    }
                    else if (j == 3)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = A;
                    }
                    else if (j == 4)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = d;
                    }
                    else if (j == 5)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = t;
                    }
                    else if (j == 6)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = T1;
                    }
                    else if (j == 7)
                    {
                        if (T2 >= 0 || T2 < 0)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = Math.Round(T2, 2);
                        }
                        else
                        {
                            T2 = 0;
                        }
                    }
                }
                textBox3.Text = T1.ToString() + " °C";
                textBox4.Text = Math.Round(T2, 2).ToString() + " °C";
            }else
            {
                MessageBox.Show("Цепь должна быть замкнута и все данные симуляции должны быть изменены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        double t;
        double R = 250;
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar4.Value.ToString() + " В";
            textBox2.Text = (trackBar4.Value / R).ToString() + " А";
            V = trackBar4.Value;
            I = trackBar4.Value/R;
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
