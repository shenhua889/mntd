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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string mySqlConnect = ConfigurationManager.AppSettings["mySqlConnect"].ToString();
            MySqlHelper msh = new MySqlHelper(mySqlConnect);
            string uid = TextBox1.Text;
            string password = TextBox2.Text;
            MySqlParameter[] msp=new MySqlParameter[2];
            msp[0] = new MySqlParameter("?uid", MySqlDbType.VarChar, 200);
            msp[0].Value = uid;
            msp[1] = new MySqlParameter("?password", MySqlDbType.VarChar, 100);
            msp[1].Value = password;
            DataTable dt = msh.ExecuteDataTable("select * from `user` where uid=?uid and `password`=?password", msp);
            if (dt.Rows.Count > 0)
            {
                Session["UID"] = uid;
                Session["NAME"] = dt.Rows[0]["name"].ToString();
                Server.Transfer("HtmlPage1.html");
            }
            else
            {
                Label4.Visible = true;
            }
        }
        public string UID
        {
            get { return TextBox1.Text; }
        }
        public string PASSWORD
        {
            get { return TextBox2.Text; }
        }
    }
}