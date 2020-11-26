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
    public partial class FrmCadastroLogin : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerLogin myControllerLogin;
        private ControllerFuncionario myControllerFuncionario;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
                {
                    if (Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                    {
                        LimparCampos();
                        CarregarLogins();
                        CarregarFuncionarios();
                        CarregarNiveisAcesso();
                        BloquearComponentesCadastro();
                        BloquearComponentesExibe();

                        lblNM_FuncionarioLogin.Text = Session["NM_FuncionarioLogin"].ToString();
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
        }

        private void LimparCampos()
        {
            txbID_Login.Text = "";
            ddlID_Funcionario.SelectedIndex = 0;
            ddlDS_NivelAcesso.SelectedIndex = 0;
            txbDS_Usuario.Text = "";
            txbDS_Senha.Text = "";
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

        private void CarregarLogins()
        {
            // instanciando um objeto da classe ControllerLogin
            myControllerLogin = new ControllerLogin(Session["ConnectionString"].ToString());

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerLogin.Exibir(chkStatusInativo.Checked ? 0 : 1);

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarFuncionarios()
        {
            myControllerFuncionario = new ControllerFuncionario(Session["ConnectionString"].ToString());

            ddlID_Funcionario.DataSource = myControllerFuncionario.Exibir(1);
            ddlID_Funcionario.DataTextField = "NM_Funcionario";
            ddlID_Funcionario.DataValueField = "ID_Funcionario";
            ddlID_Funcionario.DataBind();

            ddlID_Funcionario.Items.Insert(0, "Funcionário");
            ddlID_Funcionario.SelectedIndex = 0;
        }

        private void CarregarNiveisAcesso()
        {
            ddlDS_NivelAcesso.Items.Insert(0, "Nível de Acesso");
            ddlDS_NivelAcesso.Items.Insert(1, "Vendedor");
            ddlDS_NivelAcesso.Items.Insert(2, "Gerente");

            ddlDS_NivelAcesso.SelectedIndex = 0;
        }

        private void CarregarLoginsConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbNM_FuncionarioLoginConsultar.Text.Trim(), 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbNM_FuncionarioLoginConsultar.Text.Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe login, carregar tela e consultar
                myControllerLogin = new ControllerLogin(Session["ConnectionString"].ToString());
                gvwExibe.DataSource = myControllerLogin.Consultar(chkStatusInativo.Checked ? 0 : 1, txbNM_FuncionarioLoginConsultar.Text.Trim());
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
                txbID_Login.Text.Trim().Length == 0 &&
                ddlID_Funcionario.SelectedIndex != 0 &&
                ddlDS_NivelAcesso.SelectedIndex != 0 &&
                txbDS_Usuario.Text.Trim().Length > 0 &&
                txbDS_Senha.Text.Trim().Length > 0;

            btnLimpar.Enabled =
                ddlID_Funcionario.SelectedIndex != 0 ||
                ddlDS_NivelAcesso.SelectedIndex != 0 ||
                txbDS_Usuario.Text.Trim().Length > 0 ||
                txbDS_Senha.Text.Trim().Length > 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            myControllerLogin = new ControllerLogin(Session["ConnectionString"].ToString());
            string mDs_Msg = "";

            if (ddlID_Funcionario.SelectedIndex.Equals(0))
            {
                mDs_Msg = " É necessário selecionar um funcionário.";
            }
            else
            {
                if (myValidar.CampoPreenchido(txbDS_Usuario.Text.Trim()))
                {
                    if (!myValidar.TamanhoCampo(txbDS_Usuario.Text.Trim(), 20))
                    {
                        mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                      "o limite para este campo é: 20 caracteres, " +
                                      "quantidade utilizada: " + txbDS_Usuario.Text.Trim().Length + ".";
                    }
                    else
                    {
                        if (txbDS_Usuario.Text.Trim().Length < 10)
                        {
                            mDs_Msg = " O nome de usuário deve conter pelo menos 10 caracteres";
                        }
                        else
                        {
                            if (myControllerLogin.VerificarLoginCadastrado(txbID_Login.Text.Trim(), ddlID_Funcionario.SelectedValue, txbDS_Usuario.Text.Trim()).Equals(""))
                            {
                                if (myValidar.CampoPreenchido(txbDS_Senha.Text.Trim()))
                                {
                                    if (!myValidar.TamanhoCampo(txbDS_Senha.Text.Trim(), 20))
                                    {
                                        mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                      "o limite para este campo é: 20 caracteres, " +
                                                      "quantidade utilizada: " + txbDS_Senha.Text.Trim().Length + ".";
                                    }
                                    else
                                    {
                                        if (ddlDS_NivelAcesso.SelectedIndex.Equals(0))
                                        {
                                            mDs_Msg += " É necessário selecionar um nível de acesso.";
                                        }

                                        if (txbDS_Senha.Text.Trim().Length < 10)
                                        {
                                            mDs_Msg += " A senha do usuário deve conter pelo menos 10 caracteres";
                                        }
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " A senha do usuário deve estar preenchida.";
                                }
                            }
                            else
                            {
                                mDs_Msg += " " + myControllerLogin.DS_Mensagem + " Verifique nos logins ativos e inativos!";
                            }
                        }
                    }
                }
                else
                {
                    mDs_Msg = " O nome de usuário deve estar preenchido.";
                }
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
                // instanciar um objeto da classe login, carregar tela e incluir
                myControllerLogin = new ControllerLogin(
                    Convert.ToInt32(ddlID_Funcionario.SelectedValue),
                    ddlDS_NivelAcesso.SelectedValue,
                    txbDS_Usuario.Text.Trim(),
                    txbDS_Senha.Text.Trim(),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerLogin.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearComponentesCadastro();
                    CarregarLogins();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerLogin.DS_Mensagem;
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
                // instanciar um objeto da classe login, carregar tela e alterar
                myControllerLogin = new ControllerLogin(
                    Convert.ToInt32(txbID_Login.Text.Trim()),
                    Convert.ToInt32(ddlID_Funcionario.SelectedValue),
                    ddlDS_NivelAcesso.SelectedValue,
                    txbDS_Usuario.Text.Trim(),
                    txbDS_Senha.Text.Trim(),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerLogin.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearComponentesCadastro();
                    CarregarLogins();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerLogin.DS_Mensagem;
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
            // instanciar um objeto da classe login e carregar tela e excluir
            myControllerLogin = new ControllerLogin(Convert.ToInt32(txbID_Login.Text.Trim()), 'E', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerLogin.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarLogins();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerLogin.DS_Mensagem;
            }
        }

        private void Ativar()
        {
            // instanciar um objeto da classe login e carregar tela e ativar
            myControllerLogin = new ControllerLogin(Convert.ToInt32(txbID_Login.Text.Trim()), 'A', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerLogin.DS_Mensagem == "OK")
            {
                // tudo certinho!
                CarregarLogins();
                lblDS_Mensagem.Text = "Ativado com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerLogin.DS_Mensagem;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregarLoginsConsultar();
        }

        protected void txbNM_FuncionarioLoginConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbNM_FuncionarioLoginConsultar.Text.Trim()))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarLogins();
            }
        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[2].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Text = "Funcionário";
                e.Row.Cells[4].Text = "Nível de Acesso";
                e.Row.Cells[5].Text = "Usuário";
                e.Row.Cells[6].Text = "Senha";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDS_Mensagem.Text = "";

            txbID_Login.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());

            try
            {
                ddlID_Funcionario.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim());
            }
            catch (Exception)
            {
                lblDS_Mensagem.Text += " Funcionário [ " + gvwExibe.SelectedRow.Cells[3].Text.Trim() + " ] inativo.";
                ddlID_Funcionario.SelectedIndex = 0;
            }

            ddlDS_NivelAcesso.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[4].Text.Trim());
            txbDS_Usuario.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[5].Text.Trim());
            txbDS_Senha.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[6].Text.Trim());

            CheckBox ativo = (CheckBox)gvwExibe.SelectedRow.Cells[7].Controls[0];
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

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void chkStatusInativo_CheckedChanged(object sender, EventArgs e)
        {
            CarregarLogins();
        }

        protected void btnAtivarStatus_Click(object sender, EventArgs e)
        {
            Ativar();
            btnAtivarStatus.Enabled = false;
        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            CarregarLogins();
        }

        protected void ddlID_Funcionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlDS_NivelAcesso.Focus();
        }

        protected void ddlDS_NivelAcesso_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Usuario.Focus();
        }

        protected void txbDS_Usuario_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Senha.Focus();
        }

        protected void txbDS_Senha_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }
    }
}