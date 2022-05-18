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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-2G0B3AJ4;Initial Catalog=Eczane1;User ID=sa;Password=123456");
        public static string User;
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UName.Text==""||ParolaTb.Text=="")
            {
                MessageBox.Show("Hem Kullanıcı Adını Hem de Şifreyi Girin");
            }
            else
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from SellerTbl where SName='"+UName.Text+"' and SPass='"+ParolaTb.Text+"'",con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString()=="1")
                {
                    User = UName.Text;
                    Selling Obj = new Selling();
                    Obj.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Yanlış Kullanıcı Adı veya Parola");
                }
                con.Close();
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
