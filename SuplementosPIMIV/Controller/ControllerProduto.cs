using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerProduto
    {
        private ModelProduto myModelProduto;
        private Validar myValidar;
        public int ID_Produto { get; set; }
        public string NM_Produto { get; set; }
        public double PR_Custo { get; set; }
        public double PR_Venda { get; set; }
        public string DS_Mensagem { get; set; }

        public ControllerProduto() { }

        public ControllerProduto(string id_marca, string id_categoria, string id_subcategoria, string id_sabor, string nr_ean, string nm_produto, string ds_produto, string pr_custo, string pr_venda,
            string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields("", id_marca, id_categoria, id_subcategoria, id_sabor, nr_ean, nm_produto, ds_produto, pr_custo, pr_venda, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e incluir
                myModelProduto = new ModelProduto(Convert.ToInt32(id_marca), Convert.ToInt32(id_categoria), Convert.ToInt32(id_subcategoria), Convert.ToInt32(id_sabor), nr_ean, nm_produto,
                    ds_produto, Convert.ToDouble(pr_custo), Convert.ToDouble(pr_venda), connectionString);
                ID_Produto = myModelProduto.ID_Produto;
                DS_Mensagem = myModelProduto.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerProduto(string id_produto, string id_marca, string id_categoria, string id_subcategoria, string id_sabor, string nr_ean, string nm_produto, string ds_produto, string pr_custo,
        string pr_venda, string connectionString)
        {
            // validar a entrada de dados para alteração
            string mDs_Msg = ValidateFields(id_produto, id_marca, id_categoria, id_subcategoria, id_sabor, nr_ean, nm_produto, ds_produto, pr_custo, pr_venda, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e alterar
                myModelProduto = new ModelProduto(Convert.ToInt32(id_produto), Convert.ToInt32(id_marca), Convert.ToInt32(id_categoria), Convert.ToInt32(id_subcategoria), Convert.ToInt32(id_sabor),
                    nr_ean, nm_produto, ds_produto, Convert.ToDouble(pr_custo), Convert.ToDouble(pr_venda), connectionString);
                DS_Mensagem = myModelProduto.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerProduto(string id_produto, char acao, string connectionString)
        {
            myModelProduto = new ModelProduto(Convert.ToInt32(id_produto), acao, connectionString);
            DS_Mensagem = myModelProduto.DS_Mensagem;
        }

        public DataTable ListarProdutos(string status, string connectionString)
        {
            myModelProduto = new ModelProduto();
            return myModelProduto.ListarProdutos(Convert.ToInt32(status), connectionString);
        }

        public DataTable Exibir(string status, string connectionString)
        {
            myModelProduto = new ModelProduto();
            return myModelProduto.Exibir(Convert.ToInt32(status), connectionString);
        }

        public DataTable Consultar(string status, string filtro, string connectionString)
        {
            myModelProduto = new ModelProduto();

            DataTable dataTable = new DataTable();
            dataTable = myModelProduto.Consultar(Convert.ToInt32(status), filtro, connectionString);

            ID_Produto = myModelProduto.ID_Produto;
            NM_Produto = myModelProduto.NM_Produto;
            PR_Custo = myModelProduto.PR_Custo;
            PR_Venda = myModelProduto.PR_Venda;

            return dataTable;
        }

        public string VerificarProdutoCadastrado(string id_produto, string nr_ean, string nm_produto, string id_marca, string connectionString)
        {
            myModelProduto = new ModelProduto();
            return myModelProduto.VerificarProdutoCadastrado(id_produto, nr_ean, nm_produto, id_marca, connectionString);
        }

        private string ValidateFields(string id_produto, string id_marca, string id_categoria, string id_subcategoria, string id_sabor, string nr_ean, string nm_produto, string ds_produto, string pr_custo,
            string pr_venda, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(nr_ean))
            {
                if (!myValidar.TamanhoCampo(nr_ean, 13))
                {
                    mDs_Msg = " Limite de caracteres para o EAN excedido, " +
                                  "o limite para este campo é: 13 caracteres, " +
                                  "quantidade utilizada: " + nr_ean.Length + ".";
                }
                else
                {
                    if (!myValidar.Numero(nr_ean))
                    {
                        mDs_Msg = " O EAN deve ser numérico.";
                    }
                    else
                    {
                        if (!myValidar.EAN(nr_ean))
                        {
                            mDs_Msg = " EAN inválido.";
                        }
                        else
                        {
                            if (myValidar.CampoPreenchido(nm_produto))
                            {
                                if (!myValidar.TamanhoCampo(nm_produto, 50))
                                {
                                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                                  "o limite para este campo é: 50 caracteres, " +
                                                  "quantidade utilizada: " + nm_produto.Length + ".";
                                }
                                else
                                {
                                    if (id_marca.Equals("Marca"))
                                    {
                                        mDs_Msg = " É necessário selecionar uma marca.";
                                    }
                                    else
                                    {
                                        string verificaProduto = VerificarProdutoCadastrado(id_produto, nr_ean, nm_produto, id_marca, connectionString);

                                        if (verificaProduto.Equals(""))
                                        {
                                            if (myValidar.CampoPreenchido(ds_produto))
                                            {
                                                if (!myValidar.TamanhoCampo(ds_produto, 1500))
                                                {
                                                    mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                                  "o limite para este campo é: 1500 caracteres, " +
                                                                  "quantidade utilizada: " + ds_produto.Length + ".";
                                                }
                                            }
                                            else
                                            {
                                                mDs_Msg += " A descrição deve estar preenchida.";
                                            }

                                            if (id_categoria.Equals("Categoria"))
                                            {
                                                mDs_Msg += " É necessário selecionar uma categoria.";
                                            }

                                            if (id_subcategoria.Equals("Subcategoria") || id_subcategoria.Equals(""))
                                            {
                                                mDs_Msg += " É necessário selecionar uma subcategoria.";
                                            }

                                            if (id_sabor.Equals("Sabor"))
                                            {
                                                mDs_Msg += " É necessário selecionar um sabor.";
                                            }

                                            if (myValidar.CampoPreenchido(pr_custo))
                                            {
                                                if (!myValidar.Valor(pr_custo))
                                                {
                                                    mDs_Msg += " O preço de custo deve ser um valor numérico, no formato: 9.999.999,99.";
                                                }
                                            }
                                            else
                                            {
                                                mDs_Msg += " O preço de custo deve estar preenchido.";
                                            }

                                            if (myValidar.CampoPreenchido(pr_venda))
                                            {
                                                if (!myValidar.Valor(pr_venda))
                                                {
                                                    mDs_Msg += " O preço de venda deve ser um valor numérico, no formato: 9.999.999,99.";
                                                }
                                            }
                                            else
                                            {
                                                mDs_Msg += " O preço de venda deve estar preenchido.";
                                            }
                                        }
                                        else
                                        {
                                            mDs_Msg += " " + verificaProduto + " Verifique nos produtos ativos e inativos!";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                mDs_Msg = " O nome deve estar preenchido.";
                            }
                        }
                    }
                }
            }
            else
            {
                mDs_Msg = " O código de barras deve estar preenchido.";
            }

            return mDs_Msg;
        }
    }
}