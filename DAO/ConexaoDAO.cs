using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO
{
    public class ConexaoDAO
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=SQL5034.site4now.net;Initial Catalog=DB_A63DE8_lucasrusso;User Id=DB_A63DE8_lucasrusso_admin;Password=fzsgzd02;";
            SqlConnection connection = new SqlConnection(strCon);
            connection.Open();
            return connection;
        }
    }
}
