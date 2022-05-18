using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PharmacyManangement
{
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowMan();
        }
        SqlConnection con = new SqlConnection(@"Data Source=192.168.43.154;Initial Catalog=Eczane1;User ID=sa;Password=123456");
        private void ShowMan()
        {
            con.Open();
            string Query = "Select * from ManufacturerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MDTGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ManAdd.Text == "" || ManName.Text == "" || ManPhone.Text == "")
            {
                MessageBox.Show("eksik bilgi");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ManufacturerTbl(ManName,ManAdd,ManPhone,ManJDate)values(@MN,@MA,@MP,@MJD)", con);
                    cmd.Parameters.AddWithValue("@MN", ManName.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAdd.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhone.Text);
                    cmd.Parameters.AddWithValue("@MJD", MJDT.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("üretici eklendi");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;

        private void MDTGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ManName.Text = MDTGV.SelectedRows[0].Cells[1].Value.ToString();
            ManAdd.Text = MDTGV.SelectedRows[0].Cells[2].Value.ToString();
            ManPhone.Text = MDTGV.SelectedRows[0].Cells[3].Value.ToString();
            MJDT.Text = MDTGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ManName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MDTGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("üretici seçiniz");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ManufacturerTbl where ManId=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("üretici silindi");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Reset()
        {
            ManName.Text = "";
            ManAdd.Text = "";
            ManPhone.Text = "";
            key = 0;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ManAdd.Text == "" || ManName.Text == "" || ManPhone.Text == "")
            {
                MessageBox.Show("eksik bilgi");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update  ManufacturerTbl set ManName=@MN,ManAdd=@MA,ManPhone=@MP,ManJDate=@MJD where ManId=@MKey", con);
                    cmd.Parameters.AddWithValue("@MN", ManName.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAdd.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhone.Text);
                    cmd.Parameters.AddWithValue("@MJD", MJDT.Value.Date);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Üretici güncellendi");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Dashboard ana = new Dashboard();
            ana.ShowDialog();
            this.Hide();
        }

        private void label16_Click_1(object sender, EventArgs e)
        {
            Medicines med = new Medicines();

            med.ShowDialog();
            this.Hide();
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            Customers cus = new Customers();

            cus.ShowDialog();
            this.Hide();
        }

        private void label18_Click_1(object sender, EventArgs e)
        {
            Manufacturer man = new Manufacturer();

            man.ShowDialog();
            this.Hide();
        }

        private void label19_Click_1(object sender, EventArgs e)
        {
            Selling sel = new Selling();

            sel.ShowDialog();
            this.Hide();

        }

        private void Manufacturer_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Sellers sel = new Sellers();
            sel.ShowDialog();
            this.Hide();
        }
    }
    }


