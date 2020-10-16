<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMenuPrincipal.aspx.cs" Inherits="SuplementosPIMIV.View.FrmMenuPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Menu Principal</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="formMenuPrincipal" runat="server">

        <!-- Menu -------------------------------------- -->
        <ul class="menu">
            <li><a href="FrmMenuPrincipal.aspx">Menu</a></li>
            <li><a href="">PDV</a></li>
            <li><a href="FrmProduto.aspx">Produtos</a></li>
            <li><a href="FrmSabor.aspx">Sabores</a></li>
            <li><a href="FrmSubcategoria.aspx">Subcategorias</a></li>
            <li><a href="FrmCategoria.aspx">Categorias</a></li>
        </ul>

        <br />

        <!-- Menu -------------------------------------- -->
        <div class="conteudo"></div>

    </form>
</body>
</html>
