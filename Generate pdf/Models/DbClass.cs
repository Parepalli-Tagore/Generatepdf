using Elfie.Serialization;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
namespace Generate_pdf.Models
{
    public class DbClass
    {
        SqlConnection con = new SqlConnection("Data Source = TPAREPAL-L-5467\\SQLEXPRESS; Initial Catalog = HospitalDB; User ID = sa; Password=Welcome2evoke@1234;Trust Server Certificate=true");
        
        public DataTable GetRecord()
        {
            //con.Open()
            SqlCommand com = new SqlCommand("Select * from Doctors", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

            

        }
    }
}
