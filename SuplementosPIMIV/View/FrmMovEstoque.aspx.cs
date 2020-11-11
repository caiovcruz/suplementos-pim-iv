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
        private ControllerProduto myControllerProduto;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarProdutos();
                CarregarTiposMovimentacao();
                BloquearComponentes();
            }
        }

        private void LimparCampos()
        {
            txbID_MovimentacaoEstoque.Text = "";
            ddlID_ProdutoMovimentacaoEstoque.SelectedIndex = 0;
            txbQTD_MovimentacaoEstoque.Text = "";
            ddlDS_MovimentacaoEstoque.SelectedIndex = 0;
            Calendar.SelectedDate = DateTime.Now;
            lblDS_Mensagem.Text = "";           
        }

        private void BloquearComponentes()
        {
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            btnConsultar.Enabled = false;
            txbNM_ProdutoConsultar.Enabled = false;
            Calendar.Enabled = false;
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

        protected void btnLimpar_Click(object sender, EventArgs e)
        {

        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {

        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        protected void txbNM_ProdutoConsultar_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlID_ProdutoMovimentacaoEstoque_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txbQTD_MovimentacaoEstoque_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDS_MovimentacaoEstoque_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}