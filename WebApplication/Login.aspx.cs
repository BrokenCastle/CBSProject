using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace WebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void buton_Click(object sender, EventArgs e)
        {
            // PostgreSQL veritabanına bağlanıyoruz...
            NpgsqlConnection baglan = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            baglan.Open();

            // Girilen verileri değişkenlere tanımlıyoruz...
            string yAd = txtisim.Text;
            string yParola = txtsifre.Text;

            // Bir sorgu tanımlayıyoruz...
            NpgsqlCommand vericek = new NpgsqlCommand("SELECT * FROM \"Kullanici\" WHERE \"Adi\"='" + yAd + "' and \"Sifre\"='" + yParola + "'", baglan);



            // Sorguyu yürütüyoruz...
            NpgsqlDataReader oku = vericek.ExecuteReader();
            if (oku.Read())
            {
                // Veritabanındaki Id değerini artık her sayfada kul_id olarak kullabileceğiz...
                Session["kul_id"] = oku["Id"].ToString();
                // Üye girişi yapan kullanıcının adının tarayıcıda görünmesi için...
                Session.Add("kullanici_adi", yAd);


                // UseIDCookie isimli nesnemizi oluşturduk...
                HttpCookie UseIDCookie = new HttpCookie("UseID");
                // UseIDCookie nesnemizi tanımladık...
                UseIDCookie.Value = oku["Id"].ToString();
                // Cookie mizin geçerlilik süresini 1 saat olarak ayarladık...
                UseIDCookie.Expires = DateTime.Now.AddHours(1);
                // Coookie mizi yazdırdık...
                Response.Cookies.Add(UseIDCookie);


                // Sayfayı yönlendiriyoruz...
                Response.Redirect("Default.aspx");
            }

            else
            {
                //  Uyarı verdiriyoruz...
                labelmesaj.Text = "Giriş Başarısız";
            }
            baglan.Close();
        }
    }
}