<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLogin.aspx.cs" Inherits="SuplementosPIMIV.View.FrmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Iniciar Sessão</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />

</head>
<body>
    <form id="formLogin" runat="server">
        <div id="Login">

            <div id="logInterno">

                <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Iniciar Sessão"></asp:Label>

                <br />
                <br />

                <asp:TextBox CssClass="TextBox" ID="txbDS_Usuario" MaxLength="20" placeholder="Usuário" runat="server"></asp:TextBox>

                <br />

                <asp:TextBox CssClass="TextBox" ID="txbDS_Senha" TextMode="Password" MaxLength="20"
                    placeholder="Senha" runat="server"></asp:TextBox>

                <br />
                <br />

                <asp:Button CssClass="Button" ID="btnAcessar" runat="server" Text="Login" OnClick="btnAcessar_Click" />

                <br />
                <br />

                <div>
                    <asp:Label CssClass="Msg" ID="lblDS_Msg" runat="server" Text=""></asp:Label>
                </div>

                <br />

            </div>
        </div>
    </form>
</body>
</html>
