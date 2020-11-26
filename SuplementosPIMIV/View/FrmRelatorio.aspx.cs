using SuplementosPIMIV.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuplementosPIMIV.View
{
    public partial class FrmRelatorio : System.Web.UI.Page
    {
        private ControllerVenda myControllerVenda;
        private ControllerItemVenda myControllerItemVenda;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
                {
                    if (Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                    {
                        // instanciando um objeto da classe ControllerItemVenda
                        myControllerVenda = new ControllerVenda(Session["ConnectionString"].ToString());

                        // passando a fonte de dados para o GridView
                        gvwExibe.DataSource = myControllerVenda.Exibir();

                        // associando os dados para carregar e exibir
                        gvwExibe.DataBind();
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

        protected void ddlDiaRelatorioInicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMesRelatorioInicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAnoRelatorioInicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDiaRelatorioFinal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMesRelatorioFinal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAnoRelatorioFinal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
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
    }
}