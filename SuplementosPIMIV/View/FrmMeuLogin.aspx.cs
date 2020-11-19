using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuplementosPIMIV.View
{
    public partial class FrmMeuLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblNM_FuncionarioLogin.Text = Cache["NM_FuncionarioLogin"].ToString();
            }
        }

        protected void txbDS_UsuarioMeuLogin_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txbDS_SenhaMeuLogin_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {

        }
    }
}