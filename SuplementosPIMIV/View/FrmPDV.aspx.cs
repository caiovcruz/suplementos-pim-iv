using SuplementosPIMIV.Controller;
using System;
using System.Data;
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
        private ControllerMovEstoque myControllerMovEstoque;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
            {
                if (!IsPostBack)
                {
                    LimparCamposCadastro();
                    BloquearComponentesCadastro();
                    LimparCamposSalvar();
                    BloquearComponentesSalvar();
                    LimparMensagens();
                    Session["dtItemVenda"] = null;

                    lblNM_FuncionarioLogin.Text = Session["NM_FuncionarioLogin"].ToString();

                    if (!Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                    {
                        lbtnRelatorios.Visible = false;
                        lbtnDropFuncionarios.Visible = false;
                        lbtnDropProdutos.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("FrmLogin.aspx");
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
            lblDS_Mensagem.Text = "";
            AlterarCorQTD_Produto(System.Drawing.Color.Black);
        }

        private void LimparCamposSalvar()
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
            txbNR_EAN.ReadOnly = false;
            txbProduto.ReadOnly = true;
            txbPR_Produto.ReadOnly = true;
            txbQTD_Produto.Enabled = false;

            btnConsultar.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
        }

        private void BloquearComponentesSalvar()
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

        private void AlterarCorQTD_Produto(System.Drawing.Color color)
        {
            lblQTD_Produto.ForeColor = color;
            txbQTD_Produto.ForeColor = color;
            txbQTD_Produto.BorderColor = color;
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
                myControllerProduto = new ControllerProduto();
                myControllerProduto.Consultar("1", "PROD.NR_EAN = '" + txbNR_EAN.Text.Trim() + "' ", Session["ConnectionString"].ToString());

                if (!myControllerProduto.ID_Produto.Equals(0))
                {
                    txbID_Produto.Text = myControllerProduto.ID_Produto.ToString();
                    txbProduto.Text = myControllerProduto.NM_Produto;
                    txbPR_Produto.Text = myControllerProduto.PR_Venda.ToString("N2");

                    txbVL_LucroProduto.Text = (myControllerProduto.PR_Venda - myControllerProduto.PR_Custo).ToString("N2");

                    myControllerEstoque = new ControllerEstoque();

                    if (myControllerEstoque.QuantidadeTotalEstoque(txbID_Produto.Text.Trim(), Session["ConnectionString"].ToString()) <= 0)
                    {
                        txbQTD_Produto.Text = "ESGOTADO";
                        AlterarCorQTD_Produto(System.Drawing.Color.Red);
                        txbQTD_Produto.ReadOnly = true;
                    }
                    else
                    {
                        txbQTD_Produto.Text = "";
                        AlterarCorQTD_Produto(System.Drawing.Color.Black);
                        txbQTD_Produto.ReadOnly = false;
                    }

                    btnLimpar.Enabled = true;
                }
                else
                {
                    AlterarCorQTD_Produto(System.Drawing.Color.Black);
                    txbQTD_Produto.Text = "";
                    lblDS_Mensagem.Text = "Produto inexistente ou inativo. ☞ Verifique o EAN ou Consulte o gerente! ☜";
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
                txbVL_Total.Text.Trim().Length > 0 &&
                gvwExibe.Rows.Count > 0;

            btnCancelar.Enabled =
                txbDT_Venda.Text.Trim().Length > 0 ||
                ddlDS_TipoPagamento.SelectedIndex != 0 ||
                ddlNR_Parcelas.SelectedIndex != 0 ||
                txbVL_Total.Text.Trim().Length > 0 ||
                gvwExibe.Rows.Count > 0;
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
                valorTotal += Convert.ToDouble(row.Cells[6].Text);
            }

            txbVL_Total.Text = valorTotal.ToString("N2");
        }

        private double GetValorLucroTotal()
        {
            double valorLucro = 0;

            foreach (GridViewRow row in gvwExibe.Rows)
            {
                valorLucro += Convert.ToDouble(row.Cells[7].Text);
            }

            return valorLucro;
        }

        private void Incluir()
        {
            try
            {
                DataTable dtItemVenda = new DataTable();

                if (Session["dtItemVenda"] != null)
                {
                    dtItemVenda = (DataTable)Session["dtItemVenda"];
                }
                else
                {
                    dtItemVenda.Columns.Add("#");
                    dtItemVenda.Columns.Add("EAN");
                    dtItemVenda.Columns.Add("Produto");
                    dtItemVenda.Columns.Add("Quantidade");
                    dtItemVenda.Columns.Add("Preço");
                    dtItemVenda.Columns.Add("Subtotal");
                    dtItemVenda.Columns.Add("Lucro");
                }

                DataRow dataRow = dtItemVenda.NewRow();
                dataRow["#"] = txbID_Produto.Text.Trim();
                dataRow["EAN"] = txbNR_EAN.Text.Trim();
                dataRow["Produto"] = txbProduto.Text.Trim();
                dataRow["Quantidade"] = txbQTD_Produto.Text.Trim();
                dataRow["Preço"] = txbPR_Produto.Text.Trim();
                dataRow["Subtotal"] = (Convert.ToDouble(txbPR_Produto.Text.Trim()) * Convert.ToInt32(txbQTD_Produto.Text.Trim())).ToString("N2");
                dataRow["Lucro"] = (Convert.ToDouble(txbVL_LucroProduto.Text.Trim()) * Convert.ToInt32(txbQTD_Produto.Text.Trim())).ToString("N2");

                dtItemVenda.Rows.Add(dataRow);
                gvwExibe.DataSource = dtItemVenda;
                gvwExibe.DataBind();
                Session["dtItemVenda"] = dtItemVenda;

                LimparCamposCadastro();
                BloquearComponentesCadastro();
                lblDS_Mensagem.Text = "Incluído com sucesso!";

                AtualizarValorTotalVenda();

                if (Session["dtItemVenda"] != null)
                {
                    txbDT_Venda.Text = DateTime.Now.ToString();
                    if (ddlDS_TipoPagamento.Items.Count.Equals(0)) CarregarTiposPagamento();
                    if (ddlNR_Parcelas.Items.Count.Equals(0)) CarregarNParcelas();
                    btnCancelar.Enabled = true;
                }
            }
            catch (Exception e)
            {
                lblDS_Mensagem.Text = "Ocorreu um erro ao incluir o item na venda. Por favor, tente novamente! |#|ERRO|#| " + e.Message;
            }
        }

        private void Alterar()
        {
            try
            {
                DataTable dtItemVenda = new DataTable();

                if (Session["dtItemVenda"] != null)
                {
                    dtItemVenda = (DataTable)Session["dtItemVenda"];
                }

                foreach (DataRow row in dtItemVenda.Rows)
                {
                    if (row[0].ToString().Equals(txbID_Produto.Text.Trim()))
                    {
                        row[3] = txbQTD_Produto.Text.Trim();
                        row[4] = txbPR_Produto.Text.Trim();
                        row[5] = (Convert.ToDouble(txbPR_Produto.Text.Trim()) * Convert.ToInt32(txbQTD_Produto.Text.Trim())).ToString("N2");
                        row[6] = (Convert.ToDouble(txbVL_LucroProduto.Text.Trim()) * Convert.ToInt32(txbQTD_Produto.Text.Trim())).ToString("N2");
                        dtItemVenda.AcceptChanges();
                        break;
                    }
                }

                gvwExibe.DataSource = dtItemVenda;
                gvwExibe.DataBind();
                Session["dtItemVenda"] = dtItemVenda;

                LimparCamposCadastro();
                BloquearComponentesCadastro();
                lblDS_Mensagem.Text = "Alterado com sucesso!";

                AtualizarValorTotalVenda();
            }
            catch (Exception e)
            {
                lblDS_Mensagem.Text = "Ocorreu um erro ao alterar o item da venda. Por favor, tente novamente! #ERRO# " + e.Message;
            }
        }

        private void Excluir()
        {
            try
            {
                DataTable dtItemVenda = new DataTable();

                if (Session["dtItemVenda"] != null)
                {
                    dtItemVenda = (DataTable)Session["dtItemVenda"];

                    foreach (DataRow row in dtItemVenda.Rows)
                    {
                        if (row[0].ToString().Equals(txbID_Produto.Text.Trim()))
                        {
                            row.Delete();
                            dtItemVenda.AcceptChanges();
                            break;
                        }
                    }

                    gvwExibe.DataSource = dtItemVenda;
                    gvwExibe.DataBind();
                    Session["dtItemVenda"] = dtItemVenda;

                    LimparCamposCadastro();
                    BloquearComponentesCadastro();
                    lblDS_Mensagem.Text = "Excluído com sucesso!";

                    AtualizarValorTotalVenda();
                }
            }
            catch (Exception e)
            {
                lblDS_Mensagem.Text = "Ocorreu um erro ao excluir o item da venda. Por favor, tente novamente! #ERRO# " + e.Message;
            }
        }

        private void SalvarItensVenda(string id_venda)
        {

        }

        private void BaixaEstoqueItensVenda()
        {
            string nm_produtoErroEstoque = "";

            foreach (GridViewRow row in gvwExibe.Rows)
            {
                myControllerMovEstoque = new ControllerMovEstoque(
                    row.Cells[1].Text,
                    row.Cells[4].Text,
                    "Venda",
                    DateTime.Now,
                    Session["ConnectionString"].ToString());

                if (myControllerMovEstoque.DS_Mensagem != "OK")
                {
                    nm_produtoErroEstoque += "Produto ➯ | " + row.Cells[3].Text + " |. ";
                }
            }

            if (nm_produtoErroEstoque != "")
            {
                lblDS_MensagemFinal.Text +=
                    " Ocorreu um erro ao atualizar o estoque dos seguintes produtos: " +
                    nm_produtoErroEstoque +
                    " ☞ Informe o gerente! ☜";
            }
        }

        private void Salvar()
        {
            if (gvwExibe.Rows.Count > 0)
            {
                try
                {
                    // tudo certinho
                    // instanciar um objeto da classe venda, carregar tela e incluir
                    myControllerVenda = new ControllerVenda(
                        Session["ID_Funcionario"].ToString(),
                        DateTime.Now,
                        ddlDS_TipoPagamento.SelectedValue,
                        ddlNR_Parcelas.SelectedValue,
                        txbVL_Total.Text.Trim(),
                        GetValorLucroTotal().ToString(),
                        Session["ConnectionString"].ToString());

                    // o que ocorreu?
                    if (!myControllerVenda.ID_Venda.Equals(0))
                    {
                        // tudo certinho
                        lblDS_MensagemFinal.Text = "";

                        string nm_produtoErro = "";

                        foreach (GridViewRow row in gvwExibe.Rows)
                        {
                            myControllerItemVenda = new ControllerItemVenda(
                                myControllerVenda.ID_Venda.ToString(),
                                row.Cells[1].Text,
                                row.Cells[4].Text,
                                row.Cells[5].Text,
                                (Convert.ToDouble(row.Cells[7].Text) / Convert.ToInt32(row.Cells[4].Text)).ToString("N2"),
                                'I',
                                Session["ConnectionString"].ToString());

                            if (myControllerItemVenda.DS_Mensagem != "OK")
                            {
                                nm_produtoErro += "Produto ➯ | " + row.Cells[3].Text + " |. ";
                            }
                        }

                        if (nm_produtoErro != "")
                        {
                            lblDS_MensagemFinal.Text +=
                                " Ocorreu um erro ao salvar os seguintes itens da venda: " +
                                nm_produtoErro +
                                " ☞ Informe o gerente! ☜";
                        }

                        BaixaEstoqueItensVenda();

                        LimparCamposCadastro();
                        BloquearComponentesCadastro();
                        LimparCamposSalvar();
                        BloquearComponentesSalvar();
                        lblDS_Mensagem.Text = "";
                        lblDS_MensagemTroco.Text = "";
                        Session["dtItemVenda"] = null;

                        lblDS_MensagemFinal.Text = lblDS_MensagemFinal.Text.Equals("") ? "Venda realizada com sucesso!" : "Venda realizada com sucesso! |#|ERRO(s)|#| " + lblDS_MensagemFinal.Text;
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
                    lblDS_MensagemFinal.Text = "Ocorreu um erro ao salvar a venda. Por favor, tente novamente! |#|ERRO|#| " + e.Message;
                }
            }
            else
            {
                lblDS_MensagemFinal.Text = "Impossível salvar uma venda com zero itens! Por favor, inclua itens na venda.";
                btnSalvar.Enabled = false;
            }
        }

        private void Cancelar()
        {
            LimparCamposCadastro();
            BloquearComponentesCadastro();
            LimparCamposSalvar();
            BloquearComponentesSalvar();
            LimparMensagens();
            Session["dtItemVenda"] = null;

            lblDS_MensagemFinal.Text = "Venda cancelada com sucesso!";
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

                e.Row.Cells[7].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[7].Visible = false;
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDS_Mensagem.Text = "";

            txbID_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());
            txbNR_EAN.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim());
            txbProduto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text.Trim());
            txbQTD_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[4].Text.Trim());
            txbPR_Produto.Text = Convert.ToDouble(gvwExibe.SelectedRow.Cells[5].Text.Trim()).ToString("N2");

            txbVL_LucroProduto.Text = (Convert.ToDouble(gvwExibe.SelectedRow.Cells[7].Text.Trim()) /
                Convert.ToDouble(gvwExibe.SelectedRow.Cells[4].Text.Trim())).ToString("N2");

            txbNR_EAN.ReadOnly = true;
            txbQTD_Produto.Enabled = true;
            btnConsultar.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            gvwExibe.DataSource = (DataTable)Session["dtItemVenda"];
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
            Salvar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
    }
}