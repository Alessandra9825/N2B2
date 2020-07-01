using EletroStar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Principais
{
    public class ProdutoDAO : PadraoDAO<ProdutoViewModel>
    {
        protected override SqlParameter[] CriaParametros(ProdutoViewModel model)
        {
            object imgByte = model.ImagemEmByte;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("nome", model.nome);
            parametros[2] = new SqlParameter("descricao", model.descricao);
            parametros[3] = new SqlParameter("valor", model.valor);
            parametros[4] = new SqlParameter("id_Fabricante", model.id_Fabricante);
            parametros[5] = new SqlParameter("id_Categoria", model.id_Categoria);
            parametros[6] = new SqlParameter("imagem", imgByte);

            return parametros;
        }

        protected override ProdutoViewModel MontaModel(DataRow registro)
        {
            ProdutoViewModel produto = new ProdutoViewModel();
            produto.id = Convert.ToInt32(registro["id"]);
            produto.nome = registro["nome"].ToString();
            produto.descricao = registro["descricao"].ToString();
            produto.valor = Convert.ToDouble(registro["valor"]);
            produto.id_Fabricante = Convert.ToInt32(registro["id_Fabricante"]);
            produto.id_Categoria = Convert.ToInt32(registro["id_Categoria"]);

            if (registro["imagem"] != DBNull.Value)
                produto.ImagemEmByte = registro["imagem"] as byte[];



            return produto; 
        }

        protected override void SetTabela()
        {
            Tabela = "Produto";
        }

         public List<ProdutoViewModel> ListagemComFiltro(string nome)
         {
            var tabela = HelperDAO.ExecutaFuncSelect("dbo.func_consultarProduto", nome);
            var lista = new List<ProdutoViewModel>();

            if (tabela.Rows.Count == 0)
                return lista;
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
         }
        public List<ProdutoViewModel> ListagemComFiltroCategoria(int categoria)
        {

            var tabela = HelperDAO.ExecutaFuncSelect("dbo.func_consultarProdutoC", categoria.ToString());

            var lista = new List<ProdutoViewModel>();

            if (tabela.Rows.Count == 0)
                return lista;
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }
    }
}
