<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TablesCount.aspx.cs" Inherits="模拟投递系统.TablesCount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        #div_post {
            width: 100px;
            float: left;
            height: 20px;
            margin-left:50px;
            margin-top:5px;
        }

        #div_tdg {
            width: 200px;
            float: left;
            height: 20px;
            margin-top:5px;
        }

        #div_okcou {
            width: 100px;
            float: left;
            height: 20px;
            margin-top:5px;
        }

        #div_cou {
            width: 100px;
            float: left;
            height: 20px;
            margin-top:5px;
        }

        [id*=itemdiv] {
            height: 30px;
            width: 100%;
            width:600px;
            margin-left:150px;
        }
        .div_odd {
            background-color: #EBF6F8;
            border:#C8E6F1 1px solid;
        }

        .div_even {
            background-color: #FFF;
        }

        /* 划过行样式 */
        .div_hover {
            background-color: #99CCFF;
        }

        /* 选中行样式 */
        .div_selected {
            background-color: #C8C5FC;
        }
    </style>
    <script src="jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id^=itemdiv]:odd").addClass("div_odd");
            $("[id^=itemdiv]:even").addClass("div_even");
        })
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <div id="itemdiv" style="margin-top:50px;">
                        <div id="div_post">邮编</div>
                        <div id="div_tdg">投递组</div>
                        <div id="div_okcou">完成数</div>
                        <div id="div_cou">总数</div>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="itemdiv<%#Eval("post") %>">
                        <div id="div_post"><%#Eval("post") %></div>
                        <div id="div_tdg"><%#Eval("tdg") %></div>
                        <div id="div_okcou"><%#Eval("完成数") %></div>
                        <div id="div_cou"><%#Eval("总数") %></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
