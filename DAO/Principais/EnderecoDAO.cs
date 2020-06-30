using EletroStar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO
{
    public class EnderecoDAO : PadraoDAO<EnderecoViewModel>
    {
        protected override SqlParameter[] CriaParametros(EnderecoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[9];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("id_Cliente", model.id_Cliente);
            parametros[2] = new SqlParameter("rua", model.rua);
            parametros[3] = new SqlParameter("numero", model.numero);
            parametros[4] = new SqlParameter("complemento", model.complemento == null ? "" : model.complemento);
            parametros[5] = new SqlParameter("bairro", model.bairro);
            parametros[6] = new SqlParameter("cidade", model.cidade);
            parametros[7] = new SqlParameter("id_UF", model.id_UF);
            parametros[8] = new SqlParameter("cep", model.cep);            

            return parametros;
        }

        protected override EnderecoViewModel MontaModel(DataRow registro)
        {
            EnderecoViewModel endereco = new EnderecoViewModel();
            endereco.id = Convert.ToInt32(registro["id"]);
            endereco.id_Cliente = Convert.ToInt32(registro["id_Cliente"]);
            endereco.rua = registro["rua"].ToString();
            endereco.numero = Convert.ToInt32(registro["numero"]);
            endereco.complemento = registro["complemento"].ToString();
            endereco.bairro = registro["bairro"].ToString();
            endereco.cidade = registro["cidade"].ToString();
            endereco.id_UF = Convert.ToInt32(registro["id_UF"]);
            endereco.cep = registro["cep"].ToString();                       

            return endereco;
        }

        protected override void SetTabela()
        {
            Tabela = "Endereco";
        }

        public EnderecoViewModel Consulta_IdCliente(int idCliente)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("@id_Cliente", idCliente)
            };

            var tabela = HelperDAO.ExecutaProcSelect("spEndereco_IdCliente", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
    }
}
