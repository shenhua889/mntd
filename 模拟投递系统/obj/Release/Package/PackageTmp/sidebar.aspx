<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sidebar.aspx.cs" Inherits="模拟投递系统.sidebar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        #TreeView1{
            position:fixed;
            top:10px;
            left:10px;
        }
        body{
            position:fixed;
            margin:0;
            font-family: 微软雅黑;
            font-weight: 800;
            background-color: #f5f5f5;
            color: #333;
        }
        .treeview{
            background-color: #fff;
            margin: 5px;
            padding: 5px;
            height:30px;
            width:150px;
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged1" ShowExpandCollapse="False" NodeStyle-CssClass="treeview" ForeColor="#669999" NodeStyle-Width="150px" NodeWrap="True">
                <NodeStyle BackColor="WhiteSmoke" Font-Names="微软雅黑" Font-Size="10" />
                <SelectedNodeStyle BackColor="WhiteSmoke" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
