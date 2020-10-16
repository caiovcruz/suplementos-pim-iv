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
    public partial class FrmMarca : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerMarca myControllerMarca;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarMarcas();
                BloquearBotoes();
            }
        }

        private void LimparCampos()
        {
            txbID_Marca.Text = "";
            txbNM_Marca.Text = "";
        }

        private void BloquearBotoes()
        {
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimparMarca.Enabled = false;
            btnConsultar.Enabled = false;
        }

        private void CarregarMarcas()
        {
            // instanciando um objeto da classe ControllerMarca
            myControllerMarca = new ControllerMarca();

            // passando a fonte de dados para o GridView
            gvwMarca.DataSource = myControllerMarca.Exibir();

            // associando os dados para carregar e exibir
            gvwMarca.DataBind();
        }

        private void CarregarMarcasConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_MarcaConsultar.Text, 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_MarcaConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe marca, carregar tela e consultar
                myControllerMarca = new ControllerMarca(txbNM_MarcaConsultar.Text, false);
                gvwMarca.DataSource = myControllerMarca.Consultar();
                gvwMarca.DataBind();
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
                txbNM_Marca.Text.Length > 0 &&
                txbID_Marca.Text.Length == 0;

            btnLimparMarca.Enabled =
                txbNM_Marca.Text.Length > 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbNM_Marca.Text))
            {
                if (!myValidar.TamanhoCampo(txbNM_Marca.Text, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + txbNM_Marca.Text.Length + ".";
                }
                else
                {
                    bool MarcaCadastrada = false;

                    foreach (GridViewRow row in gvwMarca.Rows)
                    {
                        if (txbID_Marca.Text != row.Cells[1].Text)
                        {
                            if (row.Cells[2].Text.Equals(txbNM_Marca.Text))
                            {
                                MarcaCadastrada = true;
                                break;
                            }
                        }
                    }

                    if (MarcaCadastrada.Equals(true))
                    {
                        mDs_Msg = " Marca já cadastrada.";
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
                // instanciar um objeto da classe marca, carregar tela e incluir
                myControllerMarca = new ControllerMarca(
                    txbNM_Marca.Text,
                    true);

                // o que ocorreu?
                if (myControllerMarca.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarMarcas();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerMarca.DS_Mensagem;
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
                // instanciar um objeto da classe sabor, carregar tela e alterar
                myControllerMarca = new ControllerMarca(
                    Convert.ToInt32(txbID_Marca.Text),
                    txbNM_Marca.Text);

                // o que ocorreu?
                if (myControllerMarca.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarMarcas();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerMarca.DS_Mensagem;
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
            // instanciar um objeto da classe sabor e carregar tela e consultar
            myControllerMarca = new ControllerMarca(Convert.ToInt32(txbID_Marca.Text));

            // o que ocorreu?
            if (myControllerMarca.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearBotoes();
                CarregarMarcas();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerMarca.DS_Mensagem;
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

        protected void btnLimparMarca_Click(object sender, EventArgs e)
        {
            LimparCampos();
            BloquearBotoes();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregarMarcasConsultar();
        }

        protected void txbNM_Marca_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbNM_MarcaConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_MarcaConsultar.Text))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarMarcas();
            }
        }

        protected void gvwMarca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "Nome";
            }
        }

        protected void gvwMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbID_Marca.Text = Server.HtmlDecode(gvwMarca.SelectedRow.Cells[1].Text);
            txbNM_Marca.Text = Server.HtmlDecode(gvwMarca.SelectedRow.Cells[2].Text);

            lblDS_Mensagem.Text = "";

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimparMarca.Enabled = true;
        }
    }
}