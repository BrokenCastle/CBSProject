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
    public partial class AdminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserId = Session["kul_id"].ToString();

            // Veritabanına bağlanıp, tabloda yer alan kısımlara gerekli olan bilgileri yolluyoruz...
            NpgsqlConnection connect = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT \"Kullanici\".\"Id\", \"Kullanici\".\"Adi\", \"Kullanici\".\"Soyadi\", \"Tipi\".\"Name\"  FROM \"Kullanici\" LEFT OUTER JOIN \"Tipi\" ON \"Kullanici\".\"Tipi\" = \"Tipi\".\"Id\" ORDER BY \"Id\" ", connect);
            GridView1.DataSource = command.ExecuteReader();
            GridView1.DataBind();
            connect.Close();
            connect.Dispose();
        }

        protected void MyButtonClick(object sender, EventArgs e)
        {
            // Basılan butonunun satırını alıyoruz...
            Button buton = (Button)sender;
            GridViewRow gridviewrow = (GridViewRow)buton.NamingContainer;

            // Satır index ini alıyoruz...
            int rowindex = gridviewrow.RowIndex;
            rowindex++;

            NpgsqlConnection connect = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT \"Tipi\" FROM \"Kullanici\" WHERE \"Id\"='"+ rowindex +"'", connect);
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();
            string name = dr["Tipi"].ToString();

            // Admin ise üye yap...
            if (name == "1") 
            {
                int member = 2;
                NpgsqlConnection connect2 = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                connect2.Open();
                NpgsqlCommand command2 = new NpgsqlCommand("UPDATE \"Kullanici\" SET \"Tipi\"='" + member + "' WHERE \"Id\"='" + rowindex + "'", connect2);
                NpgsqlDataReader dr2 = command2.ExecuteReader();
                connect2.Close();
            }
            // Üye ise Admin yap...
            else
            {
                int member = 1;
                NpgsqlConnection connect3 = new NpgsqlConnection("Database=KKS;Server=localhost;Port=5432;User Id=postgres;Password=74108520963");
                connect3.Open();
                NpgsqlCommand command3 = new NpgsqlCommand("UPDATE \"Kullanici\" SET \"Tipi\"='" + member + "' WHERE \"Id\"='" + rowindex + "'", connect3);
                NpgsqlDataReader dr3 = command3.ExecuteReader();
                connect3.Close();
            }
            connect.Close();
            Response.Redirect("AdminPanel.aspx");
        }
    }
}