<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMenuPrincipal.aspx.cs" Inherits="SuplementosPIMIV.View.FrmMenuPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Menu Principal</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formMenuPrincipal" runat="server">

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

        <br />

        <!-- Menu -------------------------------------- -->
        <div class="conteudo"></div>

    </form>
</body>
</html>
