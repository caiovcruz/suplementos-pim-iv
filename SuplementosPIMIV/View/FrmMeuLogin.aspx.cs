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
    public partial class FrmMeuLogin : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerLogin myControllerLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ConnectionString"] != null)
                {
                    CarregarMeuLogin();
                    DesbloquearComponentes(false);

                    lblNM_FuncionarioLogin.Text = Session["NM_FuncionarioLogin"].ToString();
                }
                else
                {
                    Response.Redirect("FrmLogin.aspx");
                }
            }
        }

        private void DesbloquearComponentes(bool acao)
        {
            txbDS_UsuarioMeuLogin.Enabled = acao;
            txbDS_SenhaMeuLogin.Enabled = acao;
            btnSalvar.Enabled = acao;
        }

        private void CarregarMeuLogin()
        {
            myControllerLogin = new ControllerLogin(Session["ConnectionString"].ToString());
            myControllerLogin.GetLogin(Session["ID_Login"].ToString());

            // o que ocorreu?
            if (myControllerLogin.DS_Mensagem == "OK")
            {
                // tudo certinho!
                txbDS_UsuarioMeuLogin.Text = myControllerLogin.DS_Usuario;
                txbDS_SenhaMeuLogin.Text = myControllerLogin.DS_Senha;

                string mascara = "";
                foreach (char c in txbDS_SenhaMeuLogin.Text)
                {
                    mascara += "•";
                }

                txbDS_SenhaMeuLoginMascara.Text = mascara;

                txbDS_SenhaMeuLogin.Visible = false;
                txbDS_SenhaMeuLoginMascara.Visible = true;
                txbDS_SenhaMeuLoginMascara.Enabled = false;
                lbtnVisualizarSenha.Visible = true;
                lbtnMascararSenha.Visible = false;
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerLogin.DS_Mensagem;
            }
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            myControllerLogin = new ControllerLogin(Session["ConnectionString"].ToString());
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbDS_UsuarioMeuLogin.Text.Trim()))
            {
                if (!myValidar.TamanhoCampo(txbDS_UsuarioMeuLogin.Text.Trim(), 20))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 20 caracteres, " +
                                  "quantidade utilizada: " + txbDS_UsuarioMeuLogin.Text.Trim().Length + ".";
                }
                else
                {
                    if (txbDS_UsuarioMeuLogin.Text.Trim().Length < 10)
                    {
                        mDs_Msg = " O nome de usuário deve conter pelo menos 10 caracteres";
                    }
                    else
                    {
                        if (myControllerLogin.VerificarLoginCadastrado(Session["ID_Login"].ToString(), Session["ID_Funcionario"].ToString(), txbDS_UsuarioMeuLogin.Text.Trim()).Equals(""))
                        {
                            if (myValidar.CampoPreenchido(txbDS_SenhaMeuLogin.Text.Trim()))
                            {
                                if (!myValidar.TamanhoCampo(txbDS_SenhaMeuLogin.Text.Trim(), 20))
                                {
                                    mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                  "o limite para este campo é: 20 caracteres, " +
                                                  "quantidade utilizada: " + txbDS_SenhaMeuLogin.Text.Trim().Length + ".";
                                }
                                else
                                {
                                    if (txbDS_SenhaMeuLogin.Text.Trim().Length < 10)
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
                            mDs_Msg += " " + myControllerLogin.DS_Mensagem;
                        }
                    }
                }
            }
            else
            {
                mDs_Msg = " O nome de usuário deve estar preenchido.";
            }

            return mDs_Msg;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe login, carregar tela e alterar
                myControllerLogin = new ControllerLogin(
                    Convert.ToInt32(Session["ID_Login"].ToString()),
                    Convert.ToInt32(Session["ID_Funcionario"].ToString()),
                    Session["DS_NivelAcesso"].ToString(),
                    txbDS_UsuarioMeuLogin.Text.Trim(),
                    txbDS_SenhaMeuLogin.Text.Trim(),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerLogin.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    CarregarMeuLogin();
                    DesbloquearComponentes(false);
                    btnAlterar.Enabled = true;
                    txbDS_SenhaMeuLogin.Visible = false;
                    txbDS_SenhaMeuLoginMascara.Visible = true;
                    lbtnVisualizarSenha.Visible = true;
                    lbtnMascararSenha.Visible = false;
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

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            DesbloquearComponentes(true);
            btnAlterar.Enabled = false;
            lblDS_Mensagem.Text = "";

            txbDS_SenhaMeuLogin.Visible = true;
            txbDS_SenhaMeuLoginMascara.Visible = false;
            lbtnVisualizarSenha.Visible = false;
            lbtnMascararSenha.Visible = false;
        }

        protected void lbtnVisualizarSenha_Click(object sender, EventArgs e)
        {
            txbDS_SenhaMeuLogin.Visible = true;
            txbDS_SenhaMeuLoginMascara.Visible = false;
            lbtnVisualizarSenha.Visible = false;
            lbtnMascararSenha.Visible = true;
        }

        protected void lbtnMascararSenha_Click(object sender, EventArgs e)
        {
            txbDS_SenhaMeuLogin.Visible = false;
            txbDS_SenhaMeuLoginMascara.Visible = true;
            lbtnVisualizarSenha.Visible = true;
            lbtnMascararSenha.Visible = false;
        }
    }
}