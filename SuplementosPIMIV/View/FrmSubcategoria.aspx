<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmSubcategoria.aspx.cs" Inherits="SuplementosPIMIV.View.FrmSubcategoria" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Cadastro de Subcategorias</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="formSubcategoria" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

        <!-- Menu -------------------------------------- -->
        <ul class="menu">
            <li><a href="FrmMenuPrincipal.aspx">Menu</a></li>
            <li><a href="">PDV</a></li>
            <li><a href="FrmProduto.aspx">Produto</a></li>
            <li><a href="FrmSubcategoria.aspx">Subcategoria</a></li>
            <li><a href="FrmCategoria.aspx">Categoria</a></li>
        </ul>

        <div class="conteiner">

            <div id="cadastroSubcategoria">

                <div id="cadInternoSubcategoria">

                    <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Cadastro de Subcategorias"></asp:Label>

                    <br />

                    <asp:UpdatePanel ID="updCadastro" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbID_Subcategoria" Visible="false" runat="server"></asp:TextBox>

                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Categoria base"></asp:Label>
                            <asp:DropDownList CssClass="TextBox" ID="ddlID_Categoria" runat="server"
                                OnSelectedIndexChanged="ddlID_Categoria_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nome"></asp:Label>
                            <asp:TextBox CssClass="TextBox" ID="txbNM_Subcategoria" runat="server" MaxLengh="50"
                                placeholder="Nome da subcategoria" OnTextChanged="txbNM_Subcategoria_TextChanged" AutoPostBack="true"></asp:TextBox>

                            <br />

                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Descrição"></asp:Label>
                            <asp:TextBox CssClass="TextBox" ID="txbDS_Subcategoria" runat="server" MaxLengh="1500"
                                TextMode="MultiLine" Wrap="true" placeholder="Descrição da subcategoria"
                                OnTextChanged="txbDS_Subcategoria_TextChanged" AutoPostBack="true"></asp:TextBox>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimparSubcategoria" runat="server" Text="Limpar" OnClick="btnLimparSubcategoria_Click" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />
            </div>

            <br />

            <div id="exibeSubcategoria">
                <div id="exibeInternoSubcategoria">

                    <br />

                    <asp:UpdatePanel ID="updConsulta" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbNM_SubcategoriaConsultar" MaxLengh="50"
                                placeholder="Buscar subcategoria" runat="server" OnTextChanged="txbNM_SubcategoriaConsultar_TextChanged"
                                AutoPostBack="true"></asp:TextBox>

                            <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar"
                                OnClick="btnConsultar_Click" />

                            <br />

                            <asp:GridView ID="gvwSubcategoria" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwSubcategoria_RowDataBound"
                                OnSelectedIndexChanged="gvwSubcategoria_SelectedIndexChanged">
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
