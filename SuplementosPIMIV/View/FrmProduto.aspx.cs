using MySqlX.XDevAPI.Relational;
using SuplementosPIMIV.Controller;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmProduto : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerProduto myControllerProduto;
        private ControllerCategoria myControllerCategoria;
        private ControllerSubcategoria myControllerSubcategoria;
        private ControllerSabor myControllerSabor;

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
            btnLimparProduto.Enabled = false;
            btnConsultar.Enabled = false;
        }

        private void CarregarProdutos()
        {
            // instanciando um objeto da classe ControllerProduto
            myControllerProduto = new ControllerProduto();

            // passando a fonte de dados para o GridView
            gvwProduto.DataSource = myControllerProduto.Exibir();

            // associando os dados para carregar e exibir
            gvwProduto.DataBind();
        }

        private void CarregarCategorias()
        {
            myControllerCategoria = new ControllerCategoria();

            ddlID_Categoria.DataSource = myControllerCategoria.Exibir();
            ddlID_Categoria.DataTextField = "NM_Categoria";
            ddlID_Categoria.DataValueField = "ID_Categoria";
            ddlID_Categoria.DataBind();

            ddlID_Categoria.Items.Insert(0, "Categoria");
            ddlID_Categoria.SelectedIndex = 0;
        }

        private void CarregarSubcategorias()
        {
            myControllerSubcategoria = new ControllerSubcategoria();

            ddlID_Subcategoria.DataSource = myControllerSubcategoria.Exibir();
            ddlID_Subcategoria.DataTextField = "NM_Subcategoria";
            ddlID_Subcategoria.DataValueField = "ID_Subcategoria";
            ddlID_Subcategoria.DataBind();

            ddlID_Subcategoria.Items.Insert(0, "Subcategoria");
            ddlID_Subcategoria.SelectedIndex = 0;
        }

        private void CarregarSabores()
        {
            myControllerSabor = new ControllerSabor();

            ddlID_Sabor.DataSource = myControllerSabor.Exibir();
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
                txbDS_Produto.Text.Length > 0 &&
                txbQTD_Estoque.Text.Length > 0 &&
                txbPR_Venda.Text.Length > 0 &&
                txbPR_Custo.Text.Length > 0 &&
                ddlID_Categoria.SelectedIndex != 0 &&
                ddlID_Subcategoria.SelectedIndex != 0 &&
                ddlID_Sabor.SelectedIndex != 0 &&
                txbID_Produto.Text.Length == 0;

            btnLimparProduto.Enabled =
                txbNM_Produto.Text.Length > 0 ||
                txbDS_Produto.Text.Length > 0 ||
                txbQTD_Estoque.Text.Length > 0 ||
                txbPR_Venda.Text.Length > 0 ||
                txbPR_Custo.Text.Length > 0 ||
                ddlID_Categoria.SelectedIndex != 0 ||
                ddlID_Subcategoria.SelectedIndex != 0 ||
                ddlID_Sabor.SelectedIndex != 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

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
                    bool produtoCadastrado = false;

                    foreach (GridViewRow row in gvwProduto.Rows)
                    {
                        if (txbID_Produto.Text != row.Cells[1].Text)
                        {
                            if (row.Cells[2].Text.Equals(txbNM_Produto.Text))
                            {
                                produtoCadastrado = true;
                                break;
                            }
                        }
                    }

                    if (produtoCadastrado.Equals(true))
                    {
                        mDs_Msg = " Produto já cadastrado.";
                    }
                    else
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


                        if (myValidar.CampoPreenchido(txbQTD_Estoque.Text))
                        {
                            if (!myValidar.Numero(txbQTD_Estoque.Text))
                            {
                                mDs_Msg += " A quantidade em estoque deve ser um valor numérico.";
                            }
                        }
                        else
                        {
                            mDs_Msg += " A quantidade em estoque deve estar preenchida.";
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
                // instanciar um objeto da classe produto, carregar tela e incluir
                myControllerProduto = new ControllerProduto(
                    Convert.ToInt32(ddlID_Categoria.SelectedValue),
                    Convert.ToInt32(ddlID_Subcategoria.SelectedValue),
                    Convert.ToInt32(ddlID_Sabor.SelectedValue),
                    txbNM_Produto.Text,
                    txbDS_Produto.Text,
                    Convert.ToInt32(txbQTD_Estoque.Text),
                    Convert.ToDouble(txbPR_Custo.Text),
                    Convert.ToDouble(txbPR_Venda.Text));

                // o que ocorreu?
                if (myControllerProduto.DS_Mensagem == "OK")
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
                    Convert.ToInt32(ddlID_Categoria.SelectedValue),
                    Convert.ToInt32(ddlID_Subcategoria.SelectedValue),
                    Convert.ToInt32(ddlID_Sabor.SelectedValue),
                    txbNM_Produto.Text,
                    txbDS_Produto.Text,
                    Convert.ToInt32(txbQTD_Estoque.Text),
                    Convert.ToDouble(txbPR_Custo.Text),
                    Convert.ToDouble(txbPR_Venda.Text));

                // o que ocorreu?
                if (myControllerProduto.DS_Mensagem == "OK")
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
                    lblDS_Mensagem.Text = myControllerProduto.DS_Mensagem;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void CarregarProdutosConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_ProdutoConsultar.Text, 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_ProdutoConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e consultar
                myControllerProduto = new ControllerProduto(txbNM_ProdutoConsultar.Text);
                gvwProduto.DataSource = myControllerProduto.Consultar();
                gvwProduto.DataBind();
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void Excluir()
        {
            // instanciar um objeto da classe produto e carregar tela e consultar
            myControllerProduto = new ControllerProduto(Convert.ToInt32(txbID_Produto.Text));

            // o que ocorreu?
            if (myControllerProduto.DS_Mensagem == "OK")
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

        protected void btnLimparProduto_Click(object sender, EventArgs e)
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
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[7].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "Nome";
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Text = "Categoria";
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Text = "Subcategoria";
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
            txbNM_Produto.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[2].Text);
            ddlID_Categoria.SelectedValue = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[3].Text);
            ddlID_Subcategoria.SelectedValue = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[5].Text);
            ddlID_Subcategoria.Enabled = true;
            ddlID_Sabor.SelectedValue = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[7].Text);
            txbDS_Produto.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[9].Text);
            txbQTD_Estoque.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[10].Text);
            txbPR_Custo.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[11].Text);
            txbPR_Venda.Text = Server.HtmlDecode(gvwProduto.SelectedRow.Cells[12].Text);

            lblDS_Mensagem.Text = "";

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimparProduto.Enabled = true;
        }

        protected void txbNM_ProdutoConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_ProdutoConsultar.Text))
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

        protected void txbNM_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Produto.Focus();
        }

        protected void txbDS_Produto_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlID_Categoria.Focus();
        }

        protected void ddlID_Categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();

            ddlID_Subcategoria.Enabled = ddlID_Categoria.SelectedIndex != 0;
            ddlID_Subcategoria.Focus();
        }

        protected void ddlID_Subcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlID_Sabor.Focus();
        }

        protected void ddlID_Sabor_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbQTD_Estoque.Focus();
        }

        protected void txbQTD_Estoque_TextChanged(object sender, EventArgs e)
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
    }
}