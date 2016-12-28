using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//FARUK ALBAYRAK B131210012 SAMET TOPAKKAYA B131210066
namespace IlacDestekSistemi
{
    public partial class Form1 : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=IlacDestekDB;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }
        public string verigetir(int ID,string istenen)
        {
            string getirilen = "";
            SqlCommand cmd = new SqlCommand("Select * from Ilac Where ID='" + ID + "'", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                getirilen = Convert.ToString(dr[istenen]);
            }
            dr.Close();
            return getirilen;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int[] IDler = new int[6];
            string[] isimler = new string[6];
            string[] etkenler = new string[6];
            int i = 0;
            if (cmbIlac.SelectedIndex == -1 && cmbHastalik.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Alanları Boş Bırakmayınız!");
            }
            else
            {
                textBox3.Text = "";
                textBox2.Text = "";
                textBox1.Text = "";
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("Select * from IlacEtkilesimleri Where Ilac1='" + (cmbIlac.SelectedIndex + 1) + "'", baglanti);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    IDler[i] = Convert.ToInt32(dr["Ilac2"]);
                    i++;
                }
                dr.Close();
                for (i = 0; i < IDler.Length; i++)
                {
                    isimler[i] = verigetir(IDler[i],"Ad");
                    etkenler[i] = verigetir(IDler[i], "EtkenMadde");
                }
                i = 0;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text += "İlacın Adı: " + isimler[i] + "\t Etken Maddesi: "+etkenler[i] +"\t";
                    i++;
                    textBox1.Text += "Etki Durumu: " + dr["Durum"] + Environment.NewLine;
                    textBox1.Text += dr["Aciklama"] + Environment.NewLine + Environment.NewLine;
                }
                dr.Close();
                SqlCommand cmd2 = new SqlCommand("Select * from Ilac Where ID='" + (cmbIlac.SelectedIndex + 1) + "'", baglanti);
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    textBox2.Text += dr["Ozellik"] + Environment.NewLine;
                    textBox3.Text += "Etken Maddesi:" + dr["EtkenMadde"] + Environment.NewLine;
                    textBox3.Text += "İnhibitör Türü:" + dr["InhibitorTuru"] + Environment.NewLine;
                    textBox3.Text += "Talimat:" + dr["Kullanim"] + Environment.NewLine;
                }
                dr.Close();
                baglanti.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            cmbIlac.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from Ilac where ID<13", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlac.Items.Add(dr["Ad"]);
            }
            baglanti.Close();
            cmbIlac.SelectedIndex = 0;
        }
    }
}
