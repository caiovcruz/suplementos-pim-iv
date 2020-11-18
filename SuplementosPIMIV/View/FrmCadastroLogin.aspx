<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCadastroLogin.aspx.cs" Inherits="SuplementosPIMIV.View.FrmCadastroLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Cadastro de login</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formCadastroLogin" runat="server">
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
            <div class="dropdown">
                <button class="dropbtn">
                    Funcionários
                    <i class="fa fa-caret-down"></i>
                </button>
                <div class="dropdown-content">
                    <a href="FrmFuncionario.aspx">Funcionários</a>
                    <a href="FrmCadastroLogin.aspx">Cadastro de Login</a>
                </div>
            </div>
        </div>

        <div class="conteiner">

            <div id="cadastroLogin">

                <div id="cadInternoLogin">

                    <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Cadastro de Login"></asp:Label>

                    <br />

                    <asp:UpdatePanel ID="updCadastro" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbID_Login" Visible="false" runat="server"></asp:TextBox>

                            <div id="internoColunasLogin">

                                <div class="colunasLogin" id="colunaLogin1">
                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Funcionário"></asp:Label>
                                    <asp:DropDownList CssClass="DropDownList" ID="ddlID_Funcionario" runat="server"
                                        OnSelectedIndexChanged="ddlID_Funcionario_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="colunasLogin" id="colunaLogin2">
                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Usuário"></asp:Label>
                                    <asp:TextBox CssClass="TextBox" ID="txbDS_Usuario" runat="server" MaxLengh="20"
                                        placeholder="Nome de usuário" OnTextChanged="txbDS_Usuario_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>

                                <div class="colunasLogin" id="colunaLogin3">
                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nível de Acesso"></asp:Label>
                                    <asp:DropDownList CssClass="DropDownList" ID="ddlID_NivelAcesso" runat="server"
                                        OnSelectedIndexChanged="ddlID_NivelAcesso_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="colunasLogin" id="colunaLogin4">
                                    <asp:Label CssClass="Label" runat="server" Width="100%" Text="Senha"></asp:Label>
                                    <asp:TextBox CssClass="TextBox" ID="txbDS_Senha" runat="server" MaxLengh="20"
                                        placeholder="Senha do usuário" OnTextChanged="txbDS_Senha_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>

                            </div>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />
            </div>

            <br />
            <br />
            <br />

            <div id="exibeLogin">
                <div id="exibeInternoLogin">

                    <br />

                    <asp:UpdatePanel ID="updConsulta" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbNM_FuncionarioLoginConsultar" MaxLengh="50"
                                placeholder="Buscar funcionário" runat="server" OnTextChanged="txbNM_FuncionarioLoginConsultar_TextChanged"
                                AutoPostBack="true"></asp:TextBox>

                            <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar"
                                OnClick="btnConsultar_Click" />

                            <br />

                            <div style="overflow: auto;">
                                <asp:GridView CssClass="gvwExibe" ID="gvwExibe" runat="server" CellPadding="5" GridLines="Horizontal"
                                    AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwExibe_RowDataBound"
                                    OnSelectedIndexChanged="gvwExibe_SelectedIndexChanged" OnPageIndexChanging="gvwExibe_PageIndexChanging" AllowPaging="true"
                                    PageSize="10">
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

        </div>
    </form>
</body>
</html>
