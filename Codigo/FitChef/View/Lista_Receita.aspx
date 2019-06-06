<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lista_Receita.aspx.cs" Inherits="FitChef.View.Lista_Receita" %>

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
            <p style="height: 172px">
            </p>
            <p class="auto-style1" style="font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif; font-weight: bold; font-size: large;">
                All available recipes</p>
            <p style="text-align: center">
                <asp:ListBox ID="ListBox1" runat="server" BackColor="#FFFF99" Height="348px" Width="651px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
            </p>
            <p style="text-align: center">
                <asp:Button ID="Button1" runat="server" Height="34px" Text="Cook the selected recipe" Width="231px" OnClick="Button1_Click" />
            </p>
            <p>
            </p>
            <p>
            </p>
        </div>
    </form>
</body>
</html>
