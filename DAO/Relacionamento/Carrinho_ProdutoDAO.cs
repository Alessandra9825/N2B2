using EletroStar.Models.Relacionamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Relacionamento
{
    public class Carrinho_ProdutoDAO : PadraoDAO<Carrinho_ProdutoViewModel>
    {
        protected override SqlParameter[] CriaParametros(Carrinho_ProdutoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("id_Carrinho", model.id_Carrinho);
            parametros[2] = new SqlParameter("id_Produto", model.id_Produto);
            parametros[3] = new SqlParameter("quantidade", model.quantidade);

            return parametros;
        }

        protected override Carrinho_ProdutoViewModel MontaModel(DataRow registro)
        {
            Carrinho_ProdutoViewModel carrinho_Produto = new Carrinho_ProdutoViewModel();
            carrinho_Produto.id = Convert.ToInt32(registro["id"]);
            carrinho_Produto.id_Carrinho = Convert.ToInt32(registro["id_Carrinho"]);
            carrinho_Produto.id_Produto = Convert.ToInt32(registro["id_Produto"]);
            carrinho_Produto.quantidade = Convert.ToInt32(registro["quantidade"]);

            return carrinho_Produto;
        }

        protected override void SetTabela()
        {
            Tabela = "Carrinho_Produto";
        }

        public Carrinho_ProdutoViewModel Consulta(int id_Carrinho, int id_Produto)
        {
            var p = new SqlParameter[]
             {
                new SqlParameter("@id_Carrinho", id_Carrinho),
                new SqlParameter("@id_Produto", id_Produto)
             };

            var tabela = HelperDAO.ExecutaProcSelect("spConsulta_Carrinho_Produto", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }        
    }
}
