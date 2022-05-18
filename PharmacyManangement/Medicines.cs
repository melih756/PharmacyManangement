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
    public partial class Medicines : Form
    {
        public Medicines()
        {
            InitializeComponent();
            ShowMed();
            GetManufacturer();
        }

        SqlConnection con = new SqlConnection(@"Data Source=192.168.43.154;Initial Catalog=Eczane1;User ID=sa;Password=123456");
        private void ShowMed()
        {
            con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedDTGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Reset()
        {
            MedManName.Text = "";
            MedName.Text = "";
            MedPrice.Text = "";
            MedQty.Text = "";
            MedCbx.SelectedIndex = 0;
            key = 0;    
        }

        private void GetManufacturer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select ManId from ManufacturerTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ManId", typeof(int));
            dt.Load(Rdr);
            MedManCbx.ValueMember = "ManId";
            MedManCbx.DataSource = dt;
            con.Close();
        }

        private void GetManName()
        {
            con.Open();
            string Query = "Select * from ManufacturerTbl where ManId= " + MedManCbx.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query,con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                MedManName.Text = dr["ManName"].ToString();

            }
            con.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void MedSaveBtn_Click(object sender, EventArgs e)
        {
            if (MedName.Text == "" || MedPrice.Text == "" || MedQty.Text == "" || MedCbx.SelectedIndex == -1 || MedManName.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MedicineTbl(MedName,MedType,MedQty,MedPrice,MedManId,MedManufact)values(@MN,@MT,@MQ,@MP,@MMI,@MMF)", con);
                    cmd.Parameters.AddWithValue("@MN", MedName.Text);
                    cmd.Parameters.AddWithValue("@MT", MedCbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQty.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPrice.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCbx.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MMF", MedManName.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Added");
                    con.Close();
                    ShowMed();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void MedManCbx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManName();
        }
        int key = 0;
        private void MedDTGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedName.Text = MedDTGV.SelectedRows[0].Cells[1].Value.ToString();
            MedCbx.SelectedItem = MedDTGV.SelectedRows[0].Cells[2].Value.ToString();
            MedQty.Text = MedDTGV.SelectedRows[0].Cells[3].Value.ToString();
            MedPrice.Text = MedDTGV.SelectedRows[0].Cells[4].Value.ToString();
            MedManCbx.SelectedValue = MedDTGV.SelectedRows[0].Cells[5].Value.ToString();
            MedManName.Text = MedDTGV.SelectedRows[0].Cells[6].Value.ToString();
            if (MedName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MedDTGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void MedDeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Medicine");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from MedicineTbl where MedNum=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Deleted");
                    con.Close();
                    ShowMed();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void MedEditBtn_Click(object sender, EventArgs e)
        {
            if (MedName.Text == "" || MedPrice.Text == "" || MedQty.Text == "" || MedCbx.SelectedIndex == -1 || MedManName.Text == "")
            {
                MessageBox.Show("Missing İnformation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedName=@MN,MedType=@MT,MedQty=@MQ,MedPrice=@MP,MedManId=@MMI,MedManufact=@MMF where MedNum=@MKey", con);
                    cmd.Parameters.AddWithValue("@MN", MedName.Text);
                    cmd.Parameters.AddWithValue("@MT", MedCbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQty.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPrice.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCbx.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MMF", MedManName.Text);
                    cmd.Parameters.AddWithValue("@MKey",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Updated");
                    con.Close();
                    ShowMed();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        

        private void MedName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Medicines_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();

            dash.ShowDialog();
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

        private void label20_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label27_Click(object sender, EventArgs e)
        {
            Sellers sel = new Sellers();
            sel.ShowDialog();
            this.Hide();
        }
    }
}
