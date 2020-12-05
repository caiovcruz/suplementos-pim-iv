using SuplementosPIMIV.Controller;
using System;
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

            if (Session["ConnectionString"] != null && Session["NM_FuncionarioLogin"] != null)
            {
                if (Session["DS_NivelAcesso"].ToString().Equals("Gerente"))
                {
                    if (!IsPostBack)
                    {
                        LimparItensVenda();
                        LimparFiltro();
                        LimparCamposVenda();
                        CarregarDatas();
                        CarregarVendas();
                        BloquearComponentesRelatorioVendas();
                        BloquearComponentesRelatorioVendasConsulta();

                        lblNM_FuncionarioLogin.Text = Session["NM_FuncionarioLogin"].ToString();
                    }
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

        private void LimparFiltro()
        {
            ddlDiaRelatorioInicio.SelectedIndex = 0;
            ddlMesRelatorioInicio.SelectedIndex = 0;
            ddlAnoRelatorioInicio.SelectedIndex = 0;

            ddlDiaRelatorioFinal.SelectedIndex = 0;
            ddlMesRelatorioFinal.SelectedIndex = 0;
            ddlAnoRelatorioFinal.SelectedIndex = 0;

            btnConsultar.Enabled = false;
        }

        private void LimparCamposVenda()
        {
            txbID_Venda.Text = "";
            lblVenda.Text = "";
            lblDS_Mensagem.Text = "";
        }

        private void LimparItensVenda()
        {
            gvwExibeItensVenda.DataSource = null;
            gvwExibeItensVenda.DataBind();

            lblQTD_Itens.Text = "Quantidade Itens: 0";
            lblVL_TotalVendaItem.Text = "Venda R$0,00";
            lblVL_TotalLucroItem.Text = "Lucro R$0,00";
        }

        private void BloquearComponentesRelatorioVendasConsulta()
        {
            btnConsultar.Enabled = false;
        }

        private void BloquearComponentesRelatorioVendas()
        {
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void CarregarVendas()
        {
            // instanciando um objeto da classe ControllerVenda
            myControllerVenda = new ControllerVenda();

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerVenda.Exibir(Session["ConnectionString"].ToString());

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();

            CalcularRelatorioVendas();
        }

        private void CarregarVendasConsultar()
        {
            // instanciar um objeto da classe venda, carregar tela e consultar
            myControllerVenda = new ControllerVenda();

            // criando a data de nascimento com datetime
            DateTime dataInicio = new DateTime(
                Convert.ToInt32(ddlAnoRelatorioInicio.SelectedValue),
                ddlMesRelatorioInicio.SelectedIndex,
                Convert.ToInt32(ddlDiaRelatorioInicio.SelectedValue));

            DateTime dataFinal = new DateTime(
                Convert.ToInt32(ddlAnoRelatorioFinal.SelectedValue),
                ddlMesRelatorioFinal.SelectedIndex,
                Convert.ToInt32(ddlDiaRelatorioFinal.SelectedValue));

            gvwExibe.DataSource = myControllerVenda.Consultar(dataInicio, dataFinal, Session["ConnectionString"].ToString());
            gvwExibe.DataBind();

            CalcularRelatorioVendas();
        }

        private void CarregarItensVenda(string id_venda)
        {
            // instanciando um objeto da classe ControllerItemVenda
            myControllerItemVenda = new ControllerItemVenda();

            // passando a fonte de dados para o GridView
            gvwExibeItensVenda.DataSource = myControllerItemVenda.Exibir(id_venda, Session["ConnectionString"].ToString());

            // associando os dados para carregar e exibir
            gvwExibeItensVenda.DataBind();

            CalcularRelatorioItensVenda();
        }

        private void CalcularRelatorioVendas()
        {
            int contadorVendas = 0;
            double valorTotalVendas = 0;
            double valorTotalLucro = 0;

            foreach (GridViewRow row in gvwExibe.Rows)
            {
                contadorVendas += row.Cells[6].Text.Equals("&nbsp;") ? 0 : 1;
                valorTotalVendas += row.Cells[6].Text.Equals("&nbsp;") ? 0 : Convert.ToDouble(row.Cells[6].Text);
                valorTotalLucro += row.Cells[7].Text.Equals("&nbsp;") ? 0 : Convert.ToDouble(row.Cells[7].Text);
            }

            lblQTD_VendasRealizadas.Text = "Vendas realizadas: " + contadorVendas.ToString();
            lblVL_TotalVendas.Text = "Vendas R$" + valorTotalVendas.ToString("N2");
            lblVL_TotalLucro.Text = "Lucro R$" + valorTotalLucro.ToString("N2");
        }

        private void CalcularRelatorioItensVenda()
        {
            double valorTotalVendaItem = 0;
            double valorTotalLucroItem = 0;

            foreach (GridViewRow row in gvwExibeItensVenda.Rows)
            {
                valorTotalVendaItem += row.Cells[10].Text.Equals(DBNull.Value) ? 0 : Convert.ToDouble(row.Cells[10].Text);
                valorTotalLucroItem += row.Cells[11].Text.Equals(DBNull.Value) ? 0 : Convert.ToDouble(row.Cells[11].Text);
            }

            lblQTD_Itens.Text = "Quantidade Itens: " + gvwExibeItensVenda.Rows.Count.ToString();
            lblVL_TotalVendaItem.Text = "Venda R$" + valorTotalVendaItem.ToString("N2");
            lblVL_TotalLucroItem.Text = "Lucro R$" + valorTotalLucroItem.ToString("N2");
        }

        private void CarregarDatas()
        {
            ddlDiaRelatorioInicio.Items.Insert(0, "Dia");
            ddlDiaRelatorioFinal.Items.Insert(0, "Dia");

            for (int i = 1; i <= 31; i++)
            {
                ddlDiaRelatorioInicio.Items.Insert(i, Convert.ToString(i));
                ddlDiaRelatorioFinal.Items.Insert(i, Convert.ToString(i));
            }

            ddlMesRelatorioInicio.Items.Insert(0, "Mês");
            ddlMesRelatorioFinal.Items.Insert(0, "Mês");
            ddlMesRelatorioInicio.Items.Insert(1, "Janeiro");
            ddlMesRelatorioFinal.Items.Insert(1, "Janeiro");
            ddlMesRelatorioInicio.Items.Insert(2, "Fevereiro");
            ddlMesRelatorioFinal.Items.Insert(2, "Fevereiro");
            ddlMesRelatorioInicio.Items.Insert(3, "Março");
            ddlMesRelatorioFinal.Items.Insert(3, "Março");
            ddlMesRelatorioInicio.Items.Insert(4, "Abril");
            ddlMesRelatorioFinal.Items.Insert(4, "Abril");
            ddlMesRelatorioInicio.Items.Insert(5, "Maio");
            ddlMesRelatorioFinal.Items.Insert(5, "Maio");
            ddlMesRelatorioInicio.Items.Insert(6, "Junho");
            ddlMesRelatorioFinal.Items.Insert(6, "Junho");
            ddlMesRelatorioInicio.Items.Insert(7, "Julho");
            ddlMesRelatorioFinal.Items.Insert(7, "Julho");
            ddlMesRelatorioInicio.Items.Insert(8, "Agosto");
            ddlMesRelatorioFinal.Items.Insert(8, "Agosto");
            ddlMesRelatorioInicio.Items.Insert(9, "Setembro");
            ddlMesRelatorioFinal.Items.Insert(9, "Setembro");
            ddlMesRelatorioInicio.Items.Insert(10, "Outubro");
            ddlMesRelatorioFinal.Items.Insert(10, "Outubro");
            ddlMesRelatorioInicio.Items.Insert(11, "Novembro");
            ddlMesRelatorioFinal.Items.Insert(11, "Novembro");
            ddlMesRelatorioInicio.Items.Insert(12, "Dezembro");
            ddlMesRelatorioFinal.Items.Insert(12, "Dezembro");

            ddlMesRelatorioInicio.SelectedIndex = 0;
            ddlMesRelatorioFinal.SelectedIndex = 0;

            ddlAnoRelatorioInicio.Items.Insert(0, "Ano");
            ddlAnoRelatorioFinal.Items.Insert(0, "Ano");

            for (int i = 2020; i <= DateTime.Today.Year; i++)
            {
                ddlAnoRelatorioInicio.Items.Add(Convert.ToString(i));
                ddlAnoRelatorioFinal.Items.Add(Convert.ToString(i));
            }

            ddlAnoRelatorioInicio.SelectedIndex = 0;
            ddlAnoRelatorioFinal.SelectedIndex = 0;
        }

        private void SearchFields()
        {
            btnConsultar.Enabled =
                ddlDiaRelatorioInicio.SelectedIndex != 0 &&
                ddlMesRelatorioInicio.SelectedIndex != 0 &&
                ddlAnoRelatorioInicio.SelectedIndex != 0 &&
                ddlDiaRelatorioFinal.SelectedIndex != 0 &&
                ddlMesRelatorioFinal.SelectedIndex != 0 &&
                ddlAnoRelatorioFinal.SelectedIndex != 0;

            btnLimpar.Enabled =
                ddlDiaRelatorioInicio.SelectedIndex != 0 ||
                ddlMesRelatorioInicio.SelectedIndex != 0 ||
                ddlAnoRelatorioInicio.SelectedIndex != 0 ||
                ddlDiaRelatorioFinal.SelectedIndex != 0 ||
                ddlMesRelatorioFinal.SelectedIndex != 0 ||
                ddlAnoRelatorioFinal.SelectedIndex != 0;
        }

        private void Excluir()
        {
            // instanciar um objeto da classe venda e carregar tela e excluir
            myControllerVenda = new ControllerVenda(txbID_Venda.Text.Trim(), Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerVenda.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCamposVenda();
                CarregarVendas();
                LimparItensVenda();
                BloquearComponentesRelatorioVendas();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerVenda.DS_Mensagem;
            }
        }

        protected void ddlDiaRelatorioInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchFields();
        }

        protected void ddlMesRelatorioInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchFields();
        }

        protected void ddlAnoRelatorioInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchFields();
        }

        protected void ddlDiaRelatorioFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchFields();
        }

        protected void ddlMesRelatorioFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchFields();
        }

        protected void ddlAnoRelatorioFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchFields();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            LimparCamposVenda();
            LimparItensVenda();
            CarregarVendasConsultar();
        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "Funcionário";
                e.Row.Cells[3].Text = "Data";
                e.Row.Cells[4].Text = "Tipo de Pagamento";
                e.Row.Cells[5].Text = "Nº Parcelas";
                e.Row.Cells[6].Text = "Valor Venda";
                e.Row.Cells[7].Text = "Valor Lucro";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDS_Mensagem.Text = "";

            txbID_Venda.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());
            lblVenda.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[4].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[5].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[6].Text.Trim()) + " ➯ " +
                Server.HtmlDecode(gvwExibe.SelectedRow.Cells[7].Text.Trim());

            try
            {
                CarregarItensVenda(txbID_Venda.Text.Trim());
            }
            catch (Exception ex)
            {
                lblDS_Mensagem.Text = ex.Message + ex.StackTrace;
            }

            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            CarregarVendas();
        }

        protected void gvwExibeItensVenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[7].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "ID";
                e.Row.Cells[3].Text = "EAN";
                e.Row.Cells[4].Text = "Produto";
                e.Row.Cells[5].Text = "Marca";
                e.Row.Cells[6].Text = "Sabor";
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Text = "Preço";
                e.Row.Cells[9].Text = "Quantidade";
                e.Row.Cells[10].Text = "Subtotal";
                e.Row.Cells[11].Text = "Lucro";
            }
        }

        protected void gvwExibeItensVenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibeItensVenda.PageIndex = e.NewPageIndex;
            CarregarItensVenda(txbID_Venda.Text.Trim());
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCamposVenda();
            LimparItensVenda();
            BloquearComponentesRelatorioVendas();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        protected void btnLimparFiltro_Click(object sender, EventArgs e)
        {
            LimparFiltro();
            CarregarVendas();
        }
    }
}