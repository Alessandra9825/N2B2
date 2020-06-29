using EletroStar.Models.Secundarias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Secundarias
{
    public class StatusDAO : PadraoDAO<StatusViewModel>
    {
        protected override SqlParameter[] CriaParametros(StatusViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("descricao", model.descricao);

            return parametros;
        }

        protected override StatusViewModel MontaModel(DataRow registro)
        {
            StatusViewModel status = new StatusViewModel();
            status.id = Convert.ToInt32(registro["id"]);
            status.descricao = registro["descricao"].ToString();


            return status;
        }

        protected override void SetTabela()
        {
            Tabela = "Status";
        }
    }
}
