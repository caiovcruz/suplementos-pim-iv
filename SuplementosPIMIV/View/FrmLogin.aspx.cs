using SuplementosPIMIV.Controller;
using System;
using System.Drawing;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmLogin : System.Web.UI.Page
    {
        private ControllerLogin myControllerLogin;
        private Validar myValidar;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ConnectionString
                Session["ConnectionString"] = "Server=DRACCON\\SQLEXPRESS;Database=DB_Suplementos_PDV;Trusted_Connection=True;";

                txbDS_Usuario.Focus();
            }
        }

        private void LimparCampos()
        {
            txbDS_Usuario.Text = "";
            txbDS_Senha.Text = "";
        }

        private void Sinalizar(Color c1, Color c2)
        {
            txbDS_Usuario.BorderColor = c1;
            txbDS_Senha.BorderColor = c2;
        }

        protected void btnAcessar_Click(object sender, EventArgs e)
        {
            // Definir variáveis
            string mDs_Usuario = "";
            string mDs_Senha = "";
            string mDs_Msg = "";

            // Capturar dados da tela
            mDs_Usuario = txbDS_Usuario.Text.Trim();
            mDs_Senha = txbDS_Senha.Text.Trim();

            myValidar = new Validar();

            mDs_Msg = (myValidar.CampoPreenchido(mDs_Usuario)) ? "" : "Preencha o campo usuário.";
            txbDS_Usuario.BorderColor = (myValidar.CampoPreenchido(mDs_Usuario)) ? Color.Black : Color.Red;

            mDs_Msg += (myValidar.CampoPreenchido(mDs_Senha)) ? "" : " Preencha o campo senha.";
            txbDS_Senha.BorderColor = (myValidar.CampoPreenchido(mDs_Senha)) ? Color.Black : Color.Red;

            lblDS_Mensagem.Text = mDs_Msg;

            if (mDs_Msg == "")
            {
                myControllerLogin = new ControllerLogin(mDs_Usuario, mDs_Senha, Session["ConnectionString"].ToString());
                mDs_Msg = myControllerLogin.Acessar();

                if (mDs_Msg != "")
                {
                    LimparCampos();
                    lblDS_Mensagem.Text = "";
                    Cache["ID_Login"] = myControllerLogin.ID_Login;
                    Cache["ID_NivelAcesso"] = myControllerLogin.ID_NivelAcesso;
                    Cache["NM_FuncionarioLogin"] = myControllerLogin.NM_FuncionarioLogin;
                    Response.Redirect("FrmMenuPrincipal.aspx");
                }
                else
                {
                    lblDS_Mensagem.Text = mDs_Msg;
                }
            }
        }
    }
}