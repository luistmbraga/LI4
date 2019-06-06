<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historico_receitas.aspx.cs" Inherits="FitChef.View.Historico_receitas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body background="../imgs/frutosSimples.jpg">
    <form id="form1" runat="server">
        <div >
            <p style="height: 194px">
            </p>
            <p class="auto-style1" style="font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif; font-size: 22px; font-weight: bold;">
                Cooked Recipes&nbsp;
            </p>
            <p style="text-align: center">
                <asp:ListBox ID="ListBox1" runat="server" BackColor="#FFFFCC" Height="308px" style="margin-left: 0px" Width="580px"></asp:ListBox>
            </p>
            <p style="text-align: center">
                <asp:Button ID="Dific_Encontradas" runat="server" Height="36px" Text="Problems Encountered" Width="262px" OnClick="Dific_Encontradas_Click" />
            </p>
        </div>
    </form>
</body>
</html>
