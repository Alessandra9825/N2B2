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
            string strCon = "Data Source=localhost\\SQL2014;Initial Catalog=Portal;integrated security=true";
            SqlConnection connection = new SqlConnection(strCon);
            connection.Open();
            return connection;
        }
    }
}
