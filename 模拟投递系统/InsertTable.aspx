<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertTable.aspx.cs" Inherits="模拟投递系统.InsertTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        /* 隔行样式 */
        td {
            padding-left: 15px;
            padding-right: 15px;
        }

        .td_odd {
            background-color: #EBF6F8;
            border: #C8E6F1 1px solid;
        }

        .td_even {
            background-color: #FFF;
        }

        #div_dropDown div {
            margin-right: 30px;
        }
    </style>
    <script src="jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("tbody").children("tr:odd").addClass("td_odd");
            $("tbody").children("tr:even").addClass("td_even");
        })
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 30px; margin-left: 40%;">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
        <div>
            <table style="margin-left: auto; margin-right: auto; margin-top: 50px; border: 2px;">
                <%=GetTitle() %>
                <%=GetContent() %>
            </table>
        </div>
        <div id="div_dropDown" runat="server" style="text-align: center; margin-left: auto; margin-right: auto; width: 430px;">
            <div id="div_dz" style="float: left;">
                地址:
                <asp:DropDownList ID="list_dz" runat="server"></asp:DropDownList>
            </div>
            <div id="div_yb" style="float: left;">
                邮编:
                <asp:DropDownList ID="list_yb" runat="server"></asp:DropDownList>
            </div>
            <div id="div_zhan" style="float: left;">
                站:
                <asp:DropDownList ID="list_zhan" runat="server"></asp:DropDownList>
            </div>
            <div id="div_duan" style="float: left;">
                段:
                <asp:DropDownList ID="list_duan" runat="server"></asp:DropDownList>
            </div>
            <div style="clear: both"></div>
            <div id="div_dataTableName">
                表格名称: 
                <asp:TextBox ID="tableNameText" runat="server"></asp:TextBox>
            </div>
            <div id="div_update">
                <asp:Button ID="button_update" runat="server" Text="确认" OnClick="button_update_Click" />
            </div>
        </div>
    </form>
</body>
</html>
