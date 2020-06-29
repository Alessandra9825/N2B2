using EletroStar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO
{
    public class PedidoDAO: PadraoDAO<PedidoViewModel>
    {
        protected override SqlParameter[] CriaParametros(PedidoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[13];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("id_Cliente", model.id_Cliente);
            parametros[2] = new SqlParameter("data", model.data);
            parametros[3] = new SqlParameter("valor", model.valor);
            parametros[4] = new SqlParameter("quantidade", model.quantidade);
            parametros[5] = new SqlParameter("id_Status", model.id_Status);
            parametros[6] = new SqlParameter("prazo", model.prazo);
            parametros[7] = new SqlParameter("id_Pagamento", model.id_Pagamento);
            parametros[8] = new SqlParameter("id_Carrinho", model.id_Carrinho);

            return parametros;
        }

        protected override PedidoViewModel MontaModel(DataRow registro)
        {
            PedidoViewModel pedido = new PedidoViewModel();
            pedido.id = Convert.ToInt32(registro["id"]);
            pedido.id_Cliente = Convert.ToInt32(registro["id_Cliente"]);
            pedido.data = Convert.ToDateTime(registro["data"]);
            pedido.valor = Convert.ToDouble(registro["valor"]);
            pedido.quantidade = Convert.ToInt32(registro["quantidade"]);
            pedido.id_Status = Convert.ToInt32(registro["id_Status"]);
            pedido.prazo = Convert.ToInt32(registro["prazo"]);
            pedido.id_Pagamento = Convert.ToInt32(registro["id_Pagamento"]);
            pedido.id_Carrinho = Convert.ToInt32(registro["id_Carrinho"]);

            return pedido;
        }

        protected override void SetTabela()
        {
            Tabela = "Pedido";
        }
    }
}
