<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMovEstoque.aspx.cs" Inherits="SuplementosPIMIV.View.FrmEstoque" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Movimentação de Estoques</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formEstoque" runat="server">
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
            <a href="FrmFuncionario.aspx">Funcionários</a>
        </div>

        <div class="conteiner">

            <div id="cadastroMovEstoque">

                <div id="cadInternoMovEstoque">

                    <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Movimentação de Estoques"></asp:Label>

                    <br />

                    <asp:UpdatePanel ID="updCadastro" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbID_MovimentacaoEstoque" Visible="false" runat="server"></asp:TextBox>

                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Produto"></asp:Label>

                            <br />

                            <asp:DropDownList CssClass="DropDownList" ID="ddlID_ProdutoMovimentacaoEstoque" runat="server"
                                OnSelectedIndexChanged="ddlID_ProdutoMovimentacaoEstoque_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                            <br />

                            <div id="internoColunasMovEstoque">

                                <div class="colunasMovEstoque" id="colunaMovEstoque1">

                                    <asp:Label CssClass="Label" runat="server" Width="24%" Text="Quantidade"></asp:Label>

                                    <br />

                                    <asp:TextBox CssClass="TextBox" ID="txbQTD_MovimentacaoEstoque" runat="server" MaxLengh="50"
                                        placeholder="Quantidade" OnTextChanged="txbQTD_MovimentacaoEstoque_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>

                                </div>

                                <div class="colunasMovEstoque" id="colunaMovEstoque2">

                                    <asp:Label CssClass="Label" runat="server" Width="24%" Text="Movimentação"></asp:Label>

                                    <br />

                                    <asp:DropDownList CssClass="DropDownList" ID="ddlDS_MovimentacaoEstoque" runat="server"
                                        OnSelectedIndexChanged="ddlDS_MovimentacaoEstoque_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>

                                </div>

                                <div class="colunasMovEstoque" id="colunaMovEstoque3">

                                    <asp:Label CssClass="Label" runat="server" Width="24%" Text="Data"></asp:Label>

                                    <br />

                                    <asp:TextBox CssClass="TextBox" ID="txbDT_MovimentacaoEstoque" runat="server" AutoPostBack="true"></asp:TextBox>

                                </div>

                            </div>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />
            </div>

            <br />

            <div id="exibeMovEstoque">
                <div id="exibeInternoMovEstoque">

                    <br />

                    <asp:UpdatePanel ID="updConsulta" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbNM_ProdutoConsultar" MaxLengh="50"
                                placeholder="Buscar produto" runat="server" OnTextChanged="txbNM_ProdutoConsultar_TextChanged"
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
