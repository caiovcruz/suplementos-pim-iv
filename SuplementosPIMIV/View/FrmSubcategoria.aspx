<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmSubcategoria.aspx.cs" Inherits="SuplementosPIMIV.View.FrmSubcategoria" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Cadastro de Subcategorias</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formSubcategoria" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

        <!-- Menu -------------------------------------- -->
        <div class="navbar">
            <a href="FrmMenuPrincipal.aspx">Menu</a>
            <a href="">PDV</a>
            <div class="dropdown">
                <button class="dropbtn">
                    Produtos
                    <i class="fa fa-caret-down"></i>
                </button>
                <div class="dropdown-content">
                    <a href="FrmProduto.aspx">Produtos</a>
                    <a href="FrmMovEstoque.aspx">Mov.Estoques</a>
                    <a href="FrmMarca.aspx">Marcas</a>
                    <a href="FrmSabor.aspx">Sabores</a>
                    <a href="FrmSubcategoria.aspx">Subcategorias</a>
                    <a href="FrmCategoria.aspx">Categorias</a>
                </div>
            </div>
        </div>

        <div class="conteiner">

            <div id="cadastroSubcategoria">

                <div id="cadInternoSubcategoria">

                    <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Cadastro de Subcategorias"></asp:Label>

                    <br />

                    <asp:UpdatePanel ID="updCadastro" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbID_Subcategoria" Visible="false" runat="server"></asp:TextBox>

                            <asp:Label CssClass="Label" runat="server" Width="77%" Text="Nome"></asp:Label>
                            <asp:Label CssClass="Label" runat="server" Width="20%" Text="Categoria base"></asp:Label>

                            <asp:TextBox CssClass="TextBox" ID="txbNM_Subcategoria" runat="server" MaxLengh="50"
                                placeholder="Nome da subcategoria" OnTextChanged="txbNM_Subcategoria_TextChanged"
                                AutoPostBack="true"></asp:TextBox>

                            <asp:DropDownList CssClass="TextBox" ID="ddlID_Categoria" runat="server"
                                OnSelectedIndexChanged="ddlID_Categoria_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                            <br />

                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Descrição"></asp:Label>
                            <asp:TextBox CssClass="TextBox" ID="txbDS_Subcategoria" runat="server" MaxLengh="1500"
                                TextMode="MultiLine" Wrap="true" placeholder="Descrição da subcategoria"
                                OnTextChanged="txbDS_Subcategoria_TextChanged" AutoPostBack="true"></asp:TextBox>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAtivarStatus" runat="server" Text="Ativar" OnClick="btnAtivarStatus_Click" />

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

                            <asp:CheckBox ID="chkStatusInativo" runat="server" Text="Inativas" AutoPostBack="true" OnCheckedChanged="chkStatusInativo_CheckedChanged" />

                            <asp:TextBox CssClass="TextBox" ID="txbNM_SubcategoriaConsultar" MaxLengh="50"
                                placeholder="Buscar subcategoria" runat="server" OnTextChanged="txbNM_SubcategoriaConsultar_TextChanged"
                                AutoPostBack="true"></asp:TextBox>

                            <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar"
                                OnClick="btnConsultar_Click" />

                            <br />

                            <asp:GridView CssClass="gvwExibe" ID="gvwExibe" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwExibe_RowDataBound"
                                OnSelectedIndexChanged="gvwExibe_SelectedIndexChanged" AllowPaging="true" PageSize="10">
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
