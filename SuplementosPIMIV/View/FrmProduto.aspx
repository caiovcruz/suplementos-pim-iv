<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmProduto.aspx.cs" Inherits="SuplementosPIMIV.View.FrmProduto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Cadastro de Produtos</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formProduto" runat="server">
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
                    <a href="FrmMarca.aspx">Marcas</a>
                    <a href="FrmSabor.aspx">Sabores</a>
                    <a href="FrmSubcategoria.aspx">Subcategorias</a>
                    <a href="FrmCategoria.aspx">Categorias</a>
                </div>
            </div>
        </div>

        <div class="conteiner">

            <div id="cadastroProduto">

                <div id="cadInternoProduto">

                    <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Cadastro de Produtos"></asp:Label>

                    <br />

                    <asp:UpdatePanel ID="updCadastro" runat="server">
                        <ContentTemplate>

                            <div id="internoColunasProduto">

                                <div class="colunasProduto">

                                    <asp:TextBox CssClass="TextBox" ID="txbID_Produto" Visible="false" runat="server"></asp:TextBox>

                                    <asp:Label CssClass="Label" runat="server" Width="63%" Text="Nome"></asp:Label>
                                    <asp:Label CssClass="Label" runat="server" Width="30%" Text="Marca"></asp:Label>

                                    <asp:TextBox CssClass="TextBox" ID="txbNM_Produto" runat="server" MaxLengh="50"
                                        placeholder="Nome do produto" OnTextChanged="txbNM_Produto_TextChanged" AutoPostBack="true"></asp:TextBox>

                                    <asp:DropDownList CssClass="TextBox" ID="ddlID_Marca" runat="server"
                                        OnSelectedIndexChanged="ddlID_Marca_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>

                                    <br />

                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Descrição"></asp:Label>
                                    <asp:TextBox CssClass="TextBox" ID="txbDS_Produto" runat="server" MaxLengh="3000"
                                        TextMode="MultiLine" Wrap="true" placeholder="Descrição do produto"
                                        OnTextChanged="txbDS_Produto_TextChanged" AutoPostBack="true"></asp:TextBox>

                                </div>

                                <div class="colunasProduto">

                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Categoria"></asp:Label>
                                    <asp:DropDownList CssClass="TextBox" ID="ddlID_Categoria" runat="server"
                                        OnSelectedIndexChanged="ddlID_Categoria_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>

                                    <br />

                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Sabor"></asp:Label>
                                    <asp:DropDownList CssClass="TextBox" ID="ddlID_Sabor" runat="server"
                                        OnSelectedIndexChanged="ddlID_Sabor_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>

                                    <br />

                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Preço de Custo"></asp:Label>
                                    <asp:TextBox CssClass="TextBox" ID="txbPR_Custo" runat="server" MaxLengh="10"
                                        placeholder="Preço de custo" OnTextChanged="txbPR_Custo_TextChanged" AutoPostBack="true"></asp:TextBox>

                                </div>

                                <div class="colunasProduto">
                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Subcategoria"></asp:Label>
                                    <asp:DropDownList CssClass="TextBox" ID="ddlID_Subcategoria" runat="server"
                                        OnSelectedIndexChanged="ddlID_Subcategoria_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>

                                    <br />

                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Estoque"></asp:Label>
                                    <asp:TextBox CssClass="TextBox" ID="txbQTD_Estoque" runat="server" MaxLengh="10"
                                        placeholder="Estoque" OnTextChanged="txbQTD_Estoque_TextChanged" AutoPostBack="true"></asp:TextBox>

                                    <br />

                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Preço de Venda"></asp:Label>
                                    <asp:TextBox CssClass="TextBox" ID="txbPR_Venda" runat="server" MaxLengh="10"
                                        placeholder="Preço de venda" OnTextChanged="txbPR_Venda_TextChanged" AutoPostBack="true"></asp:TextBox>

                                </div>

                            </div>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimparProduto" runat="server" Text="Limpar" OnClick="btnLimparProduto_Click" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />
            </div>

            <br />

            <div id="exibeProduto">
                <div id="exibeInternoProduto">

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
                                OnSelectedIndexChanged="gvwExibe_SelectedIndexChanged">
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
