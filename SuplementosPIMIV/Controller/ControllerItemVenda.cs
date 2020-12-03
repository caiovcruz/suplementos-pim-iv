using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerItemVenda
    {
        private ModelItemVenda myModelItemVenda;
        private Validar myValidar;
        public string DS_Mensagem { get; set; }

        public ControllerItemVenda() { }

        public ControllerItemVenda(string id_venda, string id_produto, string qtd_itemVenda, string pr_produto, string vl_lucroProduto, char acao, string connectionString)
        {
            // validar a entrada de dados para inclusão/alteração
            string mDs_Msg = ValidateFields(id_produto, qtd_itemVenda, connectionString);

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe itemvenda, carregar tela e incluir/alterar
                myModelItemVenda = new ModelItemVenda(Convert.ToInt32(id_venda), Convert.ToInt32(id_produto), Convert.ToInt32(qtd_itemVenda),
                    Convert.ToDouble(pr_produto) * Convert.ToDouble(qtd_itemVenda), Convert.ToDouble(vl_lucroProduto) * Convert.ToDouble(qtd_itemVenda), acao, connectionString);
                DS_Mensagem = myModelItemVenda.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerItemVenda(string id_venda, string id_produto, string connectionString)
        {
            myModelItemVenda = new ModelItemVenda(Convert.ToInt32(id_venda), Convert.ToInt32(id_produto), connectionString);
            DS_Mensagem = myModelItemVenda.DS_Mensagem;
        }

        public DataTable Exibir(string id_venda, string connectionString)
        {
            myModelItemVenda = new ModelItemVenda();
            return myModelItemVenda.Exibir(Convert.ToInt32(id_venda), connectionString);
        }

        private string ValidateFields(string id_produto, string qtd_itemVenda, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            ControllerEstoque myControllerEstoque = new ControllerEstoque();

            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(qtd_itemVenda))
            {
                if (!myValidar.Numero(qtd_itemVenda))
                {
                    mDs_Msg += " A quantidade deve conter somente números.";
                }
                else
                {
                    int qtd_estoque = myControllerEstoque.QuantidadeTotalEstoque(id_produto, connectionString);

                    if (qtd_estoque < Convert.ToInt32(qtd_itemVenda))
                    {
                        mDs_Msg = "Quantidade indisponível para venda [ Quantidade máxima disponível: " + qtd_estoque + " ].";
                    }
                }
            }
            else
            {
                mDs_Msg += " A quantidade deve estar preenchida.";
            }

            return mDs_Msg;
        }
    }
}