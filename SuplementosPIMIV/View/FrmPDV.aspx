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
            <div class="dropdown">
                <asp:LinkButton CssClass="dropbtn" ID="lbtnMeuLogin" Font-Underline="false" runat="server">
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


        <asp:UpdatePanel ID="updCadastro" runat="server">
            <ContentTemplate>

                <div id="cadastroVendaProduto">

                    <div id="cadInternoVendaProduto">

                        <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Venda de Produtos"></asp:Label>

                        <br />

                        <asp:TextBox CssClass="TextBox" ID="txbID_Produto" Visible="false" runat="server"></asp:TextBox>

                        <asp:Label CssClass="Label" runat="server" Width="100%" Text="EAN"></asp:Label>
                        <asp:TextBox CssClass="TextBox" ID="txbNR_EAN" runat="server" MaxLengh="13"
                            placeholder="Escaneie o cód. barras" OnTextChanged="txbNR_EAN_TextChanged" AutoPostBack="true"></asp:TextBox>

                        <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />

                        <div id="internoColunasVendaProduto">

                            <div class="colunasVendaProduto">

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Produto"></asp:Label>
                                <asp:TextBox CssClass="TextBox" ID="txbProduto" runat="server" MaxLengh="13"
                                    placeholder="Produto" OnTextChanged="txbProduto_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>

                            <div class="colunasVendaProduto" id="colunaVendaProduto2">

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Preço"></asp:Label>
                                <asp:TextBox CssClass="TextBox" ID="txbPR_Produto" runat="server" MaxLengh="10"
                                    placeholder="Produto" OnTextChanged="txbPR_Produto_TextChanged" AutoPostBack="true"></asp:TextBox>

                                <asp:TextBox CssClass="TextBox" ID="txbVL_LucroProduto" runat="server" MaxLengh="10" Visible="false" Enabled="false"></asp:TextBox>

                            </div>

                            <div class="colunasVendaProduto" id="colunaVendaProduto3">

                                <asp:Label CssClass="Label" ID="lblQTD_Produto" runat="server" Width="100%" Text="Quantidade"></asp:Label>
                                <asp:TextBox CssClass="TextBox" ID="txbQTD_Produto" runat="server" MaxLengh="10"
                                    placeholder="Quantidade" OnTextChanged="txbQTD_Produto_TextChanged" AutoPostBack="true"></asp:TextBox>

                            </div>

                        </div>

                        <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                        <br />
                        <br />

                        <asp:Button CssClass="Button" ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                        <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />
                        <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                        <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />

                        <br />
                        <br />

                    </div>

                </div>

                <br />

                <div id="exibeVendaProduto">
                    <div id="exibeInternoVendaProduto">

                        <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Produtos na Venda"></asp:Label>

                        <br />

                        <div style="overflow: auto;">
                            <asp:GridView CssClass="gvwExibe" ID="gvwExibe" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwExibe_RowDataBound"
                                OnSelectedIndexChanged="gvwExibe_SelectedIndexChanged" OnPageIndexChanging="gvwExibe_PageIndexChanging"
                                AllowPaging="true" PageSize="10">
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

                        <br />

                        <div id="internoColunasVendaProdutoFinaliza">

                            <div>

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Data"></asp:Label>
                                <asp:TextBox CssClass="TextBox" ID="txbDT_Venda" runat="server" MaxLengh="13"
                                    placeholder="Data" OnTextChanged="txbDT_Venda_TextChanged" AutoPostBack="true"></asp:TextBox>

                            </div>

                            <div class="coluna">

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Tipo de Pagamento"></asp:Label>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlDS_TipoPagamento" runat="server"
                                    OnSelectedIndexChanged="ddlDS_TipoPagamento_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                            <div class="coluna">

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nº Parcelas"></asp:Label>
                                <asp:DropDownList CssClass="DropDownList" ID="ddlNR_Parcelas" runat="server"
                                    OnSelectedIndexChanged="ddlNR_Parcelas_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                            <div class="coluna">

                                <asp:Label CssClass="Label" runat="server" Width="100%" Text="Valor Total"></asp:Label>
                                <asp:TextBox CssClass="TextBox" ID="txbVL_Total" runat="server" MaxLengh="13"
                                    placeholder="Valor Total" OnTextChanged="txbVL_Total_TextChanged" AutoPostBack="true"></asp:TextBox>

                            </div>

                        </div>

                        <br />

                        <div id="valorRecebido">

                            <asp:Label CssClass="Label" ID="lblVL_Recebido" runat="server" Width="100%" Text="Valor Recebido"></asp:Label>
                            <asp:TextBox CssClass="TextBox" ID="txbVL_Recebido" runat="server" MaxLengh="13"
                                placeholder="Valor Recebido" OnTextChanged="txbVL_Recebido_TextChanged" AutoPostBack="true"></asp:TextBox>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_MensagemTroco" runat="server" Width="100%" Text="" Font-Bold="true"></asp:Label>

                        </div>

                        <br />
                        <br />

                        <asp:Label CssClass="Msg" ID="lblDS_MensagemFinal" runat="server" Width="100%" Text=""></asp:Label>

                        <br />
                        <br />

                        <asp:Button CssClass="Button" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                        <asp:Button CssClass="Button" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />

                        <br />
                        <br />

                    </div>
                </div>

                <br />
                <br />

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
