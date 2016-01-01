using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Npgsql;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using WebApplication.Classes;

namespace WebApplication
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<MyLocation> GetUserLocation(string userId)
        {
            // String değişkenlerimi boş tanımlıyoruz...
            string konumAdi = String.Empty;
            string xkoordinate = String.Empty;
            string ykoordinate = String.Empty;

            // Liste oluşturuyoruz...
            List<MyLocation> liste = new List<MyLocation>();

            NpgsqlConnection conn = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            conn.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT \"Konum Adi\", \"X Kordinati\", \"Y Kordinati\" FROM \"Konum\" WHERE \"Id\"='" + userId + "'", conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                // Veritabanından alınan verileri değişkenlere tanımlıyoruz...
                konumAdi = dr["Konum Adi"].ToString();
                xkoordinate = dr["X Kordinati"].ToString();
                ykoordinate = dr["Y Kordinati"].ToString();

                // MyLocation sınıfına yolluyoruz ve listeye ekliyoruz...
                liste.Add(new MyLocation(konumAdi, xkoordinate, ykoordinate));
            }

            conn.Close();
            conn.Dispose();
            return liste;
        }
    }
}