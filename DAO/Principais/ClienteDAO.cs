using EletroStar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.DAO
{
    public class ClienteDAO : PadraoDAO<ClienteViewModel>
    {
        protected override SqlParameter[] CriaParametros(ClienteViewModel model)
        {
            object imgByte = model.ImagemEmByte;
            if (imgByte == null)
                imgByte = DBNull.Value;


            SqlParameter[] parametros = new SqlParameter[12];
            parametros[0] = new SqlParameter("id", model.id);
            parametros[1] = new SqlParameter("cpf", model.cpf);
            parametros[2] = new SqlParameter("nome", model.nome);
            parametros[3] = new SqlParameter("id_EstadoCivil", model.id_EstadoCivil);
            parametros[4] = new SqlParameter("email", model.email);
            parametros[5] = new SqlParameter("nascimento", model.nascimento);
            parametros[6] = new SqlParameter("genero", model.genero);
            parametros[7] = new SqlParameter("tel_residencial", model.tel_residencial);
            parametros[8] = new SqlParameter("tel_celular", model.tel_celular);
            parametros[9] = new SqlParameter("admin", model.admin);
            parametros[10] = new SqlParameter("senha", model.senha);
            parametros[11] = new SqlParameter("imagem", imgByte);

            return parametros;
        }

        protected override ClienteViewModel MontaModel(DataRow registro)
        {
            ClienteViewModel cliente = new ClienteViewModel();
            cliente.id = Convert.ToInt32(registro["id"]);
            cliente.cpf = registro["cpf"].ToString();
            cliente.nome = registro["nome"].ToString();
            cliente.id_EstadoCivil = Convert.ToInt32(registro["id_EstadoCivil"]);
            cliente.email = registro["email"].ToString();
            cliente.nascimento = Convert.ToDateTime(registro["nascimento"]);
            cliente.genero = Convert.ToChar(registro["genero"]);
            cliente.tel_residencial = registro["tel_residencial"].ToString();
            cliente.tel_celular = registro["tel_celular"].ToString();
            cliente.admin = Convert.ToBoolean(registro["admin"]);
            cliente.senha = registro["senha"].ToString();

            if (registro["imagem"] != DBNull.Value)
                cliente.ImagemEmByte = registro["imagem"] as byte[];

            return cliente;
        }

        protected override void SetTabela()
        {
            Tabela = "Cliente";
        }
        public List<ClienteViewModel> ListagemComFiltro(string cpf)
        {
         

            var tabela = HelperDAO.ExecutaFuncSelect("dbo.func_consultarCliente",cpf);
            var lista = new List<ClienteViewModel>();

            if (tabela.Rows.Count == 0)
                return lista;
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

    }
}

