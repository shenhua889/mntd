<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="模拟投递系统.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        #post_search {
            width: 150px;
            height: 40px;
            float: left;
        }

        #main {
            min-width: 935px;
            width: auto;
        }

        #duan_search {
            width: 150px;
            height: 40px;
            float: left;
        }

        #div_name {
            width: 150px;
            height: 40px;
            float: right;
        }

        #search {
            width: 100%;
            height: 40px;
        }

        body {
            font-family: 微软雅黑;
            font-weight: 400;
            background-color: #f5f5f5;
            color: #333;
        }

        #div_rc {
            background-color: #fff;
            width: 70px;
            margin: 5px;
            float: left;
            padding: 5px;
        }

        #div_post {
            background-color: #fff;
            width: 40px;
            margin: 5px;
            float: left;
            padding: 5px;
            margin-right: 0px;
            height: 22px;
        }

        #div_posttext {
            background-color: #fff;
            width: 50px;
            margin-bottom: 5px;
            margin-top: 5px;
            margin-right: 5px;
            float: left;
            /*padding:5px;*/
        }

        #div_addr {
            background-color: #fff;
            /*width: auto;*/
            margin: 5px;
            float: left;
            padding: 5px;
            /*min-width: 500px;*/
            width:500px;
        }

        #div_zhan {
            background-color: #fff;
            width: 20px;
            margin: 5px;
            float: right;
            padding: 5px;
            margin-right: 0px;
            height: 23px;
        }

        #div_zhantext {
            background-color: #fff;
            width: 100px;
            margin-bottom: 5px;
            margin-top: 5px;
            margin-right: 5px;
            float: right;
            /*padding:5px;*/
        }

        #div_duan {
            background-color: #fff;
            width: 20px;
            margin: 5px;
            float: right;
            padding: 5px;
            margin-right: 0px;
            height: 23px;
        }

        #div_duantext {
            background-color: #fff;
            width: 50px;
            margin-bottom: 5px;
            margin-top: 5px;
            margin-right: 5px;
            float: right;
            /*padding:5px;*/
        }

        [type] {
            border: 1px solid #999;
            font-family: 微软雅黑;
        }
        /* 隔行样式 */
        .div_odd {
            background-color: #EBF6F8;
            border: #C8E6F1 1px solid;
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
            $("div [id^=div_item]:odd").addClass("div_odd");
            $("div [id^=div_item]:even").addClass("div_even");
            $("#duan").bind("keyup", function () {
                var text2 = $("#duan").val();
                $("[id*='duan_text']").val(text2);
                $("[id*='duan_text']").change();
            })
            $("#yb").bind("keyup", function () {
                var text2 = $("#yb ").val();
                $("[id*='yb_text']").val(text2);
            })
            $("#zhan").bind("keyup", function () {
                var text2 = $("#zhan").val();
                $("[id*='zhan_text']").val(text2);
            })
            $("div [id^=div_item]").mouseover(function () {
                $(this).addClass("div_hover");
            })
            $("div [id^=div_item]").mouseout(function () {
                $(this).removeClass("div_hover");
            })
            //var $inp = $("input");
            //$inp.keypress(function (e) {

            //    var key = e.which;
            //    if(key=='13')
            //    {
            //        e.preventDefault();
            //    }
            //})
            $("[id*='duan_text']").change(function () {
                $(this).val($(this).val().replace(/\D/g, ""));
            })
            $("[id*='yb_text']").change(function () {
                $(this).val($(this).val().replace(/\D/g, ""));
            })
            $("[id*='duan_text']").keydown(function (e) {
                var key = e.which;
                if (key == '13') {
                    var txt = $(this).val();
                    $(this).parent().parent().next().children("[id*='div_duantext']").children("[id*='duan_text']").val(txt);
                    $(this).parent().parent().next().children("[id*='div_duantext']").children("[id*='duan_text']").select();
                    //$(this).parent().parent().next().children("[id*='div_duantext']").children("[id*='duan_text']").focus();
                    var scroll_offset = $(this).parent().parent().next().children("[id*='div_duantext']").children("[id*='duan_text']").offset();
                    $("body").animate({ scrollTop: scroll_offset.top }, 300);
                }
                if ((key >= 48 && key <= 57) || (key >= 96 && key <= 105) || key == 8 || key == 9) {
                    return true;
                }
                else {
                    return false;

                }
            })
            $("[id*='yb_text']").keydown(function (e) {
                var key = e.which;
                if ($(this).parent().siblings("[id*=div_duantext]").children("[id*=duan_text]").val() == "888" || $(this).parent().siblings("[id*=div_duantext]").children("[id*=duan_text]").val() == "777") {
                    $(this).parent().siblings("[id*=div_duantext]").children("[id*=duan_text]").val("");
                }
                if (key == '13') {
                    var txt = $(this).val();
                    $(this).parent().parent().next().children("[id*='div_posttext']").children("[id*='yb_text']").val(txt);
                    $(this).parent().parent().next().children("[id*='div_posttext']").children("[id*='yb_text']").select();
                    //$(this).parent().parent().next().children("[id*='div_posttext']").children("[id*='yb_text']").focus();
                    var scroll_offset = $(this).parent().parent().next().children("[id*='div_posttext']").children("[id*='yb_text']").offset();
                    $("body").animate({ scrollTop: scroll_offset.top }, 300);
                }
                if ((key >= 48 && key <= 57) || (key >= 96 && key <= 105) || key == 8 || key == 9) {
                    return true;
                }
                else {
                    return false;

                }
                //if($(this).parent().siblings("[id*='dvi_duantext']").children("[id*='duan_text']").val()=='777' || $(this).parent().siblings("[id*='dvi_duantext']").children("[id*='duan_text']").val()=='888')
                //{

                //}
            })
            $("[id*='duan_text']").bind("keyup", function () {
                var name = $("#div_name").html();
                if ($(this).val() == "")
                    $(this).parent().siblings("[id*='div_zhantext']").children("[id*='zhan']").val("");
                else
                    $(this).parent().siblings("[id*='div_zhantext']").children("[id*='zhan']").val(name);
            })
            //$("[id*='duan_text']").change(function () {
            //    var name = $("#div_name").html();
            //    if ($(this).val() == "")
            //        $(this).parent().siblings("[id*='div_zhantext']").children("[id*='zhan']").val("");
            //    else
            //        $(this).parent().siblings("[id*='div_zhantext']").children("[id*='zhan']").val(name);
            //})
        })

    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="search">
            <div id="post_search">
                邮编:<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Font-Names="微软雅黑"></asp:DropDownList>
            </div>
            <div id="duan_search">
                段:<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Font-Names="微软雅黑">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem>888</asp:ListItem>
                    <asp:ListItem>777</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="div_name" runat="server">
            </div>
        </div>
        <div id="main">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <div style="height: 43px;">
                        <div id="div_rc">rc</div>
                        <div id="div_post">邮编</div>
                        <div id="div_posttext">
                            <input id="yb" type="text" maxlength="6" style="width: 50px; height: 29px;" />
                        </div>
                        <div id="div_addr">地址</div>
                        <div id="div_zhan">站</div>
                        <div id="div_zhantext">
                            <input id="zhan" type="text" maxlength="10" style="width: 100px; height: 29px;" disabled="disabled" />
                        </div>
                        <div id="div_duan">段</div>
                        <div id="div_duantext">
                            <input id="duan" type="text" maxlength="6" style="width: 50px; height: 29px;" />
                        </div>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="div_item<%#Eval("rc") %>>">
                        <div id="div_rc">
                            <asp:Label ID="rc_text" runat="server" Text='<%#Eval("rc") %>'></asp:Label>
                        </div>
                        <div id="div_post"></div>
                        <div id="div_posttext">
                            <asp:TextBox Width="50" runat="server" ID="yb_text" Text='<%#Eval("yb").ToString().Trim()%>' MaxLength="6" Height="29px"></asp:TextBox>
                        </div>
                        <div id="div_addr"><%#Eval("dz").ToString().Trim() %></div>
                        <div id="div_zhan"></div>
                        <div id="div_zhantext">
                            <asp:TextBox Width="100" ID="zhan_text" runat="server" Text='<%#Eval("zhan").ToString().Trim()%>' MaxLength="6" Height="29px" Enabled="False"></asp:TextBox>
                        </div>
                        <div id="div_duan"></div>
                        <div id="div_duantext">
                            <asp:TextBox Width="50" ID="duan_text" runat="server" Text='<%#Eval("duan").ToString().Trim()%>' MaxLength="6" Height="29px"></asp:TextBox>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div style="align-content: center; text-align: center">
                <asp:Button ID="UpPage" runat="server" Text="上一页" Style="margin-right: 250px;" OnClick="UpPage_Click" />
                <input type="text" style="display: none" />
                <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" UseSubmitBehavior="false" Style="margin: auto" />
                <asp:Button ID="NextPage" runat="server" Text="下一页" Style="margin-left: 250px;" OnClick="NextPage_Click" />
            </div>
        </div>
    </form>
</body>
</html>
