﻿using PontoDeVenda.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validacao;

namespace PontoDeVenda
{
    public partial class FrmProduto : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControlProduto myControlProduto;
        private ControlCategoria myControlCategoria;
        private ControlSubcategoria myControlSubcategoria;
        private ControlSabor myControlSabor;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarProdutos();
                CarregarCategorias();
                CarregarSubcategorias();
                CarregarSabores();
                BloquearBotoes();
            }
        }

        private void LimparCampos()
        {
            txbID_Produto.Text = "";
            txbNM_Produto.Text = "";
            txbDS_Produto.Text = "";
            ddlID_Categoria.SelectedIndex = 0;
            ddlID_Subcategoria.SelectedIndex = 0;
            ddlID_Subcategoria.Enabled = false;
            ddlID_Sabor.SelectedIndex = 0;
            txbQTD_Estoque.Text = "";
            txbPR_Venda.Text = "";
            txbPR_Custo.Text = "";
            lblDS_Mensagem.Text = "";
        }

        private void BloquearBotoes()
        {
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            btnConsultar.Enabled = false;
        }

        private void CarregarProdutos()
        {
            // instanciando um objeto da classe ControlProduto
            myControlProduto = new ControlProduto();

            // passando a fonte de dados para o GridView
            gvwProduto.DataSource = myControlProduto.Exibir();

            // associando os dados para carregar e exibir
            gvwProduto.DataBind();
        }

        private void CarregarCategorias()
        {
            myControlCategoria = new ControlCategoria();

            ddlID_Categoria.DataSource = myControlCategoria.Exibir();
            ddlID_Categoria.DataTextField = "NM_Categoria";
            ddlID_Categoria.DataValueField = "ID_Categoria";
            ddlID_Categoria.DataBind();

            ddlID_Categoria.Items.Insert(0, "Categoria");
            ddlID_Categoria.SelectedIndex = 0;
        }

        private void CarregarSubcategorias()
        {
            myControlSubcategoria = new ControlSubcategoria();

            ddlID_Subcategoria.DataSource = myControlSubcategoria.Exibir();
            ddlID_Subcategoria.DataTextField = "NM_Subcategoria";
            ddlID_Subcategoria.DataValueField = "ID_Subcategoria";
            ddlID_Subcategoria.DataBind();

            ddlID_Subcategoria.Items.Insert(0, "Subcategoria");
            ddlID_Subcategoria.SelectedIndex = 0;
        }

        private void CarregarSabores()
        {
            myControlSabor = new ControlSabor();

            ddlID_Sabor.DataSource = myControlSabor.Exibir();
            ddlID_Sabor.DataTextField = "DS_Sabor";
            ddlID_Sabor.DataValueField = "ID_Sabor";
            ddlID_Sabor.DataBind();

            ddlID_Sabor.Items.Insert(0, "Sabor");
            ddlID_Sabor.SelectedIndex = 0;
        }

        private void IncludeFields()
        {
            btnIncluir.Enabled =
                txbNM_Produto.Text.Length > 0 &&
                ddlID_Sabor.SelectedIndex != 0 &&
                txbDS_Produto.Text.Length > 0 &&
                txbQTD_Estoque.Text.Length > 0 &&
                txbPR_Venda.Text.Length > 0 &&
                txbPR_Custo.Text.Length > 0 &&
                ddlID_Categoria.SelectedIndex != 0 &&
                ddlID_Subcategoria.SelectedIndex != 0 &&
                txbID_Produto.Text.Length == 0;

            btnLimpar.Enabled =
                txbNM_Produto.Text.Length > 0 ||
                ddlID_Sabor.SelectedIndex != 0 ||
                txbDS_Produto.Text.Length > 0 ||
                txbQTD_Estoque.Text.Length > 0 ||
                txbPR_Venda.Text.Length > 0 ||
                txbPR_Custo.Text.Length > 0 ||
                ddlID_Categoria.SelectedIndex != 0 ||
                ddlID_Subcategoria.SelectedIndex != 0;
        }

        private void Incluir()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar(
                txbNM_Produto.Text,
                txbDS_Produto.Text,
                txbQTD_Estoque.Text,
                txbPR_Venda.Text,
                txbPR_Custo.Text);

            // o que ocorreu?
            if (myValidar.DS_Mensagem == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e incluir
                myControlProduto = new ControlProduto(
                    Convert.ToInt32(ddlID_Categoria.SelectedValue),
                    Convert.ToInt32(ddlID_Subcategoria.SelectedValue),
                    Convert.ToInt32(ddlID_Sabor.SelectedValue),
                    txbNM_Produto.Text,
                    txbDS_Produto.Text,
                    Convert.ToDouble(txbQTD_Estoque.Text),
                    Convert.ToDouble(txbPR_Venda.Text),
                    Convert.ToDouble(txbPR_Custo.Text));

                // o que ocorreu?
                if (myControlProduto.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarProdutos();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControlProduto.DS_Mensagem;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myValidar.DS_Mensagem;
            }
        }

        private void Alterar()
        {
            // validar a entrada de dados para alterar
            myValidar = new Validar(
                txbNM_Produto.Text,
                txbDS_Produto.Text,
                txbQTD_Estoque.Text,
                txbPR_Venda.Text,
                txbPR_Custo.Text);

            // o que ocorreu?
            if (myValidar.DS_Mensagem == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e alterar
                myControlProduto = new ControlProduto(
                    Convert.ToInt32(txbID_Produto.Text),
                    Convert.ToInt32(ddlID_Categoria.SelectedValue),
                    Convert.ToInt32(ddlID_Subcategoria.SelectedValue),
                    Convert.ToInt32(ddlID_Sabor.SelectedValue),
                    txbNM_Produto.Text,
                    txbDS_Produto.Text,
                    Convert.ToDouble(txbQTD_Estoque.Text),
                    Convert.ToDouble(txbPR_Venda.Text),
                    Convert.ToDouble(txbPR_Custo.Text));

                // o que ocorreu?
                if (myControlProduto.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarProdutos();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControlProduto.DS_Mensagem;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myValidar.DS_Mensagem;
            }
        }

        private void CarregarProdutosConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar(txbNM_ProdutoConsultar.Text);

            // o que ocorreu?
            if (myValidar.DS_Mensagem == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e consultar
                myControlProduto = new ControlProduto(txbNM_ProdutoConsultar.Text);
                gvwProduto.DataSource = myControlProduto.Consultar();
                gvwProduto.DataBind();
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myValidar.DS_Mensagem;
            }
        }

        private void Excluir()
        {
            // instanciar um objeto da classe produto e carregar tela e consultar
            myControlProduto = new ControlProduto(Convert.ToInt32(txbID_Produto.Text));

            // o que ocorreu?
            if (myControlProduto.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearBotoes();
                CarregarProdutos();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControlProduto.DS_Mensagem;
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
            BloquearBotoes();
        }

        protected void gvwProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[9].Attributes.Add("style", "word-break:break-all;word-wrap:break-word; width: 200px");
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[7].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Text = "Categoria";
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Text = "Subcategoria";
                e.Row.Cells[6].Text = "Nome";
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Text = "Sabor";
                e.Row.Cells[9].Text = "Descrição";
                e.Row.Cells[10].Text = "Estoque";
                e.Row.Cells[11].Text = "Preço\nCusto";
                e.Row.Cells[12].Text = "Preço\nVenda";
            }
        }

        protected void gvwProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbID_Produto.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[1].Text);
            ddlID_Categoria.SelectedValue = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[2].Text);
            ddlID_Subcategoria.SelectedValue = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[4].Text);
            ddlID_Subcategoria.Enabled = true;
            txbNM_Produto.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[6].Text);
            ddlID_Sabor.SelectedValue = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[7].Text);
            txbDS_Produto.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[9].Text);
            txbQTD_Estoque.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[10].Text);
            txbPR_Custo.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[11].Text);
            txbPR_Venda.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[12].Text);

            lblDS_Mensagem.Text = "";

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void txbNM_ProdutoConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_ProdutoConsultar.Text))
            {
                btnConsultar.Enabled = true;
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarProdutos();
            }
        }

        protected void txbNM_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbDS_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbQTD_Estoque_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbPR_Venda_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbPR_Custo_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void ddlID_Categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();

            ddlID_Subcategoria.Enabled = ddlID_Categoria.SelectedIndex != 0;
        }

        protected void ddlID_Subcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void ddlID_Sabor_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }
    }
}