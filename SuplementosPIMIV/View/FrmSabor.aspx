<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmSabor.aspx.cs" Inherits="SuplementosPIMIV.View.FrmSabor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Cadastro de Sabores</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="formSabor" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

        <!-- Menu -------------------------------------- -->
        <ul class="menu">
            <li><a href="FrmMenuPrincipal.aspx">Menu</a></li>
            <li><a href="">PDV</a></li>
            <li><a href="FrmProduto.aspx">Produtos</a></li>
            <li><a href="FrmSabor.aspx">Sabores</a></li>
            <li><a href="FrmSubcategoria.aspx">Subcategorias</a></li>
            <li><a href="FrmCategoria.aspx">Categorias</a></li>
        </ul>

        <div class="conteiner">

            <div id="cadastroSabor">

                <div id="cadInternoSabor">

                    <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Cadastro de Sabores"></asp:Label>

                    <br />

                    <asp:UpdatePanel ID="updCadastro" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbID_Sabor" Visible="false" runat="server"></asp:TextBox>

                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nome"></asp:Label>
                            <asp:TextBox CssClass="TextBox" ID="txbNM_Sabor" runat="server" MaxLengh="50"
                                placeholder="Nome do Sabor" OnTextChanged="txbNM_Sabor_TextChanged" AutoPostBack="true"></asp:TextBox>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimparSabor" runat="server" Text="Limpar" OnClick="btnLimparSabor_Click" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />
            </div>

            <br />

            <div id="exibeSabor">
                <div id="exibeInternoSabor">

                    <br />

                    <asp:UpdatePanel ID="updConsulta" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbNM_SaborConsultar" MaxLengh="50"
                                placeholder="Buscar Sabor" runat="server" OnTextChanged="txbNM_SaborConsultar_TextChanged"
                                AutoPostBack="true"></asp:TextBox>

                            <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar"
                                OnClick="btnConsultar_Click" />

                            <br />

                            <asp:GridView ID="gvwSabor" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwSabor_RowDataBound"
                                OnSelectedIndexChanged="gvwSabor_SelectedIndexChanged">
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

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <br />
                <br />

            </div>

            <br />

        </div>
    </form>
</body>
</html>
