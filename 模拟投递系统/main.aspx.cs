using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace 模拟投递系统
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string mySqlConnect = ConfigurationManager.AppSettings["mySqlConnect"].ToString();

        //MySqlHelper msh = new MySqlHelper("Database='test';Data Source='10.138.19.18';User Id='root';Password='85459666';charset='utf8';pooling=true");
        MySqlHelper msh;
        string TableName = "";
        string post_list_text = "";
        string duan_list_text = "";
        string zhan_duan_text = " and (length(trim(zhan))=0 and length(trim(duan))=0) ";
        string name = "";
        int page = 1;
        int PageCount = 0;
        bool GetCountFlag = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack==false)
            {
                Session["page"] = null;
                Session["PageCount"] = null;
                Session["post_list_text"] = null;
                Session["duan_list_text"] = null;
            }
            if (Session["page"] != null)
                page = int.Parse(Session["page"].ToString());
            if (Session["PageCount"] != null)
                PageCount = int.Parse(Session["PageCount"].ToString());
            if (Session["table"] != null && Session["NAME"] != null)
            {
                msh = new MySqlHelper(mySqlConnect);
                TableName = (string)(Session["table"]);
                this.div_name.InnerText = Session["NAME"].ToString();
                name = Session["NAME"].ToString();
                if (Session["post_list_text"] != null)
                {
                    if (Session["post_list_text"].ToString() == "全部")
                    {
                        post_list_text = "";
                    }
                    else
                    {
                        post_list_text = " and yb='" + Session["post_list_text"].ToString() + "'";
                    }
                }
                if (Session["duan_list_text"] != null)
                {
                    if (Session["duan_list_text"].ToString() == "全部")
                    {
                        duan_list_text = "";
                        zhan_duan_text = " and length(trim(duan))=0 and length(trim(duan))=0 ";
                    }
                    else
                    {
                        duan_list_text = " and duan='" + Session["duan_list_text"].ToString() + "'";
                        zhan_duan_text = null;
                    }
                }
                if (!IsPostBack)
                {
                    DropDownList_Load();
                    RepeaterLoad();
                }
            }
            else
                this.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {

                Label rc = (Label)this.Repeater1.Items[i].FindControl("rc_text");
                TextBox yb = (TextBox)this.Repeater1.Items[i].FindControl("yb_text");
                TextBox zhan = (TextBox)this.Repeater1.Items[i].FindControl("zhan_text");
                TextBox duan = (TextBox)this.Repeater1.Items[i].FindControl("duan_text");
                if (yb.Text.Length == 6 && DropDownList2.Text != duan.Text)
                {
                    MySqlParameter[] msp = new MySqlParameter[4];
                    msp[0] = new MySqlParameter("?rc", MySqlDbType.Int32);
                    msp[0].Value = rc.Text;
                    msp[1] = new MySqlParameter("?yb", MySqlDbType.VarChar, 10);
                    msp[1].Value = yb.Text;
                    msp[2] = new MySqlParameter("?zhan", MySqlDbType.VarChar, 10);
                    msp[2].Value = zhan.Text;
                    msp[3] = new MySqlParameter("?duan", MySqlDbType.VarChar, 10);
                    msp[3].Value = duan.Text;
                    msh.ExecuteNonQuery("update " + TableName + " set yb=?yb,zhan=?zhan,duan=?duan where rc=?rc", msp);

                }
            }
            RepeaterLoad();
        }
        private void DropDownList_Load()
        {
            DropDownList1.Items.Add("全部");
            if (name == "测试用户")
            {
                DataTable dt = msh.ExecuteDataTable("select * from tdg_post");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(dt.Rows[i]["post"].ToString());
                }
            }
            else
            {
                //DataTable dt = msh.ExecuteDataTable("select * from tdg_post");
                MySqlParameter[] msp = new MySqlParameter[1];
                msp[0] = new MySqlParameter("@tdg", name);
                DataTable dt = msh.ExecuteDataTable("select * from tdg_post where tdg=@tdg", msp);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(dt.Rows[i]["post"].ToString());
                }
            }
        }
        private void RepeaterLoad()
        {
            int start = (page - 1) * 50;
            int end = (page * 50) - 1;
            if (GetCountFlag == true)
            {
                string SqlCountText = "select count(*) from " + TableName + " where 1=1" + zhan_duan_text + post_list_text + duan_list_text;
                DataRow dr = msh.ExecuteDataRow(SqlCountText);
                int Count = int.Parse(dr[0].ToString());
                PageCount = (Count / 50) + 1;
                Session["PageCount"] = PageCount;
                GetCountFlag = false;
            }
            if (page == 1)
                UpPage.Visible = false;
            else
                UpPage.Visible = true;
            if (page >= PageCount)
                NextPage.Visible = false;
            else
                NextPage.Visible = true;
            string SqlText = "select * from " + TableName + " where 1=1" + zhan_duan_text + post_list_text + duan_list_text + " LIMIT " + start + "," + end;
            DataTable dt = msh.ExecuteDataTable(SqlText);
            Repeater1.DataSource = dt.DefaultView;
            Repeater1.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCountFlag = true;
            Session["post_list_text"] = DropDownList1.Text;
            if (DropDownList1.Text == "全部")
            {
                post_list_text = "";
            }
            else
            {
                post_list_text = " and yb='" + DropDownList1.Text + "'";
            }
            RepeaterLoad();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCountFlag = true;
            Session["duan_list_text"] = DropDownList2.Text;
            if (DropDownList2.Text == "全部")
            {
                duan_list_text = "";
                zhan_duan_text = " and (length(trim(zhan))=0 and length(trim(duan))=0) ";

            }
            else
            {
                duan_list_text = " and duan='" + DropDownList2.Text + "'";
                zhan_duan_text = "";
            }
            RepeaterLoad();
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            page++;
            Session["page"] = page;
            RepeaterLoad();
        }

        protected void UpPage_Click(object sender, EventArgs e)
        {
            if (page > 1)
                page--;
            Session["page"] = page;
            RepeaterLoad();
        }


    }
}