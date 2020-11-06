﻿using MySqlX.XDevAPI.Relational;
using SuplementosPIMIV.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmProduto : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerProduto myControllerProduto;
        private ControllerMarca myControllerMarca;
        private ControllerCategoria myControllerCategoria;
        private ControllerSubcategoria myControllerSubcategoria;
        private ControllerSabor myControllerSabor;
        private ControllerEstoque myControllerEstoque;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarProdutos();
                CarregarMarcas();
                CarregarCategorias();
                CarregarSubcategorias();
                CarregarSabores();
                CarregarFiltrosDeBusca();
                BloquearComponentes();
            }
        }

        private void LimparCampos()
        {
            txbID_Produto.Text = "";
            txbNR_EAN.Text = "";
            txbNM_Produto.Text = "";
            ddlID_MarcaProduto.SelectedIndex = 0;
            txbDS_Produto.Text = "";
            ddlID_CategoriaProduto.SelectedIndex = 0;
            ddlID_SubcategoriaProduto.SelectedIndex = 0;
            ddlID_SubcategoriaProduto.Enabled = false;
            ddlID_SaborProduto.SelectedIndex = 0;
            txbQTD_Estoque.Text = "";
            txbPR_Venda.Text = "";
            txbPR_Custo.Text = "";
            lblDS_Mensagem.Text = "";
        }

        private void BloquearComponentes()
        {
            btnAtivarStatus.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            btnConsultar.Enabled = false;
            txbConsultar.Enabled = false;
            txbQTD_Estoque.Enabled = false;
        }

        private void CarregarProdutos()
        {
            // instanciando um objeto da classe ControllerProduto
            myControllerProduto = new ControllerProduto(Session["ConnectionString"].ToString());

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerProduto.Exibir(chkStatusInativo.Checked ? 0 : 1);

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarMarcas()
        {
            myControllerMarca = new ControllerMarca(Session["ConnectionString"].ToString());

            ddlID_MarcaProduto.DataSource = myControllerMarca.Exibir(1);
            ddlID_MarcaProduto.DataTextField = "NM_Marca";
            ddlID_MarcaProduto.DataValueField = "ID_Marca";
            ddlID_MarcaProduto.DataBind();

            ddlID_MarcaProduto.Items.Insert(0, "Marca");
            ddlID_MarcaProduto.SelectedIndex = 0;
        }

        private void CarregarCategorias()
        {
            myControllerCategoria = new ControllerCategoria(Session["ConnectionString"].ToString());

            ddlID_CategoriaProduto.DataSource = myControllerCategoria.Exibir(1);
            ddlID_CategoriaProduto.DataTextField = "NM_Categoria";
            ddlID_CategoriaProduto.DataValueField = "ID_Categoria";
            ddlID_CategoriaProduto.DataBind();

            ddlID_CategoriaProduto.Items.Insert(0, "Categoria");
            ddlID_CategoriaProduto.SelectedIndex = 0;
        }

        private void CarregarSubcategorias()
        {
            myControllerSubcategoria = new ControllerSubcategoria(Session["ConnectionString"].ToString());

            ddlID_SubcategoriaProduto.DataSource = myControllerSubcategoria.Exibir(1);
            ddlID_SubcategoriaProduto.DataTextField = "NM_Subcategoria";
            ddlID_SubcategoriaProduto.DataValueField = "ID_Subcategoria";
            ddlID_SubcategoriaProduto.DataBind();

            ddlID_SubcategoriaProduto.Items.Insert(0, "Subcategoria");
            ddlID_SubcategoriaProduto.SelectedIndex = 0;
        }

        private void CarregarSabores()
        {
            myControllerSabor = new ControllerSabor(Session["ConnectionString"].ToString());

            ddlID_SaborProduto.DataSource = myControllerSabor.Exibir(1);
            ddlID_SaborProduto.DataTextField = "NM_Sabor";
            ddlID_SaborProduto.DataValueField = "ID_Sabor";
            ddlID_SaborProduto.DataBind();

            ddlID_SaborProduto.Items.Insert(0, "Sabor");
            ddlID_SaborProduto.SelectedIndex = 0;
        }

        private void CarregarProdutosConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbConsultar.Text, 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e consultar
                myControllerProduto = new ControllerProduto(Session["ConnectionString"].ToString());

                string filtro = "";

                if (ddlFiltro.SelectedValue.Equals("ID")) filtro = "PROD.ID_Produto";
                if (ddlFiltro.SelectedValue.Equals("EAN")) filtro = "PROD.NR_EAN";
                if (ddlFiltro.SelectedValue.Equals("Nome")) filtro = "PROD.NM_Produto";
                if (ddlFiltro.SelectedValue.Equals("Marca")) filtro = "MAR.NM_Marca";
                if (ddlFiltro.SelectedValue.Equals("Categoria")) filtro = "CAT.NM_Categoria";
                if (ddlFiltro.SelectedValue.Equals("Subcategoria")) filtro = "SUB.NM_Subcategoria";

                if (ddlFiltro.SelectedValue.Equals("Preço Venda"))
                {
                    filtro = "PROD.PR_Venda";
                    txbConsultar.Text = txbConsultar.Text.Replace(",", ".");
                }

                gvwExibe.DataSource = myControllerProduto.Consultar(chkStatusInativo.Checked ? 0 : 1, filtro, txbConsultar.Text);
                gvwExibe.DataBind();
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void CarregarFiltrosDeBusca()
        {
            ddlFiltro.Items.Insert(0, "Filtro");
            ddlFiltro.Items.Insert(1, "ID");
            ddlFiltro.Items.Insert(2, "EAN");
            ddlFiltro.Items.Insert(3, "Nome");
            ddlFiltro.Items.Insert(4, "Marca");
            ddlFiltro.Items.Insert(5, "Categoria");
            ddlFiltro.Items.Insert(6, "Subcategoria");
            ddlFiltro.Items.Insert(7, "Preço Venda");

            ddlFiltro.SelectedIndex = 0;
        }

        private void IncludeFields()
        {
            btnIncluir.Enabled =
                txbNR_EAN.Text.Length > 0 &&
                txbNM_Produto.Text.Length > 0 &&
                txbDS_Produto.Text.Length > 0 &&
                txbPR_Venda.Text.Length > 0 &&
                txbPR_Custo.Text.Length > 0 &&
                ddlID_MarcaProduto.SelectedIndex != 0 &&
                ddlID_CategoriaProduto.SelectedIndex != 0 &&
                ddlID_SubcategoriaProduto.SelectedIndex != 0 &&
                ddlID_SaborProduto.SelectedIndex != 0 &&
                txbID_Produto.Text.Length == 0;

            btnLimpar.Enabled =
                txbNR_EAN.Text.Length > 0 ||
                txbNM_Produto.Text.Length > 0 ||
                txbDS_Produto.Text.Length > 0 ||
                txbQTD_Estoque.Text.Length > 0 ||
                txbPR_Venda.Text.Length > 0 ||
                txbPR_Custo.Text.Length > 0 ||
                ddlID_MarcaProduto.SelectedIndex != 0 ||
                ddlID_CategoriaProduto.SelectedIndex != 0 ||
                ddlID_SubcategoriaProduto.SelectedIndex != 0 ||
                ddlID_SaborProduto.SelectedIndex != 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            myControllerProduto = new ControllerProduto(Session["ConnectionString"].ToString());
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbNR_EAN.Text))
            {
                if (!myValidar.TamanhoCampo(txbNR_EAN.Text, 13))
                {
                    mDs_Msg = " Limite de caracteres para o EAN excedido, " +
                                  "o limite para este campo é: 13 caracteres, " +
                                  "quantidade utilizada: " + txbNR_EAN.Text.Length + ".";
                }
                else
                {
                    if (!myValidar.Numero(txbNR_EAN.Text))
                    {
                        mDs_Msg = " O EAN deve ser numérico.";
                    }
                    else
                    {
                        if (!myValidar.EAN(txbNR_EAN.Text))
                        {
                            mDs_Msg = " EAN inválido.";
                        }
                        else
                        {
                            if (myValidar.CampoPreenchido(txbNM_Produto.Text))
                            {
                                if (!myValidar.TamanhoCampo(txbNM_Produto.Text, 50))
                                {
                                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                                  "o limite para este campo é: 50 caracteres, " +
                                                  "quantidade utilizada: " + txbNM_Produto.Text.Length + ".";
                                }
                                else
                                {
                                    if (myControllerProduto.VerificarProdutoCadastrado(txbID_Produto.Text, txbNR_EAN.Text, txbNM_Produto.Text, ddlID_MarcaProduto.SelectedValue).Equals(""))
                                    {
                                        if (myValidar.CampoPreenchido(txbDS_Produto.Text))
                                        {
                                            if (!myValidar.TamanhoCampo(txbDS_Produto.Text, 3000))
                                            {
                                                mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                              "o limite para este campo é: 3000 caracteres, " +
                                                              "quantidade utilizada: " + txbDS_Produto.Text.Length + ".";
                                            }
                                        }
                                        else
                                        {
                                            mDs_Msg += " A descrição deve estar preenchida.";
                                        }

                                        if (myValidar.CampoPreenchido(txbPR_Custo.Text))
                                        {
                                            if (!myValidar.Valor(txbPR_Custo.Text))
                                            {
                                                mDs_Msg += " O preço de custo deve ser um valor numérico, no formato: 9.999.999,99.";
                                            }
                                        }
                                        else
                                        {
                                            mDs_Msg += " O preço de custo deve estar preenchido.";
                                        }

                                        if (myValidar.CampoPreenchido(txbPR_Venda.Text))
                                        {
                                            if (!myValidar.Valor(txbPR_Venda.Text))
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
                                        mDs_Msg += " " + myControllerProduto.DS_Mensagem + " Verifique nos produtos ativos e inativos!";
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

        private void Incluir()
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e incluir
                myControllerProduto = new ControllerProduto(
                    Convert.ToInt32(ddlID_MarcaProduto.SelectedValue),
                    Convert.ToInt32(ddlID_CategoriaProduto.SelectedValue),
                    Convert.ToInt32(ddlID_SubcategoriaProduto.SelectedValue),
                    Convert.ToInt32(ddlID_SaborProduto.SelectedValue),
                    txbNR_EAN.Text,
                    txbNM_Produto.Text,
                    txbDS_Produto.Text,
                    Convert.ToDouble(txbPR_Custo.Text),
                    Convert.ToDouble(txbPR_Venda.Text),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerProduto.DS_Mensagem == "OK")
                {
                    myControllerEstoque = new ControllerEstoque(
                        myControllerProduto.ID_Produto,
                        0,
                        Session["ConnectionString"].ToString());

                    if (myControllerEstoque.DS_Mensagem == "OK")
                    {
                        // tudo certinho!
                        LimparCampos();
                        BloquearComponentes();
                        CarregarProdutos();
                        lblDS_Mensagem.Text = "Incluído com sucesso!";
                    }
                    else
                    {// exibir erro!
                        lblDS_Mensagem.Text = "Incluído com sucesso! #Erro ao cadastrar estoque, " +
                            "favor cadastrar manualmente para conseguir visualizar o produto.";

                    }
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

        private void Alterar()
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e alterar
                myControllerProduto = new ControllerProduto(
                    Convert.ToInt32(txbID_Produto.Text),
                    Convert.ToInt32(ddlID_MarcaProduto.SelectedValue),
                    Convert.ToInt32(ddlID_CategoriaProduto.SelectedValue),
                    Convert.ToInt32(ddlID_SubcategoriaProduto.SelectedValue),
                    Convert.ToInt32(ddlID_SaborProduto.SelectedValue),
                    txbNR_EAN.Text,
                    txbNM_Produto.Text,
                    txbDS_Produto.Text,
                    Convert.ToDouble(txbPR_Custo.Text),
                    Convert.ToDouble(txbPR_Venda.Text),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerProduto.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearComponentes();
                    CarregarProdutos();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
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
            // instanciar um objeto da classe produto e carregar tela e excluir
            myControllerProduto = new ControllerProduto(Convert.ToInt32(txbID_Produto.Text), 'E', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerProduto.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentes();
                CarregarProdutos();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerProduto.DS_Mensagem;
            }
        }

        private void Ativar()
        {
            // instanciar um objeto da classe produto e carregar tela e ativar
            myControllerProduto = new ControllerProduto(Convert.ToInt32(txbID_Produto.Text), 'A', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerProduto.DS_Mensagem == "OK")
            {
                // tudo certinho!
                CarregarProdutos();
                lblDS_Mensagem.Text = "Ativado com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerProduto.DS_Mensagem;
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Incluir();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            Alterar();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregarProdutosConsultar();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            BloquearComponentes();
        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[12].Attributes.Add("style", "word-break:break-all;word-wrap:break-word; width: 400px");
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[10].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "EAN";
                e.Row.Cells[3].Text = "Nome";
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Text = "Marca";
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Text = "Categoria";
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Text = "Subcategoria";
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Text = "Sabor";
                e.Row.Cells[12].Text = "Descrição";
                e.Row.Cells[13].Text = "Estoque";
                e.Row.Cells[14].Text = "Preço\nCusto";
                e.Row.Cells[15].Text = "Preço\nVenda";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbID_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text);
            txbNR_EAN.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text);
            txbNM_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text);
            ddlID_MarcaProduto.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[4].Text);
            ddlID_CategoriaProduto.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[6].Text);
            ddlID_SubcategoriaProduto.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[8].Text);
            ddlID_SubcategoriaProduto.Enabled = true;
            ddlID_SaborProduto.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[10].Text);
            txbDS_Produto.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[12].Text);
            txbQTD_Estoque.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[13].Text);
            txbPR_Custo.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[14].Text);
            txbPR_Venda.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[15].Text);

            CheckBox ativo = (CheckBox)gvwExibe.SelectedRow.Cells[16].Controls[0];
            if (!ativo.Checked)
            {
                btnAtivarStatus.Enabled = true;
                btnExcluir.Enabled = false;
            }
            else
            {
                btnAtivarStatus.Enabled = false;
                btnExcluir.Enabled = true;
            }

            lblDS_Mensagem.Text = "";

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlFiltro.SelectedIndex.Equals(0))
            {
                txbConsultar.Enabled = true;
                txbConsultar.Focus();
            }
            else
            {
                txbConsultar.Text = "";
                txbConsultar.Enabled = false;
            }
        }

        protected void txbConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbConsultar.Text))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarProdutos();
            }
        }

        protected void txbNR_EAN_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbNM_Produto.Focus();
        }

        protected void txbNM_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlID_MarcaProduto.Focus();
        }

        protected void ddlID_MarcaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Produto.Focus();
        }

        protected void txbDS_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlID_CategoriaProduto.Focus();
        }

        protected void ddlID_CategoriaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();

            ddlID_SubcategoriaProduto.Enabled = ddlID_CategoriaProduto.SelectedIndex != 0;
            ddlID_SubcategoriaProduto.Focus();
        }

        protected void ddlID_SubcategoriaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlID_SaborProduto.Focus();
        }

        protected void ddlID_SaborProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbPR_Custo.Focus();
        }

        protected void txbPR_Custo_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbPR_Venda.Focus();
        }

        protected void txbPR_Venda_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void chkStatusInativo_CheckedChanged(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        protected void btnAtivarStatus_Click(object sender, EventArgs e)
        {
            Ativar();
            btnAtivarStatus.Enabled = false;
        }
    }
}