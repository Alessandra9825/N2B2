using EletroStar.Models.Principais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Principais
{
    public class FabricanteDAO : PadraoDAO<FabricanteViewModel>
    {
        protected override SqlParameter[] CriaParametros(FabricanteViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("nome", model.nome);            

            return parametros;
        }

        protected override FabricanteViewModel MontaModel(DataRow registro)
        {
            FabricanteViewModel fabricante = new FabricanteViewModel();
            fabricante.id = Convert.ToInt32(registro["id"]);
            fabricante.nome = registro["nome"].ToString();
            
            return fabricante;
        }

        protected override void SetTabela()
        {
            Tabela = "Fabricante";
        }
    }
}
