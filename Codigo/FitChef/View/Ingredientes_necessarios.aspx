<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ingredientes_necessarios.aspx.cs" Inherits="FitChef.View.Ingredientes_necessarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body background="../imgs/frutosSimples.jpg">
    <form id="form1" runat="server">
        <div >
            <p style="height: 185px">
            </p>
            <p style="font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif; font-size: large; font-weight: bold; text-align: center;">
                Necessary ingredients</p>
            <p style="text-align: center">
                <asp:ListBox ID="ListBox1" runat="server" BackColor="#FFFF99" Height="313px" Width="563px"></asp:ListBox>
            </p>
            
            <a href="BingMap.aspx" > 
                <img src="../imgs/map-icon2.png" alt="HTML tutorial" style="width:42px;height:42px;border:0;" class="center"/>
            </a>
        </div>
    </form>
</body>
</html>
