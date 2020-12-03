using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSubcategoria
    {
        private ModelSubcategoria myModelSubcategoria;
        private Validar myValidar;
        public string DS_Mensagem { get; set; }

        public ControllerSubcategoria() { }

        public ControllerSubcategoria(string id_categoria, string nm_subcategoria, string ds_subcategoria, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields("", id_categoria, nm_subcategoria, ds_subcategoria, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe subcategoria, carregar tela e incluir
                myModelSubcategoria = new ModelSubcategoria(Convert.ToInt32(id_categoria), nm_subcategoria, ds_subcategoria, connectionString);
                DS_Mensagem = myModelSubcategoria.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerSubcategoria(string id_subcategoria, string id_categoria, string nm_subcategoria, string ds_subcategoria, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields(id_subcategoria, id_categoria, nm_subcategoria, ds_subcategoria, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe subcategoria, carregar tela e alterar
                myModelSubcategoria = new ModelSubcategoria(Convert.ToInt32(id_subcategoria), Convert.ToInt32(id_categoria), nm_subcategoria, ds_subcategoria, connectionString);
                DS_Mensagem = myModelSubcategoria.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerSubcategoria(string id_subcategoria, char acao, string connectionString)
        {
            myModelSubcategoria = new ModelSubcategoria(Convert.ToInt32(id_subcategoria), acao, connectionString);
            DS_Mensagem = myModelSubcategoria.DS_Mensagem;
        }

        public DataTable Exibir(string status, string connectionString)
        {
            myModelSubcategoria = new ModelSubcategoria();
            return myModelSubcategoria.Exibir(Convert.ToInt32(status), connectionString);
        }

        public DataTable Consultar(string status, string texto, string connectionString)
        {
            myModelSubcategoria = new ModelSubcategoria();
            return myModelSubcategoria.Consultar(Convert.ToInt32(status), texto, connectionString);
        }

        public DataTable Filtrar(string status, string id_categoria, string connectionString)
        {
            myModelSubcategoria = new ModelSubcategoria();
            return myModelSubcategoria.Filtrar(Convert.ToInt32(status), Convert.ToInt32(id_categoria), connectionString);
        }

        public string VerificarSubcategoriaCadastrada(string id_subcategoria, string nm_subcategoria, string id_categoria, string connectionString)
        {
            myModelSubcategoria = new ModelSubcategoria();
            return myModelSubcategoria.VerificarSubcategoriaCadastrada(id_subcategoria, nm_subcategoria, id_categoria, connectionString);
        }

        private string ValidateFields(string id_subcategoria, string id_categoria, string nm_subcategoria, string ds_subcategoria, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(nm_subcategoria))
            {
                if (!myValidar.TamanhoCampo(nm_subcategoria, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + nm_subcategoria.Length + ".";
                }
                else
                {
                    if (id_categoria.Equals("Categoria"))
                    {
                        mDs_Msg = " É necessário selecionar uma categoria base.";
                    }
                    else
                    {
                        if (VerificarSubcategoriaCadastrada(id_subcategoria, nm_subcategoria, id_categoria, connectionString).Equals(""))
                        {
                            if (myValidar.CampoPreenchido(ds_subcategoria))
                            {
                                if (!myValidar.TamanhoCampo(ds_subcategoria, 1500))
                                {
                                    mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                  "o limite para este campo é: 1500 caracteres, " +
                                                  "quantidade utilizada: " + ds_subcategoria.Length + ".";
                                }
                            }
                            else
                            {
                                mDs_Msg += " A descrição deve estar preenchida.";
                            }
                        }
                        else
                        {
                            mDs_Msg += " Subcategoria já cadastrada para a categoria selecionada. Verifique nas subcategorias ativas e inativas!";
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