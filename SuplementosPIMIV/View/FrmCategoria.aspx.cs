﻿using SuplementosPIMIV.Controller;
using System;
using System.Web.UI.WebControls;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmCategoria : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerCategoria myControllerCategoria;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
            {
                if (Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                {
                    if (!IsPostBack)
                    {
                        LimparCampos();
                        CarregarCategorias();
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
            txbID_Categoria.Text = "";
            txbNM_Categoria.Text = "";
            txbDS_Categoria.Text = "";
            lblDS_Mensagem.Text = "";
        }

        private void BloquearComponentesCadastro()
        {
            btnAtivarStatus.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
        }

        private void BloquearComponentesExibe()
        {
            btnConsultar.Enabled = false;
        }

        private void CarregarCategorias()
        {
            // instanciando um objeto da classe ControllerCategoria
            myControllerCategoria = new ControllerCategoria();

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerCategoria.Exibir(chkStatusInativo.Checked ? "0" : "1", Session["ConnectionString"].ToString());

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarCategoriasConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_CategoriaConsultar.Text.Trim(), 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_CategoriaConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe categoria, carregar tela e consultar
                myControllerCategoria = new ControllerCategoria();
                gvwExibe.DataSource = myControllerCategoria.Consultar(chkStatusInativo.Checked ? "0" : "1", txbNM_CategoriaConsultar.Text.Trim(), Session["ConnectionString"].ToString());
                gvwExibe.DataBind();
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
                txbNM_Categoria.Text.Trim().Length > 0 &&
                txbDS_Categoria.Text.Trim().Length > 0 &&
                txbID_Categoria.Text.Trim().Length == 0;

            btnLimpar.Enabled =
                txbNM_Categoria.Text.Trim().Length > 0 ||
                txbDS_Categoria.Text.Trim().Length > 0;
        }



        private void Incluir()
        {
            myControllerCategoria = new ControllerCategoria(
                txbNM_Categoria.Text.Trim(),
                txbDS_Categoria.Text.Trim(),
                Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerCategoria.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarCategorias();
                lblDS_Mensagem.Text = "Incluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerCategoria.DS_Mensagem;
            }
        }

        private void Alterar()
        {
            myControllerCategoria = new ControllerCategoria(
                txbID_Categoria.Text.Trim(),
                txbNM_Categoria.Text.Trim(),
                txbDS_Categoria.Text.Trim(),
                Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerCategoria.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarCategorias();
                lblDS_Mensagem.Text = "Alterado com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerCategoria.DS_Mensagem;
            }
        }

        private void Excluir()
        {
            // instanciar um objeto da classe categoria e carregar tela e excluir
            myControllerCategoria = new ControllerCategoria(txbID_Categoria.Text.Trim(), 'E', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerCategoria.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarCategorias();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerCategoria.DS_Mensagem;
            }
        }

        private void Ativar()
        {
            // instanciar um objeto da classe categoria e carregar tela e ativar
            myControllerCategoria = new ControllerCategoria(txbID_Categoria.Text.Trim(), 'A', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerCategoria.DS_Mensagem == "OK")
            {
                // tudo certinho!
                CarregarCategorias();
                lblDS_Mensagem.Text = "Ativado com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerCategoria.DS_Mensagem;
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

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            BloquearComponentesCadastro();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregarCategoriasConsultar();
        }

        protected void txbNM_Categoria_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Categoria.Focus();
        }

        protected void txbDS_Categoria_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbNM_CategoriaConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_CategoriaConsultar.Text.Trim()))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarCategorias();
            }
        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "Nome";
                e.Row.Cells[3].Text = "Descrição";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbID_Categoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());
            txbNM_Categoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim());
            txbDS_Categoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text.Trim());

            CheckBox ativo = (CheckBox)gvwExibe.SelectedRow.Cells[4].Controls[0];
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

        protected void chkStatusInativo_CheckedChanged(object sender, EventArgs e)
        {
            CarregarCategorias();
        }

        protected void btnAtivarStatus_Click(object sender, EventArgs e)
        {
            Ativar();
            btnAtivarStatus.Enabled = false;
        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            CarregarCategorias();
        }
    }
}