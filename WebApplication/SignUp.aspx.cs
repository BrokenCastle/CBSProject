using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

namespace WebApplication
{
    public partial class SignUp : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButonSaveClick(object sender, EventArgs e)
        {
            // LisId ve ListMail adlı liste oluşturuyoruz...
            List<string> ListId = new List<string>();
            List<string> ListMail = new List<string>();

            // Veritabanından Kullanıcı tablosundaki Id sütundaki verileri alıp, ListId listesine atıyoruz...
            NpgsqlConnection connect = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT \"Id\" FROM \"Kullanici\" ", connect);
            NpgsqlDataReader dataRead = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataRead);
            foreach (DataRow dataRow in dataTable.Rows)
                ListId.Add(string.Join(";", dataRow.ItemArray.Select(item => item.ToString())));

            // Listenin son elemanındaki sayıyı 1 arttırıp yeni kullanıcının Id si olacak...
            int id = Convert.ToInt32(ListId.Last());
            id++;

            // Textboxtaki verileri değişkenlere tanımlıyoruz...
            string name = txtname.Text;
            string surname = txtsurname.Text;
            string pasword = txtpasword.Text;
            string mail = txtmail.Text;

            // Üyelerin Id değeri 2 dir...
            int member = 2;


            // Textboxların boş olup olmadığını sorguluyoruz...
            if (string.IsNullOrEmpty(name))
            {
                LabelMessage.Text = "Lütfen isim giriniz";
            }
            else
            {
                if (string.IsNullOrEmpty(surname))
                {
                    LabelMessage.Text = "Lütfen soyadı giriniz";
                }
                else
                {
                    if (string.IsNullOrEmpty(pasword))
                    {
                        LabelMessage.Text = "Lütfen şifre giriniz";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(mail))
                        {
                            LabelMessage.Text = "Lütfen bir e-posta giriniz";
                        }
                        else
                        {
                            // Veritabanından Kullanıcı tablosundaki Mail sütundaki verileri alıp, ListMail listesine atıyoruz...
                            NpgsqlCommand command2 = new NpgsqlCommand("SELECT \"Mail\" FROM \"Kullanici\" ", connect);
                            NpgsqlDataReader dataReadMail = command2.ExecuteReader();
                            var dataTable2 = new DataTable();
                            dataTable2.Load(dataReadMail);
                            foreach (DataRow dataRow in dataTable2.Rows)
                                ListMail.Add(string.Join(";", dataRow.ItemArray.Select(item => item.ToString())));

                            // mail_counter adlı bir sayaç tanımlıyoruz...
                            int mail_counter = 0;

                            // Girilen mailin veritabanında olup olmadığını sorguluyoruz...
                            for (int i = 0; i < ListMail.Count; i++)
                            {
                                if (mail == ListMail[i])
                                {
                                    LabelMessage.Text = mail + " zaten kayıtlı. Lütfen başka bir e-posta adresi kullan";
                                }
                                else
                                {
                                    mail_counter++;
                                }
                            }

                            // Sayaç değeri listedeki eleman sayısına eşitse veritabanına kaydediyoruz...
                            if (mail_counter == ListMail.Count)
                            {
                                NpgsqlCommand save = new NpgsqlCommand("INSERT INTO \"Kullanici\" (\"Id\", \"Adi\", \"Soyadi\", \"Sifre\", \"Mail\", \"Tipi\") VALUES ('" + id + "','" + name + "','" + surname + "','" + pasword + "','" + mail + "','" + member + "'); ", connect);
                                NpgsqlDataReader saved = save.ExecuteReader();
                                connect.Close();
                                connect.Dispose();
                            }
                        }
                    }
                }
            }
        }
    }
}