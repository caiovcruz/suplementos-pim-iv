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
    public partial class FrmSubcategoria : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerSubcategoria myControllerSubcategoria;
        private ControllerCategoria myControllerCategoria;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarSubcategorias();
                CarregarCategorias();
                BloquearBotoes();
            }
        }

        private void LimparCampos()
        {
            txbID_Subcategoria.Text = "";
            ddlID_Categoria.SelectedIndex = 0;
            txbNM_Subcategoria.Text = "";
            txbDS_Subcategoria.Text = "";
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

        private void CarregarSubcategorias()
        {
            // instanciando um objeto da classe ControllerSubcategoria
            myControllerSubcategoria = new ControllerSubcategoria();

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerSubcategoria.Exibir();

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarCategorias()
        {
            myControllerCategoria = new ControllerCategoria();

            ddlID_Categoria.DataSource = myControllerCategoria.Exibir();
            ddlID_Categoria.DataTextField = "NM_Categoria";
            ddlID_Categoria.DataValueField = "ID_Categoria";
            ddlID_Categoria.DataBind();

            ddlID_Categoria.Items.Insert(0, "Categoria base");
            ddlID_Categoria.SelectedIndex = 0;
        }

        private void CarregarSubcategoriasConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_SubcategoriaConsultar.Text, 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_SubcategoriaConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe categoria, carregar tela e consultar
                myControllerCategoria = new ControllerCategoria(txbNM_SubcategoriaConsultar.Text);
                gvwExibe.DataSource = myControllerCategoria.Consultar();
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
                ddlID_Categoria.SelectedIndex != 0 &&
                txbNM_Subcategoria.Text.Length > 0 &&
                txbDS_Subcategoria.Text.Length > 0 &&
                txbID_Subcategoria.Text.Length == 0;

            btnLimpar.Enabled =
                ddlID_Categoria.SelectedIndex != 0 ||
                txbNM_Subcategoria.Text.Length > 0 ||
                txbDS_Subcategoria.Text.Length > 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbNM_Subcategoria.Text))
            {
                if (!myValidar.TamanhoCampo(txbNM_Subcategoria.Text, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + txbNM_Subcategoria.Text.Length + ".";
                }
                else
                {
                    bool categoriaCadastrada = false;

                    foreach (GridViewRow row in gvwExibe.Rows)
                    {
                        if (txbID_Subcategoria.Text != row.Cells[1].Text)
                        {
                            if (row.Cells[2].Text.Equals(txbNM_Subcategoria.Text))
                            {
                                categoriaCadastrada = true;
                                break;
                            }
                        }
                    }

                    if (categoriaCadastrada.Equals(true))
                    {
                        mDs_Msg = " Subcategoria já cadastrada.";
                    }
                    else
                    {
                        if (myValidar.CampoPreenchido(txbDS_Subcategoria.Text))
                        {
                            if (!myValidar.TamanhoCampo(txbDS_Subcategoria.Text, 1500))
                            {
                                mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                              "o limite para este campo é: 3000 caracteres, " +
                                              "quantidade utilizada: " + txbDS_Subcategoria.Text.Length + ".";
                            }
                        }
                        else
                        {
                            mDs_Msg += " A descrição deve estar preenchida.";
                        }
                    }
                }
            }
            else
            {
                mDs_Msg = " O nome deve estar preenchido.";
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
                // instanciar um objeto da classe subcategoria, carregar tela e incluir
                myControllerSubcategoria = new ControllerSubcategoria(
                    Convert.ToInt32(ddlID_Categoria.SelectedValue),
                    txbNM_Subcategoria.Text,
                    txbDS_Subcategoria.Text);

                // o que ocorreu?
                if (myControllerSubcategoria.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarSubcategorias();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerSubcategoria.DS_Mensagem;
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
                // instanciar um objeto da classe subcategoria, carregar tela e alterar
                myControllerSubcategoria = new ControllerSubcategoria(
                    Convert.ToInt32(txbID_Subcategoria.Text),
                    Convert.ToInt32(ddlID_Categoria.SelectedValue),
                    txbNM_Subcategoria.Text,
                    txbDS_Subcategoria.Text);

                // o que ocorreu?
                if (myControllerSubcategoria.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarSubcategorias();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerSubcategoria.DS_Mensagem;
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
            // instanciar um objeto da classe subcategoria e carregar tela e consultar
            myControllerCategoria = new ControllerCategoria(Convert.ToInt32(txbID_Subcategoria.Text));

            // o que ocorreu?
            if (myControllerSubcategoria.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearBotoes();
                CarregarSubcategorias();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerSubcategoria.DS_Mensagem;
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
            BloquearBotoes();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregarSubcategoriasConsultar();
        }

        protected void ddlID_Categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbNM_Subcategoria.Focus();
        }

        protected void txbNM_Subcategoria_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Subcategoria.Focus();
        }

        protected void txbDS_Subcategoria_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbNM_SubcategoriaConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_SubcategoriaConsultar.Text))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarSubcategorias();
            }
        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[5].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                e.Row.Cells[3].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "Nome";
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Text = "Categoria base";
                e.Row.Cells[5].Text = "Descrição";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbID_Subcategoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text);
            txbNM_Subcategoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text);
            ddlID_Categoria.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text);
            txbDS_Subcategoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[5].Text);

            lblDS_Mensagem.Text = "";

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }
    }
}