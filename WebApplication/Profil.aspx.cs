using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace WebApplication
{
    public partial class Profil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sayfa Yenilenmediyse...
            if (!IsPostBack)
            {
                // Session ile Giriş yapan kullanıcının Id sini UserId sine aktarıyoruz...
                string UserId = Session["kul_id"].ToString();

                NpgsqlConnection connect = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                connect.Open();

                NpgsqlCommand command = new NpgsqlCommand("SELECT \"Adi\", \"Soyadi\", \"Sifre\", \"Mail\" FROM \"Kullanici\" WHERE \"Id\"='" + UserId + "'", connect);
                NpgsqlDataReader dr = command.ExecuteReader();

                // Giriş yapan kullanıcının veritabanındaki Adı, Soyadı, Mail bilgilerini textboxlara atıyoruz...
                dr.Read();
                txtName.Text = dr["Adi"].ToString();
                txtSurname.Text = dr["Soyadi"].ToString();
                txtMail.Text = dr["Mail"].ToString();
                connect.Close();
                connect.Dispose();
            }
            // Page.IsPostBack ---> Sayfa Yenilendiyse
        }


        protected void ButonSaveClick(object sender, EventArgs e)
        {
            string userId = Session["kul_id"].ToString();

            NpgsqlConnection connect = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            connect.Open();

            NpgsqlCommand command = new NpgsqlCommand("SELECT \"Adi\", \"Soyadi\", \"Sifre\", \"Mail\" FROM \"Kullanici\" WHERE \"Id\"='" + userId + "'", connect);
            NpgsqlDataReader dr = command.ExecuteReader();
            // Giriş yapan kullanıcının veritabanındaki Adı, Soyadı, Şifre, Mail bilgilerini değişkenlere tanımlıyoruz...
            dr.Read();
            string name = dr["Adi"].ToString();
            string surname = dr["Soyadi"].ToString();
            string password = dr["Sifre"].ToString();
            string mail = dr["Mail"].ToString();
            connect.Close();

            // Eğer txtPassword ve txtPasswordNew textboxları boşsa...
            if (txtPassword.Text == String.Empty & txtPasswordNew.Text == String.Empty)
            {
                // Veritabanındaki Adı ve Textbox a yeni girdiği Adı birbirine eşit değilse...
                if (name != txtName.Text)
                {
                    //Yeni Adı veritabanına güncelle...
                    string newname = txtName.Text;
                    NpgsqlConnection connect3 = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                    connect3.Open();

                    NpgsqlCommand command3 = new NpgsqlCommand("UPDATE \"Kullanici\" SET \"Adi\" ='" + newname + "' WHERE \"Id\" ='" + userId + "'", connect3);
                    NpgsqlDataReader Dr = command3.ExecuteReader();

                    LabelMessage.Text = "Adınız güncellenmiştir";

                    connect3.Close();
                    connect3.Dispose();
                }

                // Veritabanındaki Soyadı ve Textbox a yeni girdiği Soyadı birbirine eşit değilse...
                if (surname != txtSurname.Text)
                {
                    //Yeni Soyadı veritabanına güncelle...
                    string newsurname = txtSurname.Text;
                    NpgsqlConnection connect4 = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                    connect4.Open();

                    NpgsqlCommand command4 = new NpgsqlCommand("UPDATE \"Kullanici\" SET \"Soyadi\" ='" + newsurname + "' WHERE \"Id\" ='" + userId + "'", connect4);
                    NpgsqlDataReader Dr = command4.ExecuteReader();

                    LabelMessage.Text = "Soyadınız güncellenmiştir";

                    connect4.Close();
                    connect4.Dispose();
                }

                // Veritabanındaki Mail ve Textbox a yeni girdiği Mail birbirine eşit değilse...
                if (mail != txtMail.Text)
                {
                    //Yeni Mail veritabanına güncelle...
                    string newmail = txtMail.Text;
                    NpgsqlConnection connect5 = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                    connect5.Open();

                    NpgsqlCommand command5 = new NpgsqlCommand("UPDATE \"Kullanici\" SET \"Mail\" ='" + newmail + "' WHERE \"Id\" ='" + userId + "'", connect5);
                    NpgsqlDataReader Dr = command5.ExecuteReader();

                    LabelMessage.Text = "Mailiniz güncellenmiştir";

                    connect5.Close();
                    connect5.Dispose();
                }

            }

            else
            {
                // Veritabanındaki Şifre ve Textbox a yeni girdiği Şifre yi aynı olup olmadığını kontrol et...
                if (password == txtPassword.Text)
                {
                    // Eğer txtPasswordNew textbox boş değilse...
                    if (txtPasswordNew.Text != String.Empty)
                    {
                        //Yeni Şifre veritabanına güncelle...
                        string newpasword = txtPasswordNew.Text;
                        NpgsqlConnection connect2 = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                        connect2.Open();

                        NpgsqlCommand command2 = new NpgsqlCommand("UPDATE \"Kullanici\" SET \"Sifre\" ='" + newpasword + "' WHERE \"Id\" ='" + userId + "'", connect2);
                        NpgsqlDataReader Dr = command2.ExecuteReader();

                        LabelMessage.Text = "Yeni şifreniz güncellenmiştir";

                        connect2.Close();
                        connect2.Dispose();
                    }
                    else
                    {
                        LabelMessage.Text = "Yeni şifre boş";
                    }
                }
                else
                {
                    LabelMessage.Text = "Eski şifrenizi yanlış girdiniz";
                }

            }
        }
    }
}