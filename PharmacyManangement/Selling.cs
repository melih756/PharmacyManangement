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
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
            ShowMed();
            ShowBill();
            SLbl.Text = Login.User;
            GetCustomer();
            
        }
        SqlConnection con = new SqlConnection(@"Data Source=192.168.43.154;Initial Catalog=Eczane1;User ID=sa;Password=123456");
        private void GetCustomer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select CustNum from CustomerTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(Rdr);
            CustCbx.ValueMember = "CustNum";
            CustCbx.DataSource = dt;
            con.Close();
        }
        private void GetCustName()
        {
            con.Open();
            string Query = "Select * from CustomerTbl where CustNum= " + CustCbx.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustName.Text = dr["CustName"].ToString();

            }
            con.Close();
        }
        private void UpdateQty() {

                try
                {
                    int newQty = Stock - Convert.ToInt32(MedQty.Text);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedQty=@MQ where MedNum=@MKey", con);

                    cmd.Parameters.AddWithValue("@MQ", newQty);

                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("İlaç Güncellendi");
                    con.Close();
                    ShowMed();
                    //Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        

        private void InsertBill()
        {
            if (CustName.Text=="")
            {

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(SName,CustNum,CustName,BDate,BAmount)values(@SN,@CN,@CNA,@BD,@BA)", con);
                    cmd.Parameters.AddWithValue("@SN", SLbl.Text);
                    cmd.Parameters.AddWithValue("@CN", CustCbx.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CNA", CustName.Text);
                    cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@BA", GrdTotal);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fatura Kaydedildi");
                    con.Close();
                    ShowBill();

                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
           
        }
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

        private void ShowBill()                        
        {
            con.Open();
            string Query = "Select * from BillTbl where SName='"+SLbl.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        int GrdTotal = 0;
        int n = 0;
        int key = 0, Stock;
            private void MedSaveBtn_Click(object sender, EventArgs e) { 
        
            if (MedQty.Text == "" || Convert.ToInt32(MedQty.Text) > Stock)
            {
                MessageBox.Show("Adet Giriniz");
            }
            else
            {
                int total = Convert.ToInt32(MedQty.Text) * Convert.ToInt32(MedPrice.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BDTGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = MedName.Text;
                newRow.Cells[2].Value = MedQty.Text;
                newRow.Cells[3].Value = MedPrice.Text;
                newRow.Cells[4].Value = total;
                BDTGV.Rows.Add(newRow);
                GrdTotal = GrdTotal + total;
                Totallbl.Text = " Total " + GrdTotal;
                n++;
                UpdateQty();
            }
        }
        
        int medid, medprice, medQty, medTot;
        int pos = 60;
        string medName;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Üniversite Eczanesi Kocaeli", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID İLAÇ FİYAT MİKTAR TUTAR", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in BDTGV.Rows)
            {
                medid = Convert.ToInt32(row.Cells["Column1"].Value);
                medName = "" + row.Cells["column2"].Value;
                medprice = Convert.ToInt32(row.Cells["Column3"].Value);
                medQty = Convert.ToInt32(row.Cells["Column4"].Value);
                medTot = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + medid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + medName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + medprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + medQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + medTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Toplam Tutar:" + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("*******Üniversite Eczanesi*******", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            BDTGV.Rows.Clear();
            BDTGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }

        private void CustCbx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Selling_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();

            dash.ShowDialog();
            this.Hide();
        }

       

        private void label20_Click(object sender, EventArgs e)
        {
            
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

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

        private void TDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BDTGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label18_Click_1(object sender, EventArgs e)
        {
            Manufacturer man = new Manufacturer();

            man.ShowDialog();
            this.Hide();
        }

        private void label20_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            Sellers sel = new Sellers();
            sel.ShowDialog();
            this.Hide();
        }

        private void MedDTGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedName.Text = MedDTGV.SelectedRows[0].Cells[1].Value.ToString();
            //MedCbx.SelectedItem = MedDTGV.SelectedRows[0].Cells[2].Value.ToString();
            Stock = Convert.ToInt32(MedDTGV.SelectedRows[0].Cells[3].Value.ToString());
            MedPrice.Text = MedDTGV.SelectedRows[0].Cells[4].Value.ToString();
            //MedManCbx.SelectedValue = MedDTGV.SelectedRows[0].Cells[5].Value.ToString();
            //MedManName.Text = MedDTGV.SelectedRows[0].Cells[6].Value.ToString();
            if (MedName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MedDTGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
