<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Altear_Perfil.aspx.cs" Inherits="FitChef.View.Altear_Perfil" %>

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
            <p style="height: 151px">
            </p>
            <p class="auto-style1" style="font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif; font-weight: bold; font-size: large; height: 46px;">
                Change account information&nbsp;&nbsp;
            </p>
            <p style="text-align: center">
                New username:</p>
            <p style="text-align: center">
                <asp:TextBox ID="TextBox1" runat="server" Width="278px"></asp:TextBox>
            </p>
            <p style="text-align: center">
                New password:</p>
            <p style="text-align: center">
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" Width="282px"></asp:TextBox>
            </p>
            <p style="height: 20px; text-align: center;">
                Change preferences?</p>
            <p style="height: 34px; text-align: center;">
                <asp:Button ID="Button1" runat="server" Height="34px" OnClick="Button1_Click" Text="Yes" Width="86px" />
            </p>
            <p style="height: 30px">
            </p>
            <p style="text-align: center">
                Save changes?</p>
            <p style="text-align: center">
                <asp:ImageButton ID="ImageButton1" runat="server" Height="47px" ImageUrl="~/imgs/save.png" Width="55px" OnClick="ImageButton1_Click" />
            </p>
        </div>
    </form>
</body>
</html>
