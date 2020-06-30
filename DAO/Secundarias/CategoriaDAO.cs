using EletroStar.Models.Secundarias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Secundarias
{
    public class CategoriaDAO: PadraoDAO<CategoriaViewModel>
    {
        protected override SqlParameter[] CriaParametros(CategoriaViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("descricao", model.descricao);
           
            return parametros;
        }

        protected override CategoriaViewModel MontaModel(DataRow registro)
        {
            CategoriaViewModel categoria = new CategoriaViewModel();
            categoria.id = Convert.ToInt32(registro["id"]);
            categoria.descricao = registro["descricao"].ToString();
            

            return categoria;
        }

        protected override void SetTabela()
        {
            Tabela = "Categoria";
        }
    }
}
