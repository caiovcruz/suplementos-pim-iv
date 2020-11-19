<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLogin.aspx.cs" Inherits="SuplementosPIMIV.View.FrmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Iniciar Sessão</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />

</head>
<body>
    <form id="formLogin" runat="server">
                <div id="Login">

            <div id="InternoLogin">

                <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Iniciar Sessão"></asp:Label>

                <br />

                <asp:TextBox CssClass="TextBox" ID="txbID_Login" Visible="false" runat="server"></asp:TextBox>

                <asp:Label CssClass="Label" ID="lblDS_Usuario" runat="server" Width="100%" Text="Usuário"></asp:Label>
                <asp:TextBox CssClass="TextBox" ID="txbDS_Usuario" runat="server" MaxLengh="20"
                    placeholder="Nome de usuário"></asp:TextBox>

                <br />

                <asp:Label CssClass="Label" ID="lblDS_Senha" runat="server" Width="100%" Text="Senha"></asp:Label>
                <asp:TextBox CssClass="TextBox" ID="txbDS_Senha" runat="server" MaxLengh="20"
                    placeholder="Senha do usuário" TextMode="Password"></asp:TextBox>

                <br />

                <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                <br />
                <br />

                <asp:Button CssClass="Button" ID="btnAcessar" runat="server" Text="Login" OnClick="btnAcessar_Click" />

            </div>
            <br />
        </div>
    </form>
</body>
</html>
