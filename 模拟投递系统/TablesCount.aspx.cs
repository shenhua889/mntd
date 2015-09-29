using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
namespace 模拟投递系统
{
    public partial class TablesCount : System.Web.UI.Page
    {
        string mySqlConnect = ConfigurationManager.AppSettings["mySqlConnect"].ToString();

        //MySqlHelper msh = new MySqlHelper("Database='test';Data Source='10.138.19.18';User Id='root';Password='85459666';charset='utf8';pooling=true");
        MySqlHelper msh;
        string TableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["table"] != null)
            {
                msh = new MySqlHelper(mySqlConnect);
                TableName = Session["table"].ToString();
                RepeLoad();
            }
            else
            {
                this.Visible = false;
            }
        }
        private void RepeLoad()
        {
            DataTable RepeTable = msh.GetDataTable("select * from tdg_post");
            RepeTable.Columns.Add("完成数");
            RepeTable.Columns.Add("总数");
            DataRow count = RepeTable.NewRow();
            count["tdg"] = "总数";
            count["完成数"] = "0";
            count["总数"] = "0";
            DataRow other = RepeTable.NewRow();
            other["tdg"] = "其他";
            other["完成数"] = "0";
            other["总数"] = "0";
            RepeTable.Rows.Add(other);
            DataRow qqq = RepeTable.NewRow();
            qqq["post"] = "   ";
            qqq["tdg"] = "777";
            qqq["完成数"] = "";
            qqq["总数"] = "0";
            RepeTable.Rows.Add(qqq);
            DataRow bbb = RepeTable.NewRow();
            bbb["post"] = "      ";
            bbb["tdg"] = "888";
            bbb["完成数"] = "";
            bbb["总数"] = "0";
            RepeTable.Rows.Add(bbb);
            //不包含777和888数据的统计
            int IndexOfOther = RepeTable.Rows.IndexOf(other);
            int IndexOfqqq = RepeTable.Rows.IndexOf(qqq);
            int IndexOfbbb = RepeTable.Rows.IndexOf(bbb);
            DataTable CouTable = msh.GetDataTable("select a.*,IFNULL(b.`完成数`,0) as 完成数 from (SELECT *,count(*) as 总数 from " + TableName + " where duan<>'777' and duan<>'888'  group by yb) as a left join (select *,count(*) as 完成数 from " + TableName + " where LENGTH(duan)>=1 and duan<>'777' and duan<>'888' group by yb) as b on a.yb=b.yb");
            for (int i = 0; i < CouTable.Rows.Count; i++)
            {
                bool flat = false;
                count["完成数"] =int.Parse(count["完成数"].ToString()) + int.Parse(CouTable.Rows[i]["完成数"].ToString());
                count["总数"] = int.Parse(count["总数"].ToString()) + int.Parse(CouTable.Rows[i]["总数"].ToString());
                for (int j = 0; j < RepeTable.Rows.Count; j++)
                {
                    if (CouTable.Rows[i]["yb"].ToString() == RepeTable.Rows[j]["post"].ToString())
                    {
                        RepeTable.Rows[j]["完成数"] = CouTable.Rows[i]["完成数"];
                        RepeTable.Rows[j]["总数"] = CouTable.Rows[i]["总数"];
                        flat = true;
                    }
                }
                if (flat == false)
                {
                    RepeTable.Rows[IndexOfOther]["总数"] = (int.Parse(RepeTable.Rows[IndexOfOther]["总数"].ToString()) + int.Parse(CouTable.Rows[i]["总数"].ToString())).ToString();
                    RepeTable.Rows[IndexOfOther]["完成数"] = (int.Parse(RepeTable.Rows[IndexOfOther]["完成数"].ToString()) + int.Parse(CouTable.Rows[i]["完成数"].ToString())).ToString();
                }
            }
            DataTable NonTable = msh.GetDataTable("select duan,IFNULL(count(*),0) as 总数 from " + TableName + " where duan='777' or duan='888' group by duan");
            if (NonTable.Rows.Count > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    count["总数"] = int.Parse(count["总数"].ToString()) + int.Parse(NonTable.Rows[i]["总数"].ToString());
                    if (NonTable.Rows[i]["duan"].ToString() == "777")
                    {
                        RepeTable.Rows[IndexOfqqq]["总数"] = NonTable.Rows[i]["总数"];
                    }
                    else if (NonTable.Rows[i]["duan"].ToString() == "888")
                    {
                        RepeTable.Rows[IndexOfbbb]["总数"] = NonTable.Rows[i]["总数"];
                    }
                }
            }
            else if (NonTable.Rows.Count == 1)
            {
                count["总数"] = int.Parse(count["总数"].ToString()) + int.Parse(NonTable.Rows[0]["总数"].ToString());
                if (NonTable.Rows[0]["duan"].ToString() == "777")
                {
                    RepeTable.Rows[IndexOfqqq]["总数"] = NonTable.Rows[0]["总数"];
                }
                else if (NonTable.Rows[0]["duan"].ToString() == "888")
                {
                    RepeTable.Rows[IndexOfbbb]["总数"] = NonTable.Rows[0]["总数"];
                }
            }
            RepeTable.Rows.Add(count);
            Repeater1.DataSource = RepeTable.DefaultView;
            Repeater1.DataBind();
        }
    }
}