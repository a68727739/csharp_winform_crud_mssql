using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-H73555T\\SQLEXPRESS;Initial Catalog=Test01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobiles
            (First,Last,Mobile,Email,Category)
        VALUES  ('" + textBox1.Text + "','"
        + textBox2.Text + "','"
        + textBox3.Text + "','"
        + textBox4.Text + "','"
        + comboBox1.Text + "')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("SuccessFully Saved...!");
            Display();
        }
        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Mobiles", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[0].Visible = false;
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Last"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Mobile"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Email"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Category"].ToString();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Display();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Mobiles 
            WHERE (Mobile='"+textBox3.Text+"')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Delete SuccessFully ...!");
            Display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE Mobiles 
            SET First='"+textBox1.Text+"',Last='"+textBox2.Text+"',Mobile='"+textBox3.Text+"',Email='"+textBox4.Text + "',Category='"+comboBox1.Text + "' WHERE (Mobile='" + textBox3.Text + "')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Update SuccessFully ...!");
            Display();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter(
                "Select * from Mobiles WHERE (Mobile like '"
                +textBox5.Text+"%') or" +
                " (First like '%"+textBox5.Text+"%') or (Last like '%" 
                + textBox5.Text + "%') ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[0].Visible = false;
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }
    }
}
