using EletroStar.Models.Secundarias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Secundarias
{
    public class PagamentoDAO : PadraoDAO<PagamentoViewModel>
    {
        protected override SqlParameter[] CriaParametros(PagamentoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("descricao", model.descricao);

            return parametros;
        }

        protected override PagamentoViewModel MontaModel(DataRow registro)
        {
            PagamentoViewModel pagamento = new PagamentoViewModel();
            pagamento.id = Convert.ToInt32(registro["id"]);
            pagamento.descricao = registro["descricao"].ToString();


            return pagamento;
        }

        protected override void SetTabela()
        {
            Tabela = "Pagamento";
        }
    }
}
