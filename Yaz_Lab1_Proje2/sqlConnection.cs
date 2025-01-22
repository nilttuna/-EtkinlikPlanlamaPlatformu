using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Yaz_Lab1_Proje2
{
    public class sqlConnection
    {
       public SqlConnection baglanti()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-9UGD5QJ\\SQLEXPRESS;Initial Catalog=Akilli_Etkinlik_Planlama_Platformu;Integrated Security=True");
            connection.Open();
            return connection;
        }
    }
}