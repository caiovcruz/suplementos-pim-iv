﻿using SuplementosPIMIV.Controller;
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
                Session["ID_Login"] = null;
                Session["ID_Funcionario"] = null;
                Session["DS_NivelAcesso"] = null;
                Session["NM_FuncionarioLogin"] = null;

                txbDS_Usuario.Focus();
            }
        }

        private void LimparCampos()
        {
            txbDS_Usuario.Text = "";
            txbDS_Senha.Text = "";
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
                myControllerLogin = new ControllerLogin();
                mDs_Msg = myControllerLogin.Acessar(mDs_Usuario, Session["ConnectionString"].ToString());

                if (mDs_Msg == "OK")
                {
                    if (BCrypt.Net.BCrypt.Verify(mDs_Senha, myControllerLogin.DS_Senha))
                    {
                        LimparCampos();
                        lblDS_Mensagem.Text = "";

                        Session["ID_Login"] = myControllerLogin.ID_Login;
                        Session["ID_Funcionario"] = myControllerLogin.ID_Funcionario;
                        Session["DS_NivelAcesso"] = myControllerLogin.DS_NivelAcesso;
                        Session["NM_FuncionarioLogin"] = myControllerLogin.NM_FuncionarioLogin;

                        Response.Redirect("FrmPDV.aspx");
                    }
                    else
                    {
                        lblDS_Mensagem.Text = "Senha incorreta!";
                    }
                }
                else
                {
                    lblDS_Mensagem.Text = mDs_Msg;
                }
            }
        }
    }
}