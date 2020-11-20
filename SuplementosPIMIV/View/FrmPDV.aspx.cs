using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuplementosPIMIV.View
{
    public partial class FrmPDV : System.Web.UI.Page
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