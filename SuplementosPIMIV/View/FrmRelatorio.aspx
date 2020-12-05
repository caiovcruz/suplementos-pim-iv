<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmRelatorio.aspx.cs" Inherits="SuplementosPIMIV.View.FrmRelatorio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Relatórios</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formRelatorio" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

        <!-- Menu -------------------------------------- -->
        <div class="navbar">
            <div class="dropdown">
                <asp:LinkButton CssClass="dropbtn" ID="lbtnMeuLogin" Font-Underline="false" runat="server">
                    <i class="fas fa-user-circle fa-lg" style="margin-right: 2px;"></i>
                    <asp:Label ID="lblNM_FuncionarioLogin" runat="server"></asp:Label>
                </asp:LinkButton>
                <div class="dropdown-content">
                    <a href="FrmMeuLogin.aspx">Meu Login</a>
                    <asp:LinkButton ID="lbtnSair" Font-Underline="false" runat="server" PostBackUrl="~/View/FrmLogin.aspx">
                   <i class="fas fa-sign-out-alt fa-lg" style="margin-right: 2px;"></i>Sair</asp:LinkButton>
                </div>
            </div>
            <a href="FrmPDV.aspx"><i class="fas fa-store-alt" style="margin-right: 5px;"></i>PDV</a>
            <div class="dropdown">
                <asp:LinkButton CssClass="dropbtn" ID="lbtnDropProdutos" Font-Underline="false" runat="server">
                    <i class="fas fa-tablets" style="margin-right: 2px;"></i> Produtos <i class="fa fa-caret-down"></i></asp:LinkButton>
                <div class="dropdown-content">
                    <a href="FrmProduto.aspx">Produtos</a>
                    <a href="FrmMovEstoque.aspx">Mov.Estoques</a>
                    <a href="FrmMarca.aspx">Marcas</a>
                    <a href="FrmSabor.aspx">Sabores</a>
                    <a href="FrmSubcategoria.aspx">Subcategorias</a>
                    <a href="FrmCategoria.aspx">Categorias</a>
                </div>
            </div>
            <div class="dropdown">
                <asp:LinkButton CssClass="dropbtn" ID="lbtnDropFuncionarios" Font-Underline="false" runat="server">
                   <i class="fas fa-user-tie fa-sm" style="margin-right: 2px;"></i> Funcionários <i class="fa fa-caret-down"></i></asp:LinkButton>
                <div class="dropdown-content">
                    <a href="FrmFuncionario.aspx">Funcionários</a>
                    <a href="FrmCadastroLogin.aspx">Cadastro de Login</a>
                </div>
            </div>
            <asp:LinkButton CssClass="dropbtn" ID="lbtnRelatorios" Font-Underline="false" runat="server" PostBackUrl="~/View/FrmRelatorio.aspx">
                   <i class="fas fa-chart-line fa-sm" style="margin-right: 2px;"></i> Relatórios </asp:LinkButton>
        </div>

        <asp:UpdatePanel ID="updExibe" runat="server">
            <ContentTemplate>

                <div class="exibeRelatorio">
                    <div class="exibeInternoRelatorio">

                        <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Vendas"></asp:Label>

                        <br />

                        <div id="internoColunasRelatorio">

                            <div>

                                <asp:Label CssClass="Label" runat="server" Width="100%"
                                    Text="Data Início"></asp:Label>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlDiaRelatorioInicio"
                                    runat="server" OnSelectedIndexChanged="ddlDiaRelatorioInicio_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlMesRelatorioInicio"
                                    runat="server" OnSelectedIndexChanged="ddlMesRelatorioInicio_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlAnoRelatorioInicio"
                                    runat="server" OnSelectedIndexChanged="ddlAnoRelatorioInicio_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                            <div class="coluna">

                                <asp:Label CssClass="Label" runat="server" Width="100%"
                                    Text="Data Final"></asp:Label>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlDiaRelatorioFinal"
                                    runat="server" OnSelectedIndexChanged="ddlDiaRelatorioFinal_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlMesRelatorioFinal"
                                    runat="server" OnSelectedIndexChanged="ddlMesRelatorioFinal_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlAnoRelatorioFinal"
                                    runat="server" OnSelectedIndexChanged="ddlAnoRelatorioFinal_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                            <div class="coluna">

                                <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar"
                                    OnClick="btnConsultar_Click" />

                            </div>

                            <div class="coluna">

                                <asp:LinkButton CssClass="Button" ID="lbtnLimparFiltro" Font-Underline="false" runat="server" OnClick="btnLimparFiltro_Click">
                   <i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>

                            </div>

                        </div>

                        <br />

                        <div class="internoColunasRelatorio2">

                            <div>

                                <i class="fas fa-check-square"></i>
                                <asp:Label CssClass="Label" ID="lblQTD_VendasRealizadas" runat="server" Text="Vendas realizadas: "></asp:Label>

                            </div>

                            <div class="coluna">

                                <i class="fas fa-file-invoice-dollar" style="color: green;"></i>
                                <asp:Label CssClass="Label" ID="lblVL_TotalVendas" runat="server" Text="Vendas R$"></asp:Label>

                            </div>

                            <div class="coluna">

                                <i class="fas fa-funnel-dollar" style="color: blue;"></i>
                                <asp:Label CssClass="Label" ID="lblVL_TotalLucro" runat="server" Text="Lucro R$"></asp:Label>

                            </div>

                        </div>

                        <br />

                        <div style="overflow: auto;">
                            <asp:GridView CssClass="gvwExibe" ID="gvwExibe" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwExibe_RowDataBound"
                                OnSelectedIndexChanged="gvwExibe_SelectedIndexChanged" OnPageIndexChanging="gvwExibe_PageIndexChanging" 
                                on AllowPaging="true" PageSize="10">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbSelecionar" runat="server" CausesValidation="False"
                                                CommandName="Select" Style="display: none;"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="WhiteSmoke" BorderStyle="Solid" BorderColor="Black" Font-Bold="True" />
                            </asp:GridView>
                        </div>

                        <br />

                        <asp:TextBox CssClass="TextBox" ID="txbID_Venda" runat="server" Visible="false"></asp:TextBox>
                        <asp:Label CssClass="Label" ID="lblVenda" runat="server" MaxLengh="150"></asp:Label>

                        <br />

                        <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                        <br />
                        <br />

                        <asp:Button CssClass="Button" ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                        <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />

                    </div>

                    <br />
                    <br />

                </div>

                <br />
                <br />

                <div class="exibeRelatorio">
                    <div class="exibeInternoRelatorio">

                        <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Itens da Venda"></asp:Label>

                        <br />

                        <div class="internoColunasRelatorio2">

                            <div>

                                <i class="fas fa-boxes"></i>
                                <asp:Label CssClass="Label" ID="lblQTD_Itens" runat="server" Text="Quantidade Itens: 0"></asp:Label>

                            </div>

                            <div class="coluna">

                                <i class="fas fa-file-invoice-dollar" style="color: green;"></i>
                                <asp:Label CssClass="Label" ID="lblVL_TotalVendaItem" runat="server" Text="Venda R$0,00"></asp:Label>

                            </div>

                            <div class="coluna">

                                <i class="fas fa-funnel-dollar" style="color: blue;"></i>
                                <asp:Label CssClass="Label" ID="lblVL_TotalLucroItem" runat="server" Text="Lucro R$0,00"></asp:Label>

                            </div>

                        </div>

                        <br />

                        <div style="overflow: auto;">
                            <asp:GridView CssClass="gvwExibe" ID="gvwExibeItensVenda" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwExibeItensVenda_RowDataBound"
                                OnPageIndexChanging="gvwExibeItensVenda_PageIndexChanging" AllowPaging="true" PageSize="10">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbSelecionar" runat="server" CausesValidation="False"
                                                CommandName="Select" Style="display: none;"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="WhiteSmoke" BorderStyle="Solid" BorderColor="Black" Font-Bold="True" />
                            </asp:GridView>
                        </div>

                        <br />
                        <br />

                    </div>

                    <br />
                    <br />

                </div>

                <br />
                <br />

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
