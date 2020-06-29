using EletroStar.Models.Relacionamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Relacionamento
{
    public class Produto_Projeto : PadraoDAO<Produto_ProjetoViewModel>
    {
        protected override SqlParameter[] CriaParametros(Produto_ProjetoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("id_Produto", model.id_Produto);
            parametros[2] = new SqlParameter("id_Projeto", model.id_Projeto);

            return parametros;
        }

        protected override Produto_ProjetoViewModel MontaModel(DataRow registro)
        {
            Produto_ProjetoViewModel produto_Projeto = new Produto_ProjetoViewModel();
            produto_Projeto.id = Convert.ToInt32(registro["id"]);
            produto_Projeto.id_Produto = Convert.ToInt32(registro["id_Produto"]);
            produto_Projeto.id_Projeto = Convert.ToInt32(registro["id_Projeto"]);


            return produto_Projeto;
        }

        protected override void SetTabela()
        {
            Tabela = "Produto_Projeto";
        }
    }
}
