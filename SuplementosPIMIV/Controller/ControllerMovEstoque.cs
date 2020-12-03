using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerMovEstoque
    {
        private ModelMovEstoque myModelMovEstoque;
        private Validar myValidar;
        public string DS_Mensagem { get; set; }

        public ControllerMovEstoque() { }

        public ControllerMovEstoque(string id_movimentacaoEstoque, string connectionString)
        {
            myModelMovEstoque = new ModelMovEstoque(Convert.ToInt32(id_movimentacaoEstoque), connectionString);
            DS_Mensagem = myModelMovEstoque.DS_Mensagem;
        }

        public ControllerMovEstoque(string id_produto, string qtd_movimentacaoEstoque, string ds_movimentacaoEstoque, DateTime dt_movimentacaoEstoque, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields(id_produto, qtd_movimentacaoEstoque, ds_movimentacaoEstoque, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe movestoque, carregar tela e incluir
                myModelMovEstoque = new ModelMovEstoque(Convert.ToInt32(id_produto), Convert.ToInt32(qtd_movimentacaoEstoque), ds_movimentacaoEstoque, dt_movimentacaoEstoque, connectionString);
                DS_Mensagem = myModelMovEstoque.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public DataTable Exibir(string connectionString)
        {
            myModelMovEstoque = new ModelMovEstoque();
            return myModelMovEstoque.Exibir(connectionString);
        }

        public DataTable Consultar(string texto, string connectionString)
        {
            myModelMovEstoque = new ModelMovEstoque();
            return myModelMovEstoque.Consultar(texto, connectionString);
        }

        private string ValidateFields(string id_produto, string qtd_movimentacaoEstoque, string ds_movimentacaoEstoque, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            ControllerEstoque myControllerEstoque = new ControllerEstoque();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(qtd_movimentacaoEstoque))
            {
                if (!myValidar.Numero(qtd_movimentacaoEstoque))
                {
                    mDs_Msg += " A quantidade da movimentação deve conter somente números.";
                }
                else
                {
                    int qtd_estoque = myControllerEstoque.QuantidadeTotalEstoque(id_produto, connectionString);

                    if (ds_movimentacaoEstoque.Equals("Saída") && Convert.ToInt32(qtd_movimentacaoEstoque) > qtd_estoque)
                    {
                        mDs_Msg += " Quantidade ultrapassada para movimentação de saída [ Quantidade máxima disponível: " + qtd_estoque + " ].";
                    }

                    if (ds_movimentacaoEstoque.Equals("Venda") && Convert.ToInt32(qtd_movimentacaoEstoque) > qtd_estoque)
                    {
                        mDs_Msg += " Quantidade ultrapassada para movimentação de venda [ Quantidade máxima disponível: " + qtd_estoque + " ].";
                    }
                }
            }
            else
            {
                mDs_Msg += " A quantidade da movimentação deve estar preenchida.";
            }

            return mDs_Msg;
        }
    }
}