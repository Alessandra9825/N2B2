using EletroStar.Models.Secundarias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Secundarias
{
    public class UFDAO : PadraoDAO<UFViewModel>
    {
        protected override SqlParameter[] CriaParametros(UFViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("descricao", model.descricao);

            return parametros; 
        }

        protected override UFViewModel MontaModel(DataRow registro)
        {
            UFViewModel uf = new UFViewModel();
            uf.id = Convert.ToInt32(registro["id"]);
            uf.descricao = registro["descricao"].ToString();


            return uf;
        }

        protected override void SetTabela()
        {
            Tabela = "UF";
        }
    }
}
