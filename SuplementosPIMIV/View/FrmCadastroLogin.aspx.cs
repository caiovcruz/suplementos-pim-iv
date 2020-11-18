using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuplementosPIMIV.View
{
    public partial class FrmCadastroLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblNM_FuncionarioLogin.Text = Cache["NM_FuncionarioLogin"].ToString();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {

        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {

        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void txbNM_FuncionarioLoginConsultar_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        protected void ddlID_Funcionario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlID_NivelAcesso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txbDS_Usuario_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txbDS_Senha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}