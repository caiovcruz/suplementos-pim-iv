using System;

namespace SuplementosPIMIV.View
{
    public partial class FrmMenuPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblNM_FuncionarioLogin.Text = Cache["NM_FuncionarioLogin"].ToString();
            }
        }
    }
}