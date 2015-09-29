<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="模拟投递系统.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        html,body{
            height:100%;
            width:100%;
        }
        #login {
            position: fixed;
            top: 50%;
            left: 40%;
            width: 400px;
            height: 200px;
        }

        #Label1 {
            position: absolute;
            left: 60px;
            font-size:20px;
        }

        body {
            background-color: #777;
            color: #fff;
            font-family: 微软雅黑;
            font-weight: 900;
        }
        #div_name{
            position:absolute;
            top:50px;
        }
        #div_password{
            position:absolute;
            top:80px;
        }
        #TextBox1{
            width:150px;
            height:20px;
            border:1px solid #999;
        }
        #TextBox2{
            width:150px;
            height:20px;
            border:1px solid #999;
        }
        #Button1{
            position:absolute;
            top:120px;
            left:100px;
            font-family:微软雅黑;
            border:1px solid #999;
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="login">

            <asp:Label ID="Label1" runat="server" Text="模拟投递子系统" ToolTip="ju"></asp:Label>

            <div id="div_name">
                <asp:Label ID="Label2" runat="server" Text="用户名" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="用户名或密码错误" Visible="False"></asp:Label>
            </div>
            <div id="div_password">
                <asp:Label ID="Label3" runat="server" Text="密码" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登录" />
            <br />
        </div>
    </form>
</body>
</html>
