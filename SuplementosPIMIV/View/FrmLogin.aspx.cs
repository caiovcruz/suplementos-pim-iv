﻿using PontoDeVenda.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validacao;

namespace PontoDeVenda
{
    public partial class FrmLogin : System.Web.UI.Page
    {
        private ControllerLogin myControllerLogin;
        private Validar myValidar;

        protected void Page_Load(object sender, EventArgs e)
        {

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
            mDs_Usuario = txbDS_Usuario.Text;
            mDs_Senha = txbDS_Senha.Text;

            myValidar = new Validar();

            mDs_Msg = (myValidar.CampoPreenchido(mDs_Usuario)) ? "" : "Preencha o campo usuário.";
            txbDS_Usuario.BorderColor = (myValidar.CampoPreenchido(mDs_Usuario)) ? Color.Black : Color.Red;

            mDs_Msg += (myValidar.CampoPreenchido(mDs_Senha)) ? "" : " Preencha o campo senha.";
            txbDS_Senha.BorderColor = (myValidar.CampoPreenchido(mDs_Senha)) ? Color.Black : Color.Red;

            lblDS_Msg.Text = mDs_Msg;

            if (mDs_Msg == "")
            {
                myControllerLogin = new ControllerLogin(mDs_Usuario, mDs_Senha);
                mDs_Msg = myControllerLogin.Acessar();

                if (mDs_Msg == "Ok")
                {
                    LimparCampos();
                    lblDS_Msg.Text = "";
                    Response.Redirect("FrmMenuPrincipal.aspx");
                }
                else
                {
                    lblDS_Msg.Text = mDs_Msg;
                }
            }
        }
    }
}