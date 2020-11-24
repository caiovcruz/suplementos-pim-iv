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
    public partial class FrmPDV : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerProduto myControllerProduto;
        private ControllerEstoque myControllerEstoque;
        private ControllerVenda myControllerVenda;
        private ControllerItemVenda myControllerItemVenda;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ConnectionString"] != null)
                {
                    LimparCamposCadastro();
                    BloquearComponentesCadastro();
                    LimparCamposFinaliza();
                    BloquearComponentesFinaliza();
                    LimparMensagens();

                    lblNM_FuncionarioLogin.Text = Session["NM_FuncionarioLogin"].ToString();
                }
                else
                {
                    Response.Redirect("FrmLogin.aspx");
                }
            }
        }

        private void LimparMensagens()
        {
            lblDS_Mensagem.Text = "";
            lblDS_MensagemTroco.Text = "";
            lblDS_MensagemFinal.Text = "";
        }

        private void LimparCamposCadastro()
        {
            txbID_Produto.Text = "";
            txbNR_EAN.Text = "";
            txbProduto.Text = "";
            txbPR_Produto.Text = "";
            txbQTD_Produto.Text = "";
        }

        private void LimparCamposFinaliza()
        {
            gvwExibe.DataSource = null;
            gvwExibe.DataBind();
            txbDT_Venda.Text = "";
            ddlDS_TipoPagamento.Items.Clear();
            ddlNR_Parcelas.Items.Clear();
            txbVL_Total.Text = "";
            txbVL_Recebido.Text = "";
        }

        private void BloquearComponentesCadastro()
        {
            txbProduto.ReadOnly = true;
            txbPR_Produto.ReadOnly = true;
            txbQTD_Produto.Enabled = false;

            btnConsultar.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
        }

        private void BloquearComponentesFinaliza()
        {
            txbDT_Venda.ReadOnly = true;
            ddlDS_TipoPagamento.Enabled = false;
            ddlNR_Parcelas.Enabled = false;
            txbVL_Total.ReadOnly = true;

            lblVL_Recebido.Visible = false;
            txbVL_Recebido.Visible = false;
            lblDS_MensagemTroco.Visible = false;

            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void CarregarItensVenda()
        {
            // instanciando um objeto da classe ControllerItemVenda
            myControllerItemVenda = new ControllerItemVenda(Session["ConnectionString"].ToString());

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerItemVenda.Exibir(Convert.ToInt32(txbID_Venda.Text.Trim()));

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void ConsultarEANProduto()
        {
            lblDS_Mensagem.Text = "";
            txbID_Produto.Text = "";
            txbProduto.Text = "";
            txbPR_Produto.Text = "";

            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = "";


            if (myValidar.CampoPreenchido(txbNR_EAN.Text.Trim()))
            {
                if (txbNR_EAN.Text.Trim().Length > 13 || txbNR_EAN.Text.Trim().Length < 13)
                {
                    mDs_Msg = " O EAN (código de barras) deve conter exatamente 13 dígitos, " +
                                  "quantidade de dígitos utilizada: " + txbNR_EAN.Text.Trim().Length + ".";
                }
                else
                {
                    if (!myValidar.Numero(txbNR_EAN.Text.Trim()))
                    {
                        mDs_Msg = " O EAN deve ser numérico.";
                    }
                    else
                    {
                        if (!myValidar.EAN(txbNR_EAN.Text.Trim()))
                        {
                            mDs_Msg = " EAN inválido.";
                        }
                    }
                }
            }
            else
            {
                mDs_Msg = " O código de barras deve estar preenchido.";
            }



            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e consultar
                myControllerProduto = new ControllerProduto(Session["ConnectionString"].ToString());
                myControllerProduto.Consultar(1, "PROD.NR_EAN = '" + txbNR_EAN.Text.Trim() + "' ");

                txbID_Produto.Text = myControllerProduto.ID_Produto.ToString();
                txbProduto.Text = myControllerProduto.NM_Produto;
                txbPR_Produto.Text = myControllerProduto.PR_Venda.ToString("N2");

                myControllerEstoque = new ControllerEstoque(Session["ConnectionString"].ToString());

                if (myControllerEstoque.QuantidadeTotalEstoque(myControllerProduto.ID_Produto) <= 0)
                {
                    txbQTD_Produto.Text = "ESGOTADO";
                    txbQTD_Produto.ReadOnly = true;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void IncludeFields()
        {
            btnIncluir.Enabled =
                txbID_Produto.Text.Trim().Length > 0 &&
                txbProduto.Text.Trim().Length > 0 &&
                txbPR_Produto.Text.Trim().Length > 0 &&
                txbQTD_Produto.Text.Trim().Length > 0 &&
                btnAlterar.Enabled == false;

            btnLimpar.Enabled =
                txbProduto.Text.Trim().Length > 0 ||
                txbPR_Produto.Text.Trim().Length > 0 ||
                txbQTD_Produto.Text.Trim().Length > 0;
        }

        private void IncludeFieldsFinal()
        {
            btnSalvar.Enabled =
                txbDT_Venda.Text.Trim().Length > 0 &&
                ddlDS_TipoPagamento.SelectedIndex != 0 &&
                ddlNR_Parcelas.SelectedIndex != 0 &&
                txbVL_Total.Text.Trim().Length > 0;

            btnCancelar.Enabled =
                txbDT_Venda.Text.Trim().Length > 0 ||
                ddlDS_TipoPagamento.SelectedIndex != 0 ||
                ddlNR_Parcelas.SelectedIndex != 0 ||
                txbVL_Total.Text.Trim().Length > 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            myControllerEstoque = new ControllerEstoque(Session["ConnectionString"].ToString());

            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbQTD_Produto.Text.Trim()))
            {
                if (!myValidar.Numero(txbQTD_Produto.Text.Trim()))
                {
                    mDs_Msg += " A quantidade deve conter somente números.";
                }
                else
                {
                    int qtd_estoque = myControllerEstoque.QuantidadeTotalEstoque(Convert.ToInt32(txbID_Produto.Text.Trim()));

                    if (qtd_estoque < Convert.ToInt32(txbQTD_Produto.Text.Trim()))
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

        private void CarregarTiposPagamento()
        {
            ddlDS_TipoPagamento.Items.Insert(0, "Escolha");
            ddlDS_TipoPagamento.Items.Insert(1, "Dinheiro");
            ddlDS_TipoPagamento.Items.Insert(2, "Cartão de Débito");
            ddlDS_TipoPagamento.Items.Insert(3, "Cartão de Crédito");

            ddlDS_TipoPagamento.SelectedIndex = 0;
            ddlDS_TipoPagamento.Enabled = true;
        }

        private void CarregarNParcelas()
        {
            ddlNR_Parcelas.Items.Insert(0, "Escolha");

            for (int i = 1; i <= 12; i++)
            {
                ddlNR_Parcelas.Items.Add(i.ToString());
            }

            ddlNR_Parcelas.SelectedIndex = 1;
        }

        private void AtualizarValorTotalVenda()
        {
            double valorTotal = 0;

            foreach (GridViewRow row in gvwExibe.Rows)
            {
                valorTotal += Convert.ToDouble(row.Cells[9].Text);
            }

            txbVL_Total.Text = valorTotal.ToString("N2");
        }

        private void Incluir()
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                if (string.IsNullOrWhiteSpace(txbID_Venda.Text.Trim()))
                {
                    // tudo certinho
                    // instanciar um objeto da classe venda, carregar tela e incluir
                    myControllerVenda = new ControllerVenda(
                        Convert.ToInt32(Session["ID_Funcionario"].ToString()),
                        DateTime.Now,
                        Session["ConnectionString"].ToString());

                    txbID_Venda.Text = myControllerVenda.ID_Venda.ToString();
                    lblDS_Mensagem.Text = txbID_Venda.Text;

                    txbDT_Venda.Text = DateTime.Now.ToString();
                    CarregarTiposPagamento();
                    CarregarNParcelas();
                    btnCancelar.Enabled = true;
                }

                // o que ocorreu?
                if (!string.IsNullOrWhiteSpace(txbID_Venda.Text.Trim()))
                {
                    myControllerItemVenda = new ControllerItemVenda(
                        Convert.ToInt32(txbID_Venda.Text.Trim()),
                        Convert.ToInt32(txbID_Produto.Text.Trim()),
                        Convert.ToInt32(txbQTD_Produto.Text.Trim()),
                        Convert.ToDouble(txbPR_Produto.Text.Trim()) * Convert.ToInt32(txbQTD_Produto.Text.Trim()),
                        'I',
                        Session["ConnectionString"].ToString());

                    if (myControllerItemVenda.DS_Mensagem == "OK")
                    {
                        // tudo certinho!
                        LimparCamposCadastro();
                        BloquearComponentesCadastro();
                        CarregarItensVenda();
                        lblDS_Mensagem.Text = "Incluído com sucesso!";

                        AtualizarValorTotalVenda();
                    }
                    else
                    {// exibir erro!
                        lblDS_Mensagem.Text = "#Erro ao incluir produto na venda, tente novamente.";
                    }
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerVenda.DS_Mensagem;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void Finalizar()
        {
            try
            {
                // instanciar um objeto da classe venda, carregar tela e finalizar
                myControllerVenda = new ControllerVenda(
                    Convert.ToInt32(txbID_Venda.Text.Trim()),
                    Convert.ToInt32(Session["ID_Funcionario"].ToString()),
                    DateTime.Now,
                    ddlDS_TipoPagamento.SelectedValue,
                    Convert.ToInt32(ddlNR_Parcelas.SelectedValue),
                    Convert.ToDouble(txbVL_Total.Text.Trim()),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerVenda.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    txbID_Venda.Text = "";
                    LimparCamposCadastro();
                    BloquearComponentesCadastro();
                    LimparCamposFinaliza();
                    BloquearComponentesFinaliza();
                    LimparMensagens();
                    lblDS_MensagemFinal.Text = "Venda realizada com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_MensagemFinal.Text = myControllerVenda.DS_Mensagem;
                }
            }
            catch (Exception e)
            {
                // exibir erro!
                lblDS_MensagemFinal.Text = e.Message;
            }
        }

        private void ExcluirVenda()
        {
            try
            {
                // instanciar um objeto da classe venda, carregar tela e excluir venda
                myControllerVenda = new ControllerVenda(
                    Convert.ToInt32(txbID_Venda.Text.Trim()),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerVenda.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    txbID_Venda.Text = "";
                    LimparCamposCadastro();
                    BloquearComponentesCadastro();
                    LimparCamposFinaliza();
                    BloquearComponentesFinaliza();
                    LimparMensagens();
                    lblDS_MensagemFinal.Text = "Venda cancelada com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_MensagemFinal.Text = myControllerVenda.DS_Mensagem;
                }
            }
            catch (Exception e)
            {
                // exibir erro!
                lblDS_MensagemFinal.Text = e.Message;
            }
        }

        private void Alterar()
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe itemvenda, carregar tela e alterar
                myControllerItemVenda = new ControllerItemVenda(
                    Convert.ToInt32(txbID_Venda.Text.Trim()),
                    Convert.ToInt32(txbID_Produto.Text.Trim()),
                    Convert.ToInt32(txbQTD_Produto.Text.Trim()),
                    Convert.ToDouble(txbPR_Produto.Text.Trim()) * Convert.ToInt32(txbQTD_Produto.Text.Trim()),
                    'A',
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerItemVenda.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCamposCadastro();
                    BloquearComponentesCadastro();
                    CarregarItensVenda();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";

                    AtualizarValorTotalVenda();
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerProduto.DS_Mensagem;
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
            // instanciar um objeto da classe itemvenda e carregar tela e excluir
            myControllerItemVenda = new ControllerItemVenda(
                Convert.ToInt32(txbID_Venda.Text.Trim()),
                Convert.ToInt32(txbID_Produto.Text.Trim()),
                Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerItemVenda.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCamposCadastro();
                BloquearComponentesCadastro();
                CarregarItensVenda();
                lblDS_Mensagem.Text = "Excluído com sucesso!";

                AtualizarValorTotalVenda();
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerItemVenda.DS_Mensagem;
            }
        }

        protected void txbNR_EAN_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNR_EAN.Text.Trim()))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarEANProduto();

            if (!string.IsNullOrWhiteSpace(txbProduto.Text.Trim()))
            {
                txbQTD_Produto.Enabled = true;
            }
            else
            {
                txbQTD_Produto.Enabled = false;
            }
        }

        protected void txbProduto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbQTD_Produto.Focus();
        }

        protected void txbPR_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbQTD_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCamposCadastro();
            BloquearComponentesCadastro();
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Incluir();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            Alterar();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "ID";
                e.Row.Cells[3].Text = "EAN";
                e.Row.Cells[4].Text = "Produto";
                e.Row.Cells[5].Text = "Marca";
                e.Row.Cells[6].Text = "Sabor";
                e.Row.Cells[7].Text = "Preço";
                e.Row.Cells[8].Text = "Quantidade";
                e.Row.Cells[9].Text = "Subtotal";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDS_Mensagem.Text = "";

            txbID_Venda.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());
            txbID_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim());
            txbNR_EAN.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text.Trim());

            txbProduto.Text =
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[4].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[5].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[6].Text.Trim());

            txbPR_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[7].Text.Trim());
            txbQTD_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[8].Text.Trim());
            txbQTD_Produto.Enabled = true;

            txbNR_EAN.ReadOnly = true;
            btnConsultar.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            CarregarItensVenda();
        }

        protected void txbDT_Venda_TextChanged(object sender, EventArgs e)
        {
            IncludeFieldsFinal();
        }

        protected void ddlDS_TipoPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFieldsFinal();

            if (ddlDS_TipoPagamento.SelectedValue.Equals("Cartão de Crédito"))
            {
                ddlNR_Parcelas.Enabled = true;
                ddlNR_Parcelas.Focus();
            }

            if (ddlDS_TipoPagamento.SelectedValue.Equals("Cartão de Débito"))
            {
                ddlNR_Parcelas.SelectedIndex = 1;
                ddlNR_Parcelas.Enabled = false;
            }

            if (ddlDS_TipoPagamento.SelectedValue.Equals("Dinheiro"))
            {
                ddlNR_Parcelas.SelectedIndex = 1;
                ddlNR_Parcelas.Enabled = false;
                lblVL_Recebido.Visible = true;
                txbVL_Recebido.Visible = true;
                lblDS_MensagemTroco.Visible = true;
                btnSalvar.Enabled = false;
            }
            else
            {
                lblVL_Recebido.Visible = false;
                txbVL_Recebido.Visible = false;
                lblDS_MensagemTroco.Visible = false;
            }

        }

        protected void ddlNR_Parcelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFieldsFinal();
        }

        protected void txbVL_Total_TextChanged(object sender, EventArgs e)
        {
            IncludeFieldsFinal();
        }

        protected void txbVL_Recebido_TextChanged(object sender, EventArgs e)
        {
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbVL_Recebido.Text.Trim()))
            {
                if (!myValidar.Valor(txbVL_Recebido.Text.Trim()))
                {
                    mDs_Msg = " O valor recebido deve ser um valor numérico, no formato: 9.999.999,99.";
                }
            }
            else
            {
                mDs_Msg = " O valor recebido deve estar preenchido.";
            }

            if (mDs_Msg == "")
            {
                double troco = Convert.ToDouble(txbVL_Recebido.Text.Trim()) - Convert.ToDouble(txbVL_Total.Text.Trim());

                if (troco >= 0)
                {
                    lblDS_MensagemTroco.Text = "Troco: R$" + troco.ToString("N2");
                    btnSalvar.Enabled = true;
                }
                else
                {
                    lblDS_MensagemTroco.Text = "Falta: R$" + (troco * -1).ToString("N2");
                    btnSalvar.Enabled = false;
                }
            }
            else
            {
                btnSalvar.Enabled = false;
                lblDS_MensagemTroco.Text = mDs_Msg;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Finalizar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ExcluirVenda();
        }
    }
}