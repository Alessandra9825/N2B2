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
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("id_Cliente", model.id_Cliente);
            parametros[2] = new SqlParameter("quantidade", model.quantidade);           

            return parametros;
        }

        protected override CarrinhoViewModel MontaModel(DataRow registro)
        {
            CarrinhoViewModel carrinho = new CarrinhoViewModel();
            carrinho.id = Convert.ToInt32(registro["id"]);
            carrinho.id_Cliente = Convert.ToInt32(registro["id_Cliente"]);
            carrinho.quantidade = Convert.ToInt32(registro["quantidade"]);

            return carrinho;
        }

        protected override void SetTabela()
        {
            Tabela = "Carrinho";
        }
    }
}
