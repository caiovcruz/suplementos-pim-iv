using SuplementosPIMIV.Controller;
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
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarCategorias();
                BloquearBotoes();
            }
        }

        private void LimparCampos()
        {
            txbID_Categoria.Text = "";
            txbNM_Categoria.Text = "";
            txbDS_Categoria.Text = "";
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

        private void CarregarCategorias()
        {
            // instanciando um objeto da classe ControllerCategoria
            myControllerCategoria = new ControllerCategoria();

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerCategoria.Exibir();

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarCategoriasConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_CategoriaConsultar.Text, 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_CategoriaConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe categoria, carregar tela e consultar
                myControllerCategoria = new ControllerCategoria(txbNM_CategoriaConsultar.Text);
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
                txbNM_Categoria.Text.Length > 0 &&
                txbDS_Categoria.Text.Length > 0 &&
                txbID_Categoria.Text.Length == 0;

            btnLimpar.Enabled =
                txbNM_Categoria.Text.Length > 0 ||
                txbDS_Categoria.Text.Length > 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbNM_Categoria.Text))
            {
                if (!myValidar.TamanhoCampo(txbNM_Categoria.Text, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + txbNM_Categoria.Text.Length + ".";
                }
                else
                {
                    bool categoriaCadastrada = false;

                    foreach (GridViewRow row in gvwExibe.Rows)
                    {
                        if (txbID_Categoria.Text != row.Cells[1].Text)
                        {
                            if (row.Cells[2].Text.Equals(txbNM_Categoria.Text))
                            {
                                categoriaCadastrada = true;
                                break;
                            }
                        }
                    }

                    if (categoriaCadastrada.Equals(true))
                    {
                        mDs_Msg = " Categoria já cadastrada.";
                    }
                    else
                    {
                        if (myValidar.CampoPreenchido(txbDS_Categoria.Text))
                        {
                            if (!myValidar.TamanhoCampo(txbDS_Categoria.Text, 1500))
                            {
                                mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                              "o limite para este campo é: 3000 caracteres, " +
                                              "quantidade utilizada: " + txbDS_Categoria.Text.Length + ".";
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
                // instanciar um objeto da classe categoria, carregar tela e incluir
                myControllerCategoria = new ControllerCategoria(
                    txbNM_Categoria.Text,
                    txbDS_Categoria.Text);

                // o que ocorreu?
                if (myControllerCategoria.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarCategorias();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerCategoria.DS_Mensagem;
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
                // instanciar um objeto da classe categoria, carregar tela e alterar
                myControllerCategoria = new ControllerCategoria(
                    Convert.ToInt32(txbID_Categoria.Text),
                    txbNM_Categoria.Text,
                    txbDS_Categoria.Text);

                // o que ocorreu?
                if (myControllerCategoria.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarCategorias();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerCategoria.DS_Mensagem;
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
            // instanciar um objeto da classe categoria e carregar tela e consultar
            myControllerCategoria = new ControllerCategoria(Convert.ToInt32(txbID_Categoria.Text));

            // o que ocorreu?
            if (myControllerCategoria.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearBotoes();
                CarregarCategorias();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
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
            BloquearBotoes();
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
            if (!string.IsNullOrWhiteSpace(txbNM_CategoriaConsultar.Text))
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
            txbID_Categoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text);
            txbNM_Categoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text);
            txbDS_Categoria.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text);

            lblDS_Mensagem.Text = "";

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }
    }
}