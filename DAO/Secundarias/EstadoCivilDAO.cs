using EletroStar.Models.Secundarias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Secundarias
{
    public class EstadoCivilDAO : PadraoDAO<EstadoCivilViewModel>
    {
        protected override SqlParameter[] CriaParametros(EstadoCivilViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("descricao", model.descricao);

            return parametros;
        }

        protected override EstadoCivilViewModel MontaModel(DataRow registro)
        {
            EstadoCivilViewModel estado = new EstadoCivilViewModel();
            estado.id = Convert.ToInt32(registro["id"]);
            estado.descricao = registro["descricao"].ToString();


            return estado;
        }

        protected override void SetTabela()
        {
            Tabela = "EstadoCivil";
        }
    }
}
