<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCategoria.aspx.cs" Inherits="SuplementosPIMIV.View.FrmCategoria" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Cadastro de Categorias</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="formCategoria" runat="server">
        <div>
            <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

            <!-- Menu -------------------------------------- -->
            <ul class="menu">
                <li><a href="FrmMenuPrincipal.aspx">Menu</a></li>
                <li><a href="">PDV</a></li>
                <li><a href="FrmProduto.aspx">Produto</a></li>
                <li><a href="FrmCategoria.aspx">Categoria</a></li>
            </ul>

            <div class="conteiner">

                <div id="cadastroCategoria">

                    <div id="cadInternoCategoria">

                        <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Cadastro de Categorias"></asp:Label>

                        <br />

                        <asp:UpdatePanel ID="updCadastro" runat="server">
                            <ContentTemplate>

                                <asp:TextBox CssClass="TextBox" ID="txbID_Categoria" Visible="false" runat="server"></asp:TextBox>

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nome"></asp:Label>
                                <asp:TextBox CssClass="TextBox" ID="txbNM_Categoria" runat="server" MaxLengh="50"
                                    placeholder="Nome da categoria" AutoPostBack="true"></asp:TextBox>

                                <br />

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Descrição"></asp:Label>
                                <asp:TextBox CssClass="TextBox" ID="txbDS_Categoria" runat="server" MaxLengh="1500"
                                    TextMode="MultiLine" Wrap="true" placeholder="Descrição da categoria"
                                    AutoPostBack="true"></asp:TextBox>

                                <br />

                                <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                                <br />
                                <br />

                                <asp:Button CssClass="Button" ID="btnLimparCategoria" runat="server" Text="Limpar" />
                                <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" />
                                <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" />
                                <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" />

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <br />
                </div>

                <br />

                <div id="exibeCategoria">
                    <div id="exibeInternoCategoria">

                        <br />

                        <asp:UpdatePanel ID="updConsulta" runat="server">
                            <ContentTemplate>

                                <asp:TextBox CssClass="TextBox" ID="txbNM_CategoriaConsultar" MaxLengh="50"
                                    placeholder="Buscar categoria" runat="server"
                                    AutoPostBack="true"></asp:TextBox>

                                <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar"
                                     />

                                <br />

                                <asp:GridView ID="gvwCategoria" runat="server" CellPadding="5" GridLines="Horizontal"
                                    AlternatingRowStyle-BackColor="WhiteSmoke">
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

        </div>
    </form>
</body>
</html>
