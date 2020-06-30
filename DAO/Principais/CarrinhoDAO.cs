using EletroStar.Models.Principais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Principais
{
    public class CarrinhoDAO : PadraoDAO<CarrinhoViewModel>
    {
        protected override SqlParameter[] CriaParametros(CarrinhoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("id_Cliente", model.id_Cliente);

            return parametros;
        }

        protected override CarrinhoViewModel MontaModel(DataRow registro)
        {
            CarrinhoViewModel carrinho = new CarrinhoViewModel();
            carrinho.id = Convert.ToInt32(registro["id"]);
            carrinho.id_Cliente = Convert.ToInt32(registro["id_Cliente"]);

            return carrinho;
        }

        public CarrinhoProdutoViewModel MontaModelProduto(DataRow registro)
        {
            CarrinhoProdutoViewModel carrinhoProduto = new CarrinhoProdutoViewModel();
            carrinhoProduto.id = Convert.ToInt32(registro["id"]);
            carrinhoProduto.id_Produto = Convert.ToInt32(registro["id_Produto"]);
            carrinhoProduto.nome = registro["nome"].ToString();
            carrinhoProduto.descricao = registro["descricao"].ToString();
            carrinhoProduto.quantidade = Convert.ToInt32(registro["quantidade"]);
            carrinhoProduto.valor = Convert.ToDouble(registro["valor"]);
            if (registro["imagem"] != DBNull.Value)
                carrinhoProduto.ImagemEmByte = registro["imagem"] as byte[];


            return carrinhoProduto;
        }

        protected CarrinhoProjetoViewModel MontaModelProjeto(DataRow registro)
        {
            CarrinhoProjetoViewModel carrinhoProjeto = new CarrinhoProjetoViewModel();
            carrinhoProjeto.id = Convert.ToInt32(registro["id"]);
            carrinhoProjeto.id_Projeto = Convert.ToInt32(registro["id_Projeto"]);
            carrinhoProjeto.nome = registro["nome"].ToString();
            carrinhoProjeto.quantidade = Convert.ToInt32(registro["quantidade"]);
            carrinhoProjeto.valor = Convert.ToDouble(registro["valor"]);

            return carrinhoProjeto;
        }

        protected override void SetTabela()
        {
            Tabela = "Carrinho";
        }

        public CarrinhoViewModel Consulta_IdCliente(int id_Cliente)
        {
            var p = new SqlParameter[]
            {
               new SqlParameter("@id_Cliente", id_Cliente)
            };

            var tabela = HelperDAO.ExecutaProcSelect("spConsulta_Carrinho_IdCliente", p);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
        

        public List<CarrinhoProdutoViewModel> Listagem(int idCliente)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("@id_Cliente", idCliente)
            };

            var tabela = HelperDAO.ExecutaProcSelect("spCarrinho_Produto", p);
            List<CarrinhoProdutoViewModel> carrinho = new List<CarrinhoProdutoViewModel>();

            foreach (DataRow row in tabela.Rows)
                carrinho.Add(MontaModelProduto(row));

            return carrinho;
        }

        public List<CarrinhoProjetoViewModel> ListagemProjetos(int idCliente)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("@id_Cliente", idCliente)
            };

            var tabela = HelperDAO.ExecutaProcSelect("spCarrinho_Projeto", p);
            List<CarrinhoProjetoViewModel> carrinho = new List<CarrinhoProjetoViewModel>();

            foreach (DataRow row in tabela.Rows)
                carrinho.Add(MontaModelProjeto(row));

            return carrinho;
        }        
    }
}
