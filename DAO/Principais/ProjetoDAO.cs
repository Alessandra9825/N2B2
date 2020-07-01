using EletroStar.Models;
using EletroStar.Models.Principais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Principais
{
    public class ProjetoDAO : PadraoDAO<ProjetoViewModel>
    {
        protected override SqlParameter[] CriaParametros(ProjetoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("nome", model.nome);
            parametros[2] = new SqlParameter("descricao", model.descricao);
            parametros[3] = new SqlParameter("link", model.link);            

            return parametros;
        }

        protected override ProjetoViewModel MontaModel(DataRow registro)
        {
            ProjetoViewModel fabricante = new ProjetoViewModel();
            fabricante.id = Convert.ToInt32(registro["id"]);
            fabricante.nome = registro["nome"].ToString();
            fabricante.descricao = registro["descricao"].ToString();
            fabricante.link = registro["link"].ToString();

            return fabricante;
        }

        protected override void SetTabela()
        {
            Tabela = "Projeto";
        }

        public List<ProjetoViewModel> ListagemComFiltro(string nome)
        {
            var tabela = HelperDAO.ExecutaFuncSelect("dbo.func_consultarProjeto", nome);
            var lista = new List<ProjetoViewModel>();

            if (tabela.Rows.Count == 0)
                return lista;
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

        public void Insert(ProjetoViewModel model, List<int> idProdutos)
        {
            using (var transacao = new System.Transactions.TransactionScope())
            {
                ProjetoDAO projetoDAO = new ProjetoDAO();
                projetoDAO.Insert(model);                

                var p = new SqlParameter[2];
                p[0] = new SqlParameter("@id_Projeto", model.id);
                p[1] = new SqlParameter("@id_Produto", 0);


                foreach (int id in idProdutos)
                {
                    p[1].Value = id;

                    HelperDAO.ExecutaProc("spInsert_ProjetoProduto", p);
                }                
                transacao.Complete();
            }
        }

        public void Update(ProjetoViewModel model, List<int> idProdutos)
        {
            using (var transacao = new System.Transactions.TransactionScope())
            {
                ProjetoDAO projetoDAO = new ProjetoDAO();
                projetoDAO.Update(model);

                HelperDAO.ExecutaProc("spDelete_LimpaProjetoProduto", new SqlParameter[] { new SqlParameter("@id_Projeto", model.id) });

                var p = new SqlParameter[2];
                p[0] = new SqlParameter("@id_Projeto", model.id);
                p[1] = new SqlParameter("@id_Produto", 0);

                foreach (int id in idProdutos)
                {
                    p[1].Value = id;

                    HelperDAO.ExecutaProc("spInsert_ProjetoProduto", p);
                }
                transacao.Complete();
            }
        }
    }
}
