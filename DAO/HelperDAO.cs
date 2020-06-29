using EletroStar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace EletroStar.DAO
{
    public class HelperDAO
    {
        //Conexoes realizadas com PROCEDURES
        public static DataTable ExecutaProcSelect(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection connection = ConexaoDAO.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nomeProc, connection))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    connection.Close();
                    return table;
                }
            }
        }

        public static void ExecutaProc(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection connection = ConexaoDAO.GetConexao())
            {
                using (SqlCommand command = new SqlCommand(nomeProc, connection))
                {
                    if (parametros != null)
                        command.Parameters.AddRange(parametros);

                    command.CommandType = CommandType.StoredProcedure;

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        public static DataTable ExecutaFuncSelect(string funcao, string cpf)
        {
            using (SqlConnection conexao = ConexaoDAO.GetConexao())
            {
                string sql = $"select * from {funcao}('{cpf.Trim()}')";

                using (SqlDataAdapter adapter = new SqlDataAdapter (sql,conexao))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    conexao.Close();
                    return tabela;
                }
            }
        }

    }

}
