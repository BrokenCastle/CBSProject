using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Npgsql;
using System.Data;

namespace WebApplication
{
    public partial class Default : System.Web.UI.Page
    {
        // Yeni listeler oluşturuyoruz...
        List<string> strList = new List<string>();
        List<int> strList2 = new List<int>();
        int kayit_no;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Çıkış yaptıktan sonra Id sini sildiğimiz için tekrardan giriş yapması için Login sayfasına yönlendir...
            if (Session["kul_id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            string kullanici = Session["kullanici_adi"].ToString();
            string userId = Session["kul_id"].ToString();

            // userId null veya boşsa Login.aspx'e yönlendir...
            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                labelmesaj.Text = "Hoşgeldiniz Sayın " + kullanici.ToString();
            }






            using (NpgsqlConnection verial = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963"))
            {
                // Veritabanından Konum tablosundaki Id sütundaki verileri alıp, strList listesine atıyoruz...
                verial.Open();
                NpgsqlCommand liste = new NpgsqlCommand("SELECT \"No\" FROM \"Konum\" WHERE \"Id\"='" + userId + "'", verial);
                NpgsqlDataReader listelemek = liste.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(listelemek);
                foreach (DataRow dataRow in dataTable.Rows)
                    strList.Add(string.Join(";", dataRow.ItemArray.Select(item => item.ToString())));
            }





            // Veritabanından Konum Adı, X ve Y Kordinatlarını alıyoruz...
            NpgsqlConnection al = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            al.Open();
            NpgsqlCommand almak = new NpgsqlCommand("SELECT \"Konum Adi\", \"X Kordinati\", \"Y Kordinati\" FROM \"Konum\" WHERE \"Id\"='" + userId + "'", al);
            // Gridview1 de listeliyoruz...
            GridView1.DataSource = almak.ExecuteReader();
            GridView1.DataBind();
            al.Close();
            al.Dispose();
        }


        protected void btnEkle_Click(object sender, EventArgs e)
        {
            // Eğer txtGizlemeId boşsa yeni kordinat ekle...
            if (string.IsNullOrWhiteSpace(txtGizlemeId.Text))
            {
                // Textboxta buluanan verileri değişkenlere tanımlıyoruz...
                string ad = txtKonumadi.Text;
                string xkor = txtKordinatx.Text;
                string ykor = txtKordinaty.Text;
                string kuId = Session["kul_id"].ToString();

                // Eğer strList eleman sayısı 0 ise yeni kayıtı 1 den başlat...
                if (strList.Count == 0)
                {
                    kayit_no = 1;
                }
                // Değilse son elemanı alıp diğer elemanlarla kıyaslayıp büyük elemanı bulup, 1 arttır...
                // Bunun nedeni öncelerinde veriler güncellendiyse veya yeni kayıt yapıldıysa kayıt no larının yer değişmesindendir...
                else
                {
                    kayit_no = Convert.ToInt32(strList.Last());
                    int max = 0;
                    for (int i = 0; i < strList.Count; i++)
                        if (max < Convert.ToInt32(strList[i]))
                        {
                            max = Convert.ToInt32(strList[i]);
                            kayit_no = max + 1;
                        }
                }
                NpgsqlConnection ekle = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                ekle.Open();
                NpgsqlCommand vericek = new NpgsqlCommand("INSERT INTO \"Konum\" (\"Konum Adi\", \"X Kordinati\", \"Y Kordinati\", \"Id\", \"No\") VALUES ('" + ad + "', '" + xkor + "', '" + ykor + "', '" + kuId + "', '" + kayit_no + "'); ", ekle);
                NpgsqlDataReader oku = vericek.ExecuteReader();
                ekle.Close();
                ekle.Dispose();
            }

            // Değilse güncelleme yap...
            else
            {
                // Textboxta buluanan verileri değişkenlere tanımlıyoruz... 
                string ad2 = txtKonumadi.Text;
                string xkor2 = txtKordinatx.Text;
                string ykor2 = txtKordinaty.Text;
                kayit_no = Convert.ToInt32(txtGizlemeId.Text);
                NpgsqlConnection güncel = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                güncel.Open();
                NpgsqlCommand güncelle = new NpgsqlCommand("UPDATE \"Konum\" SET \"Konum Adi\"='" + ad2 + "', \"X Kordinati\"='" + xkor2 + "', \"Y Kordinati\"='" + ykor2 + "' WHERE  \"No\"='" + kayit_no + "'", güncel);
                NpgsqlDataReader oku2 = güncelle.ExecuteReader();
                güncel.Close();
                güncel.Dispose();
            }

            Response.Redirect("Default.aspx");
        }


        protected void btnSil_Click(object sender, EventArgs e)
        {
            // Textbox ları temizle...
            txtKonumadi.Text = String.Empty;
            txtKordinatx.Text = String.Empty;
            txtKordinaty.Text = String.Empty;
            Response.Redirect("Default.aspx");
        }


        protected void DeleteRowButton_Click(Object sender, GridViewDeleteEventArgs e)
        {

            int rowIndex = Convert.ToInt32(e.RowIndex);
            string kuId = Session["kul_id"].ToString();
            string number = strList[rowIndex];
            NpgsqlConnection sil = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            NpgsqlCommand silmek = new NpgsqlCommand("DELETE FROM \"Konum\" WHERE \"No\"='" + number + "' AND \"Id\"='" + kuId + "'", sil);
            sil.Open();
            silmek.ExecuteNonQuery();
            sil.Close();
            sil.Dispose();
            Response.Redirect("Default.aspx");
        }


        protected void EditRowButton_Click(object sender, GridViewEditEventArgs e)
        {
            int rowIndex2 = e.NewEditIndex;
            string number2 = strList[rowIndex2];
            NpgsqlConnection düzenle = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            NpgsqlCommand düzenlemek = new NpgsqlCommand("SELECT \"Konum Adi\", \"X Kordinati\", \"Y Kordinati\" FROM \"Konum\" WHERE \"No\"='" + number2 + "'", düzenle);
            düzenle.Open();
            NpgsqlDataReader okundu = düzenlemek.ExecuteReader();
            okundu.Read();
            // Verileri textboxlara yolluyoruz...
            txtKonumadi.Text = okundu["Konum Adi"].ToString();
            txtKordinatx.Text = okundu["X Kordinati"].ToString();
            txtKordinaty.Text = okundu["Y Kordinati"].ToString();
            txtGizlemeId.Text = strList[rowIndex2];
            düzenle.Close();
            düzenle.Dispose();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            // Default.aspx sayfadan çıkış yap ve Session sil...
            Session["kul_id"] = null;
            Response.Redirect("Login.aspx");
        }


    }
}
