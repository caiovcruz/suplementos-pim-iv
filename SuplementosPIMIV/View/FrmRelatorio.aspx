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
            <asp:LinkButton CssClass="dropbtn" ID="lbtnMeuLogin" Font-Underline="false" runat="server" PostBackUrl="~/View/FrmMeuLogin.aspx">
                <i class="fas fa-user-circle fa-lg" style="margin-right: 2px;"></i>
                <asp:Label ID="lblNM_FuncionarioLogin" runat="server"></asp:Label>
            </asp:LinkButton>
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

        <div id="exibeRelatorio">
            <div id="exibeInternoRelatorio">

                <br />

                <asp:UpdatePanel ID="updConsulta" runat="server">
                    <ContentTemplate>

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

                        </div>

                        <br />

                        <div style="overflow: auto;">
                            <asp:GridView CssClass="gvwExibe" ID="gvwExibe" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwExibe_RowDataBound"
                                OnSelectedIndexChanged="gvwExibe_SelectedIndexChanged" OnPageIndexChanging="gvwExibe_PageIndexChanging" AllowPaging="true" PageSize="10">
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

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <br />
            <br />

        </div>

        <br />
        <br />

    </form>
</body>
</html>
