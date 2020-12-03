using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSabor
    {
        private ModelSabor myModelSabor;
        private Validar myValidar;
        public string DS_Mensagem { get; set; }

        public ControllerSabor() { }

        public ControllerSabor(string nm_sabor, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields("", nm_sabor, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe sabor, carregar tela e incluir
                myModelSabor = new ModelSabor(nm_sabor, connectionString);
                DS_Mensagem = myModelSabor.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerSabor(string id_sabor, string nm_sabor, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields(id_sabor, nm_sabor, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe sabor, carregar tela e alterar
                myModelSabor = new ModelSabor(Convert.ToInt32(id_sabor), nm_sabor, connectionString);
                DS_Mensagem = myModelSabor.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerSabor(string id_sabor, char acao, string connectionString)
        {
            myModelSabor = new ModelSabor(Convert.ToInt32(id_sabor), acao, connectionString);
            DS_Mensagem = myModelSabor.DS_Mensagem;
        }

        public DataTable Exibir(string status, string connectionString)
        {
            myModelSabor = new ModelSabor();
            return myModelSabor.Exibir(Convert.ToInt32(status), connectionString);
        }

        public DataTable Consultar(string status, string texto, string connectionString)
        {
            myModelSabor = new ModelSabor();
            return myModelSabor.Consultar(Convert.ToInt32(status), texto, connectionString);
        }

        public string VerificarSaborCadastrado(string id_sabor, string nm_sabor, string connectionString)
        {
            myModelSabor = new ModelSabor();
            return myModelSabor.VerificarSaborCadastrado(id_sabor, nm_sabor, connectionString);
        }

        private string ValidateFields(string id_sabor, string nm_sabor, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(nm_sabor))
            {
                if (!myValidar.TamanhoCampo(nm_sabor, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + nm_sabor.Length + ".";
                }
                else
                {
                    if (!VerificarSaborCadastrado(id_sabor, nm_sabor, connectionString).Equals(""))
                    {
                        mDs_Msg += " Sabor já cadastrado. Verifique nos sabores ativos e inativos!";
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