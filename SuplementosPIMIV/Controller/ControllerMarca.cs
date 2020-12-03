using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerMarca
    {
        private ModelMarca myModelMarca;
        private Validar myValidar;
        public string DS_Mensagem { get; set; }

        public ControllerMarca() { }

        public ControllerMarca(string nm_marca, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields("", nm_marca, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe marca, carregar tela e incluir
                myModelMarca = new ModelMarca(nm_marca, connectionString);
                DS_Mensagem = myModelMarca.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerMarca(string id_marca, string nm_marca, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields(id_marca, nm_marca, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe sabor, carregar tela e alterar
                myModelMarca = new ModelMarca(Convert.ToInt32(id_marca), nm_marca, connectionString);
                DS_Mensagem = myModelMarca.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerMarca(string id_marca, char acao, string connectionString)
        {
            myModelMarca = new ModelMarca(Convert.ToInt32(id_marca), acao, connectionString);
            DS_Mensagem = myModelMarca.DS_Mensagem;
        }

        public DataTable Exibir(string status, string connectionString)
        {
            myModelMarca = new ModelMarca();
            return myModelMarca.Exibir(Convert.ToInt32(status), connectionString);
        }

        public DataTable Consultar(string status, string texto, string connectionString)
        {
            myModelMarca = new ModelMarca();
            return myModelMarca.Consultar(Convert.ToInt32(status), texto, connectionString);
        }

        public string VerificarMarcaCadastrada(string id_marca, string nm_marca, string connectionString)
        {
            myModelMarca = new ModelMarca();
            return myModelMarca.VerificarMarcaCadastrada(id_marca, nm_marca, connectionString);
        }

        private string ValidateFields(string id_marca, string nm_marca, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(nm_marca))
            {
                if (!myValidar.TamanhoCampo(nm_marca, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + nm_marca.Length + ".";
                }
                else
                {
                    if (!VerificarMarcaCadastrada(id_marca, nm_marca, connectionString).Equals(""))
                    {
                        mDs_Msg += " Marca já cadastrada. Verifique nas marcas ativas e inativas!";
                    }
                }
            }
            else
            {
                mDs_Msg = " O nome deve estar preenchido.";
            }

            return mDs_Msg;
        }
    }
}