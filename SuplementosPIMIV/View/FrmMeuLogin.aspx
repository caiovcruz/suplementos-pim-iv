<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMeuLogin.aspx.cs" Inherits="SuplementosPIMIV.View.FrmMeuLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Meu login</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formMeuLogin" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

        <!-- Menu -------------------------------------- -->
        <div class="navbar">
            <div class="dropdown">
                <asp:LinkButton CssClass="dropbtn" ID="lbtnMeuLogin" Font-Underline="false" runat="server" PostBackUrl="~/View/FrmMeuLogin.aspx">
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

        <div id="cadastroMeuLogin">

            <div id="cadInternoMeuLogin">

                <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Meu Login"></asp:Label>

                <br />

                <asp:TextBox CssClass="TextBox" ID="txbID_Login" Visible="false" runat="server"></asp:TextBox>

                <asp:Label CssClass="Label" ID="lblDS_UsuarioMeuLogin" runat="server" Width="100%" Text="Usuário"></asp:Label>
                <asp:TextBox CssClass="TextBox" ID="txbDS_UsuarioMeuLogin" runat="server" MaxLengh="20"
                    placeholder="Nome de usuário"></asp:TextBox>

                <br />

                <asp:UpdatePanel ID="updSenha" runat="server">
                    <ContentTemplate>

                        <asp:Label CssClass="Label" ID="lblDS_SenhaMeuLogin" runat="server" Width="100%" Text="Senha"></asp:Label>
                        <asp:TextBox CssClass="TextBox" ID="txbDS_SenhaMeuLoginMascara" runat="server" MaxLengh="20"
                            placeholder="Senha do usuário">
                        </asp:TextBox>
                        <asp:TextBox CssClass="TextBox" ID="txbDS_SenhaMeuLogin" runat="server" MaxLengh="20"
                            placeholder="Senha do usuário">
                        </asp:TextBox>

                        <br />

                        <asp:LinkButton CssClass="dropbtn" ID="lbtnVisualizarSenha" Font-Underline="false" OnClick="lbtnVisualizarSenha_Click" ToolTip="Visualizar Senha" runat="server">
                        <i class="far fa-eye fa-lg" style="margin-right: 2px;"></i>
                        </asp:LinkButton>
                        <asp:LinkButton CssClass="dropbtn" ID="lbtnMascararSenha" Font-Underline="false" OnClick="lbtnMascararSenha_Click" ToolTip="Mascarar Senha" runat="server">
                        <i class="far fa-eye-slash fa-lg" style="margin-right: 2px;"></i>
                        </asp:LinkButton>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <br />

                <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                <br />
                <br />

                <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                <asp:Button CssClass="Button" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />

            </div>

        </div>
    </form>
</body>
</html>
