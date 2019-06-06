<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dificuldades_Encontradas.aspx.cs" Inherits="FitChef.View.Dificuldades_Encontradas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body background="../imgs/frutosSimples.jpg">
    <form id="form1" runat="server">
        <div >
            <p style="height: 161px">
            </p>
            <p style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-weight: bold; text-align: center;">
                Difficulties encountred while cooking</p>
            <p style="text-align: center">
                <asp:ListBox ID="ListBox1" runat="server" BackColor="#FFFFCC" Height="215px" Width="473px"></asp:ListBox>
            </p>
            <p>
            </p>
        </div>
    </form>
</body>
</html>
