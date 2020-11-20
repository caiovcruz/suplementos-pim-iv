<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmPDV.aspx.cs" Inherits="SuplementosPIMIV.View.FrmPDV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>PDV Muscle Definition</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formPDV" runat="server">
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
        </div>
    </form>
</body>
</html>
