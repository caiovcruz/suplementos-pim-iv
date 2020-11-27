using SuplementosPIMIV.Controller;
using System;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmMeuLogin : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerLogin myControllerLogin;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
            {
                if (!IsPostBack)
                {
                    CarregarMeuLogin();
                    DesbloquearComponentes(false);

                    lblNM_FuncionarioLogin.Text = Session["NM_FuncionarioLogin"].ToString();

                    if (!Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                    {
                        lbtnRelatorios.Visible = false;
                        lbtnDropFuncionarios.Visible = false;
                        lbtnDropProdutos.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("FrmLogin.aspx");
            }
        }

        private void DesbloquearComponentes(bool acao)
        {
            txbDS_UsuarioMeuLogin.Enabled = acao;
            txbDS_SenhaMeuLoginAtual.Enabled = acao;
            txbDS_SenhaMeuLoginNovo.Enabled = acao;
            btnSalvar.Enabled = acao;
        }

        private void LimparCampos()
        {
            txbDS_SenhaMeuLoginAtual.Text = "";
            txbDS_SenhaMeuLoginNovo.Text = "";
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
            myControllerLogin.GetLogin(Session["ID_Login"].ToString());
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
                            if (myValidar.CampoPreenchido(txbDS_SenhaMeuLoginAtual.Text.Trim()))
                            {
                                if (!myValidar.TamanhoCampo(txbDS_SenhaMeuLoginAtual.Text.Trim(), 20))
                                {
                                    mDs_Msg += " Limite de caracteres para a senha atual do usuário excedida, " +
                                                  "o limite para este campo é: 20 caracteres, " +
                                                  "quantidade utilizada: " + txbDS_SenhaMeuLoginNovo.Text.Trim().Length + ".";
                                }
                                else
                                {
                                    if (txbDS_SenhaMeuLoginAtual.Text.Trim().Length < 10)
                                    {
                                        mDs_Msg += " A senha atual do usuário deve conter pelo menos 10 caracteres.";
                                    }
                                    else
                                    {
                                        if (BCrypt.Net.BCrypt.Verify(txbDS_SenhaMeuLoginAtual.Text.Trim(), myControllerLogin.DS_Senha))
                                        {
                                            if (myValidar.CampoPreenchido(txbDS_SenhaMeuLoginNovo.Text.Trim()))
                                            {
                                                if (!myValidar.TamanhoCampo(txbDS_SenhaMeuLoginNovo.Text.Trim(), 20))
                                                {
                                                    mDs_Msg += " Limite de caracteres para a nova senha do usuário excedida, " +
                                                                  "o limite para este campo é: 20 caracteres, " +
                                                                  "quantidade utilizada: " + txbDS_SenhaMeuLoginNovo.Text.Trim().Length + ".";
                                                }
                                                else
                                                {
                                                    if (txbDS_SenhaMeuLoginNovo.Text.Trim().Length < 10)
                                                    {
                                                        mDs_Msg += " A senha do usuário deve conter pelo menos 10 caracteres.";
                                                    }
                                                    else
                                                    {
                                                        if (txbDS_SenhaMeuLoginAtual.Text.Trim() == txbDS_SenhaMeuLoginNovo.Text.Trim())
                                                        {
                                                            mDs_Msg += " A nova senha do usuário não pode ser igual a atual.";
                                                        }
                                                        else
                                                        {
                                                            if (txbDS_UsuarioMeuLogin.Text.Trim() == txbDS_SenhaMeuLoginNovo.Text.Trim())
                                                            {
                                                                mDs_Msg += " O nome de usuário e a nova senha não podem ser iguais.";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mDs_Msg += " A nova senha do usuário deve estar preenchida.";
                                            }
                                        }
                                        else
                                        {
                                            mDs_Msg += " A senha atual do usuário está incorreta.";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                mDs_Msg += " A senha atual do usuário deve estar preenchida.";
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
                    BCrypt.Net.BCrypt.HashPassword(txbDS_SenhaMeuLoginNovo.Text.Trim()),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerLogin.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    CarregarMeuLogin();
                    DesbloquearComponentes(false);
                    LimparCampos();
                    btnAlterar.Enabled = true;
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
            LimparCampos();
            btnAlterar.Enabled = false;
            lblDS_Mensagem.Text = "";
        }
    }
}