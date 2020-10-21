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
    public partial class FrmSabor : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerSabor myControllerSabor;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarSabores();
                BloquearBotoes();
            }
        }

        private void LimparCampos()
        {
            txbID_Sabor.Text = "";
            txbNM_Sabor.Text = "";
        }

        private void BloquearBotoes()
        {
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            btnConsultar.Enabled = false;
        }

        private void CarregarSabores()
        {
            // instanciando um objeto da classe ControllerSabor
            myControllerSabor = new ControllerSabor(Session["ConnectionString"].ToString());

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerSabor.Exibir();

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarSaboresConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_SaborConsultar.Text, 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_SaborConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe sabor, carregar tela e consultar
                myControllerSabor = new ControllerSabor(txbNM_SaborConsultar.Text, false, Session["ConnectionString"].ToString());
                gvwExibe.DataSource = myControllerSabor.Consultar();
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
                txbNM_Sabor.Text.Length > 0 &&
                txbID_Sabor.Text.Length == 0;

            btnLimpar.Enabled =
                txbNM_Sabor.Text.Length > 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbNM_Sabor.Text))
            {
                if (!myValidar.TamanhoCampo(txbNM_Sabor.Text, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + txbNM_Sabor.Text.Length + ".";
                }
                else
                {
                    bool SaborCadastrado = false;

                    foreach (GridViewRow row in gvwExibe.Rows)
                    {
                        if (txbID_Sabor.Text != row.Cells[1].Text)
                        {
                            if (row.Cells[2].Text.Equals(txbNM_Sabor.Text))
                            {
                                SaborCadastrado = true;
                                break;
                            }
                        }
                    }

                    if (SaborCadastrado.Equals(true))
                    {
                        mDs_Msg = " Sabor já cadastrado.";
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
                // instanciar um objeto da classe sabor, carregar tela e incluir
                myControllerSabor = new ControllerSabor(
                    txbNM_Sabor.Text,
                    true,
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerSabor.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarSabores();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerSabor.DS_Mensagem;
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
                myControllerSabor = new ControllerSabor(
                    Convert.ToInt32(txbID_Sabor.Text),
                    txbNM_Sabor.Text,
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerSabor.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearBotoes();
                    CarregarSabores();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerSabor.DS_Mensagem;
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
            myControllerSabor = new ControllerSabor(Convert.ToInt32(txbID_Sabor.Text), Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerSabor.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearBotoes();
                CarregarSabores();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerSabor.DS_Mensagem;
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
            CarregarSaboresConsultar();
        }

        protected void txbNM_Sabor_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }

        protected void txbNM_SaborConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_SaborConsultar.Text))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarSabores();
            }
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
                e.Row.Cells[2].Text = "Nome";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbID_Sabor.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text);
            txbNM_Sabor.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text);

            lblDS_Mensagem.Text = "";

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }
    }
}