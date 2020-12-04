using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerCategoria
    {
        private ModelCategoria myModelCategoria;
        private Validar myValidar;
        public string DS_Mensagem { get; set; }

        public ControllerCategoria() { }

        public ControllerCategoria(string nm_categoria, string ds_categoria, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields("", nm_categoria, ds_categoria, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe categoria, carregar tela e incluir
                myModelCategoria = new ModelCategoria(nm_categoria, ds_categoria, connectionString);
                DS_Mensagem = myModelCategoria.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerCategoria(string id_categoria, string nm_categoria, string ds_categoria, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields(id_categoria, nm_categoria, ds_categoria, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe categoria, carregar tela e alterar
                myModelCategoria = new ModelCategoria(Convert.ToInt32(id_categoria), nm_categoria, ds_categoria, connectionString);
                DS_Mensagem = myModelCategoria.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerCategoria(string id_categoria, char acao, string connectionString)
        {
            myModelCategoria = new ModelCategoria(Convert.ToInt32(id_categoria), acao, connectionString);
            DS_Mensagem = myModelCategoria.DS_Mensagem;
        }


        public DataTable Exibir(string status, string connectionString)
        {
            myModelCategoria = new ModelCategoria();
            return myModelCategoria.Exibir(Convert.ToInt32(status), connectionString);
        }

        public DataTable Consultar(string status, string texto, string connectionString)
        {
            myModelCategoria = new ModelCategoria();
            return myModelCategoria.Consultar(Convert.ToInt32(status), texto, connectionString);
        }

        public string VerificarCategoriaCadastrada(string id_categoria, string nm_categoria, string connectionString)
        {
            myModelCategoria = new ModelCategoria();
            return myModelCategoria.VerificarCategoriaCadastrada(id_categoria, nm_categoria, connectionString);
        }

        private string ValidateFields(string id_categoria, string nm_categoria, string ds_categoria, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(nm_categoria))
            {
                if (!myValidar.TamanhoCampo(nm_categoria, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + nm_categoria.Length + ".";
                }
                else
                {
                    if (!myValidar.Letra(nm_categoria))
                    {
                        mDs_Msg = " O nome deve conter somente letras";
                    }
                    else
                    {
                        if (VerificarCategoriaCadastrada(id_categoria, nm_categoria, connectionString).Equals(""))
                        {
                            if (myValidar.CampoPreenchido(ds_categoria))
                            {
                                if (!myValidar.TamanhoCampo(ds_categoria, 1500))
                                {
                                    mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                  "o limite para este campo é: 1500 caracteres, " +
                                                  "quantidade utilizada: " + ds_categoria.Length + ".";
                                }
                            }
                            else
                            {
                                mDs_Msg += " A descrição deve estar preenchida.";
                            }
                        }
                        else
                        {
                            mDs_Msg += " Categoria já cadastrada. Verifique nas categorias ativas e inativas!";
                        }
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