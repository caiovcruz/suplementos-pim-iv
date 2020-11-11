using SuplementosPIMIV.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmEstoque : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerMovEstoque myControllerMovEstoque;
        private ControllerEstoque myControllerEstoque;
        private ControllerProduto myControllerProduto;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarMovimentacoesEstoque();
                CarregarProdutos();
                CarregarTiposMovimentacao();
                BloquearComponentesCadastro();
                BloquearComponentesExibe();
            }
        }

        private void LimparCampos()
        {
            txbID_MovimentacaoEstoque.Text = "";
            ddlID_ProdutoMovimentacaoEstoque.SelectedIndex = 0;
            txbQTD_MovimentacaoEstoque.Text = "";
            ddlDS_MovimentacaoEstoque.SelectedIndex = 0;
            txbDT_MovimentacaoEstoque.Text = DateTime.Now.ToString();
            lblDS_Mensagem.Text = "";           
        }

        private void BloquearComponentesCadastro()
        {
            btnIncluir.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            btnConsultar.Enabled = false;
            txbNM_ProdutoConsultar.Enabled = false;
            txbDT_MovimentacaoEstoque.Enabled = false;
        }

        private void BloquearComponentesExibe()
        {
            btnConsultar.Enabled = false;
        }

        private void CarregarMovimentacoesEstoque()
        {
            // instanciando um objeto da classe ControllerMovEstoque
            myControllerMovEstoque = new ControllerMovEstoque(Session["ConnectionString"].ToString());

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerMovEstoque.Exibir();

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarProdutos()
        {
            myControllerProduto = new ControllerProduto(Session["ConnectionString"].ToString());

            ddlID_ProdutoMovimentacaoEstoque.DataSource = myControllerProduto.Exibir(1);
            ddlID_ProdutoMovimentacaoEstoque.DataTextField = "NM_Produto";
            ddlID_ProdutoMovimentacaoEstoque.DataValueField = "ID_Produto";
            ddlID_ProdutoMovimentacaoEstoque.DataBind();

            ddlID_ProdutoMovimentacaoEstoque.Items.Insert(0, "Produto");
            ddlID_ProdutoMovimentacaoEstoque.SelectedIndex = 0;
        }

        private void CarregarTiposMovimentacao()
        {
            ddlDS_MovimentacaoEstoque.Items.Insert(0, "Movimentação");
            ddlDS_MovimentacaoEstoque.Items.Insert(1, "Entrada");
            ddlDS_MovimentacaoEstoque.Items.Insert(2, "Saída");

            ddlDS_MovimentacaoEstoque.SelectedIndex = 0;
        }

        private void IncludeFields()
        {
            btnIncluir.Enabled =
                ddlID_ProdutoMovimentacaoEstoque.SelectedIndex != 0 &&
                txbQTD_MovimentacaoEstoque.Text.Trim().Length > 0 &&
                ddlDS_MovimentacaoEstoque.SelectedIndex != 0;

            btnLimpar.Enabled =
                ddlID_ProdutoMovimentacaoEstoque.SelectedIndex != 0 ||
                txbQTD_MovimentacaoEstoque.Text.Trim().Length > 0 ||
                ddlDS_MovimentacaoEstoque.SelectedIndex != 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            myControllerEstoque = new ControllerEstoque(Session["ConnectionString"].ToString());
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbQTD_MovimentacaoEstoque.Text.Trim()))
            {
                if (!myValidar.Numero(txbQTD_MovimentacaoEstoque.Text.Trim()))
                {
                    mDs_Msg += " A quantidade da movimentação deve conter somente números.";
                }
                else
                {
                    int qtd_movimentacaoEstoque = Convert.ToInt32(txbQTD_MovimentacaoEstoque.Text.Trim());
                    int qtd_estoque = myControllerEstoque.QuantidadeTotalEstoque(Convert.ToInt32(ddlID_ProdutoMovimentacaoEstoque.SelectedValue));

                    if (ddlDS_MovimentacaoEstoque.SelectedValue.Equals("Saída") && qtd_movimentacaoEstoque > qtd_estoque)
                    {
                        mDs_Msg += " Quantidade ultrapassada para movimentação de saída [ Quantidade máxima disponível: " + qtd_estoque + " ].";
                    }
                }
            }
            else
            {
                mDs_Msg += " A quantidade da movimentação deve estar preenchida.";
            }

            return mDs_Msg;
        }

        private void Incluir()
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe movestoque, carregar tela e incluir
                myControllerMovEstoque = new ControllerMovEstoque(
                    Convert.ToInt32(ddlID_ProdutoMovimentacaoEstoque.SelectedValue),
                    Convert.ToInt32(txbQTD_MovimentacaoEstoque.Text.Trim()),
                    ddlDS_MovimentacaoEstoque.SelectedValue,
                    DateTime.Now,
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerMovEstoque.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearComponentesCadastro();
                    CarregarMovimentacoesEstoque();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerMovEstoque.DS_Mensagem;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void Excluir()
        {
            // instanciar um objeto da classe movestoque e carregar tela e excluir
            myControllerMovEstoque = new ControllerMovEstoque(Convert.ToInt32(txbID_MovimentacaoEstoque.Text.Trim()), Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerMovEstoque.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarMovimentacoesEstoque();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerMovEstoque.DS_Mensagem;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            BloquearComponentesCadastro();
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Incluir();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        protected void txbNM_ProdutoConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_ProdutoConsultar.Text.Trim()))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarMovimentacoesEstoque();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[2].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Text = "Produto";
                e.Row.Cells[4].Text = "Quantidade";
                e.Row.Cells[5].Text = "Movimentação";
                e.Row.Cells[6].Text = "Data";
                e.Row.Cells[7].Text = "Estoque total";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDS_Mensagem.Text = "";

            txbID_MovimentacaoEstoque.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());

            try
            {
                ddlID_ProdutoMovimentacaoEstoque.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim());
            }
            catch (Exception)
            {
                lblDS_Mensagem.Text = "Produto [ " + gvwExibe.SelectedRow.Cells[3].Text.Trim() + " ] inativo.";
                ddlID_ProdutoMovimentacaoEstoque.SelectedIndex = 0;
            }

            txbQTD_MovimentacaoEstoque.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[4].Text.Trim());

            try
            {
                ddlDS_MovimentacaoEstoque.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[5].Text.Trim());
            }
            catch (Exception)
            {
                lblDS_Mensagem.Text += " Movimentação [ " + gvwExibe.SelectedRow.Cells[5].Text.Trim() + " ] inativa.";
                ddlDS_MovimentacaoEstoque.SelectedIndex = 0;
            }

            txbDT_MovimentacaoEstoque.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[6].Text.Trim());

            btnIncluir.Enabled = false;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void ddlID_ProdutoMovimentacaoEstoque_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbQTD_MovimentacaoEstoque.Focus();
        }

        protected void txbQTD_MovimentacaoEstoque_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlDS_MovimentacaoEstoque.Focus();
        }

        protected void ddlDS_MovimentacaoEstoque_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }
    }
}