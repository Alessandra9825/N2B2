using EletroStar.Models.Relacionamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Relacionamento
{
    public class Carrinho_ProjetoDAO: PadraoDAO<Carrinho_ProjetoViewModel>
    {
        protected override SqlParameter[] CriaParametros(Carrinho_ProjetoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("id_Carrinho", model.id_Carrinho);
            parametros[2] = new SqlParameter("id_Projeto", model.id_Projeto);
            parametros[3] = new SqlParameter("quantidade", model.quantidade);

            return parametros;
        }

        protected override Carrinho_ProjetoViewModel MontaModel(DataRow registro)
        {
            Carrinho_ProjetoViewModel carrinho_Produto = new Carrinho_ProjetoViewModel();
            carrinho_Produto.id = Convert.ToInt32(registro["id"]);
            carrinho_Produto.id_Carrinho = Convert.ToInt32(registro["id_Carrinho"]);
            carrinho_Produto.id_Projeto = Convert.ToInt32(registro["id_Projeto"]);
            carrinho_Produto.quantidade = Convert.ToInt32(registro["quantidade"]);

            return carrinho_Produto;
        }

        protected override void SetTabela()
        {
            Tabela = "Carrinho_Projeto";
        }

        public Carrinho_ProjetoViewModel Consulta(int id_Cliente, int id_Projeto)
        {
            var p = new SqlParameter[]
             {
                new SqlParameter("@id_Carrinho", id_Cliente),
                new SqlParameter("@id_Projeto", id_Projeto)
             };

            var tabela = HelperDAO.ExecutaProcSelect("spConsulta_Carrinho_Projeto", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
    }
}
