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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountMed();
            CountSeller();
            CountCust();
            SumAmt();
            GetSeller();
            GetBestSeller();
            GetBestCustomer();
        }
        SqlConnection con = new SqlConnection(@"Data Source=192.168.43.154;Initial Catalog=Eczane1;User ID=sa;Password=123456");
        private void CountMed()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from MedicineTbl",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Medicinelbl.Text = dt.Rows[0][0].ToString();
            con.Close();

        }

        private void CountSeller()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from SellerTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellLbl.Text = dt.Rows[0][0].ToString();
            con.Close();

        }

        private void CountCust()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from CustomerTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Custlbl.Text = dt.Rows[0][0].ToString();
            con.Close();

        }

        private void SumAmt()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(BAmount) from BillTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellAmt.Text = "Toplam " + dt.Rows[0][0].ToString();
            con.Close();
        }

        private void SumAmtBySell()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(BAmount) from BillTbl where SName='"+SellCbx.SelectedValue.ToString()+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sellsbysellerlbl.Text = "Toplam " + dt.Rows[0][0].ToString();
            con.Close();
        }

        private void GetSeller()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select SName from SellerTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName", typeof(string));
            dt.Load(Rdr);
            SellCbx.ValueMember = "SName";
            SellCbx.DataSource = dt;
            con.Close();
        }

        private void GetBestSeller()
        {
            try
            {
                con.Open();
                string Innerquery = "select Max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(Innerquery,con);
                sda1.Fill(dt1);
                string Query=("select SName from BillTbl where BAmount='"+dt1.Rows[0][0].ToString()+"'");
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                bestsellerlbl.Text = dt.Rows[0][0].ToString();
                con.Close();
               

            }
            catch (Exception ex)
            {
                con.Close();
               
            }
        }

        private void GetBestCustomer()
        {
            try
            {
                con.Open();
                string Innerquery = "select Max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(Innerquery, con);
                sda1.Fill(dt1);
                string Query = ("select CustName from BillTbl where BAmount='" + dt1.Rows[0][0].ToString() + "'");
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                bestcustlbl.Text = dt.Rows[0][0].ToString();
                con.Close();


            }
            catch (Exception ex)
            {
                con.Close();

            }
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SellCbx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SumAmtBySell();
        }

        private void bestsellerlbl_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {
            Customers cus = new Customers();

            cus.ShowDialog();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Manufacturer man = new Manufacturer();

            man.ShowDialog();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Selling sel = new Selling();

            sel.ShowDialog();
            this.Hide();
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sellsbysellerlbl_Click(object sender, EventArgs e)
        {

        }

        private void SellAmt_Click(object sender, EventArgs e)
        {

        }

        private void bestcustlbl_Click(object sender, EventArgs e)
        {

        }

        private void Medicinelbl_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            Medicines med = new Medicines();
            med.ShowDialog();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Sellers sel = new Sellers();
            sel.ShowDialog();
            this.Hide();
        }
    }
}
