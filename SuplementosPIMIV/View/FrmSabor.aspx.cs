using SuplementosPIMIV.Controller;
using System;
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

            if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
            {
                if (Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                {
                    if (!IsPostBack)
                    {
                        LimparCampos();
                        CarregarSabores();
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
            txbID_Sabor.Text = "";
            txbNM_Sabor.Text = "";
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

        private void CarregarSabores()
        {
            // instanciando um objeto da classe ControllerSabor
            myControllerSabor = new ControllerSabor();

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerSabor.Exibir(chkStatusInativo.Checked ? "0" : "1", Session["ConnectionString"].ToString());

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarSaboresConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_SaborConsultar.Text.Trim(), 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_SaborConsultar.Text.Trim().Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe sabor, carregar tela e consultar
                myControllerSabor = new ControllerSabor();
                gvwExibe.DataSource = myControllerSabor.Consultar(chkStatusInativo.Checked ? "0" : "1", txbNM_SaborConsultar.Text.Trim(), Session["ConnectionString"].ToString());
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
                txbNM_Sabor.Text.Trim().Length > 0 &&
                txbID_Sabor.Text.Trim().Length == 0;

            btnLimpar.Enabled =
                txbNM_Sabor.Text.Trim().Length > 0;
        }

        private void Incluir()
        {
            myControllerSabor = new ControllerSabor(
                txbNM_Sabor.Text.Trim(),
                Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerSabor.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarSabores();
                lblDS_Mensagem.Text = "Incluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerSabor.DS_Mensagem;
            }
        }

        private void Alterar()
        {
            myControllerSabor = new ControllerSabor(
                txbID_Sabor.Text.Trim(),
                txbNM_Sabor.Text.Trim(),
                Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerSabor.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarSabores();
                lblDS_Mensagem.Text = "Alterado com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerSabor.DS_Mensagem;
            }
        }

        private void Excluir()
        {
            // instanciar um objeto da classe sabor e carregar tela e consultar
            myControllerSabor = new ControllerSabor(txbID_Sabor.Text.Trim(), 'E', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerSabor.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarSabores();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerSabor.DS_Mensagem;
            }
        }

        private void Ativar()
        {
            // instanciar um objeto da classe sabor e carregar tela e ativar
            myControllerSabor = new ControllerSabor(txbID_Sabor.Text.Trim(), 'A', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerSabor.DS_Mensagem == "OK")
            {
                // tudo certinho!
                CarregarSabores();
                lblDS_Mensagem.Text = "Ativado com sucesso!";
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
            BloquearComponentesCadastro();
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
            if (!string.IsNullOrWhiteSpace(txbNM_SaborConsultar.Text.Trim()))
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
            txbID_Sabor.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());
            txbNM_Sabor.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim());

            CheckBox ativo = (CheckBox)gvwExibe.SelectedRow.Cells[3].Controls[0];
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
            CarregarSabores();
        }

        protected void btnAtivarStatus_Click(object sender, EventArgs e)
        {
            Ativar();
            btnAtivarStatus.Enabled = false;
        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            CarregarSabores();
        }
    }
}