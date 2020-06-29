using EletroStar.Models.Principais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO.Principais
{
    public class ImagensDAO : PadraoDAO<ImagensViewModel>
    {
        protected override SqlParameter[] CriaParametros(ImagensViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("imagem", model.imagem);
            parametros[2] = new SqlParameter("id_Produto", model.id_Produto);

            return parametros;
        }

        protected override ImagensViewModel MontaModel(DataRow registro)
        {
            ImagensViewModel imagem = new ImagensViewModel();
            imagem.id = Convert.ToInt32(registro["id"]);
            imagem.imagem = registro["imagem"].ToString();
            imagem.id_Produto = Convert.ToInt32(registro["id_Produto"]);


            return imagem;
        }

        protected override void SetTabela()
        {
            Tabela = "Imagens";
        }
    }
}
