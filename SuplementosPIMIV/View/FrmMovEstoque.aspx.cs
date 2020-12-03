using SuplementosPIMIV.Controller;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmEstoque : System.Web.UI.Page
    {
        private ControllerMovEstoque myControllerMovEstoque;
        private ControllerProduto myControllerProduto;
        private Validar myValidar;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
            {
                if (Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                {
                    if (!IsPostBack)
                    {
                        LimparCampos();
                        CarregarMovimentacoesEstoque();
                        CarregarProdutos();
                        CarregarTiposMovimentacao();
                        BloquearComponentesCadastro();
                        BloquearComponentesExibe();

                        lblNM_FuncionarioLogin.Text = Session["NM_FuncionarioLogin"].ToString();
                    }
                }
                else
                {
                    Response.Redirect("FrmPDV.aspx");
                }
            }
            else
            {
                Response.Redirect("FrmLogin.aspx");
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
            txbDT_MovimentacaoEstoque.Enabled = false;
        }

        private void BloquearComponentesExibe()
        {
            btnConsultar.Enabled = false;
        }

        private void CarregarMovimentacoesEstoque()
        {
            // instanciando um objeto da classe ControllerMovEstoque
            myControllerMovEstoque = new ControllerMovEstoque();

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerMovEstoque.Exibir(Session["ConnectionString"].ToString());

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarMovimentacoesEstoqueConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_ProdutoConsultar.Text, 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_ProdutoConsultar.Text.Trim().Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe marca, carregar tela e consultar
                myControllerMovEstoque = new ControllerMovEstoque();
                gvwExibe.DataSource = myControllerMovEstoque.Consultar(txbNM_ProdutoConsultar.Text.Trim(), Session["ConnectionString"].ToString());
                gvwExibe.DataBind();
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void CarregarProdutos()
        {
            myControllerProduto = new ControllerProduto();

            ddlID_ProdutoMovimentacaoEstoque.DataSource = myControllerProduto.ListarProdutos("1", Session["ConnectionString"].ToString());
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
            ddlDS_MovimentacaoEstoque.Items.Insert(3, "Venda");

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

        private void Incluir()
        {
            myControllerMovEstoque = new ControllerMovEstoque(
                ddlID_ProdutoMovimentacaoEstoque.SelectedValue,
                txbQTD_MovimentacaoEstoque.Text.Trim(),
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

        private void Excluir()
        {
            // instanciar um objeto da classe movestoque e carregar tela e excluir
            myControllerMovEstoque = new ControllerMovEstoque(txbID_MovimentacaoEstoque.Text.Trim(), Session["ConnectionString"].ToString());

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
            CarregarMovimentacoesEstoqueConsultar();
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
                e.Row.Cells[4].Text = "Marca";
                e.Row.Cells[5].Text = "Sabor";
                e.Row.Cells[6].Text = "Quantidade";
                e.Row.Cells[7].Text = "Movimentação";
                e.Row.Cells[8].Text = "Data";
                e.Row.Cells[9].Text = "Estoque total";
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

            txbQTD_MovimentacaoEstoque.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[6].Text.Trim());

            try
            {
                ddlDS_MovimentacaoEstoque.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[7].Text.Trim());
            }
            catch (Exception)
            {
                lblDS_Mensagem.Text += " Movimentação [ " + gvwExibe.SelectedRow.Cells[7].Text.Trim() + " ] inativa.";
                ddlDS_MovimentacaoEstoque.SelectedIndex = 0;
            }

            txbDT_MovimentacaoEstoque.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[8].Text.Trim());

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

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            CarregarMovimentacoesEstoque();
        }
    }
}