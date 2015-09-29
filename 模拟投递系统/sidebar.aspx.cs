using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
namespace 模拟投递系统
{
    public partial class sidebar : System.Web.UI.Page
    {
        string mySqlConnect = ConfigurationManager.AppSettings["mySqlConnect"].ToString();

        //MySqlHelper msh = new MySqlHelper("Database='test';Data Source='10.138.19.18';User Id='root';Password='85459666';charset='utf8';pooling=true");
        MySqlHelper msh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                msh = new MySqlHelper(mySqlConnect);
                Tree_Load();
            }
        }
        private void Tree_Load()
        {
            DataTable dt = msh.ExecuteDataTable("select * from tablelist");
            TreeNode tn = new TreeNode();
            tn.Text = "录入模拟投递";
            TreeView1.Nodes.Add(tn);
            tn.SelectAction = TreeNodeSelectAction.None;
            TreeNode tn1 = new TreeNode();
            tn1.Text = "录入情况";
            tn1.SelectAction = TreeNodeSelectAction.None;
            TreeView1.Nodes.Add(tn1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode node_temp = new TreeNode();
                node_temp.Text = dt.Rows[i]["tablename"].ToString();
                node_temp.Value = dt.Rows[i]["table"].ToString();
                //node_temp.NavigateUrl = @"../main.aspx";
                //node_temp.Target = "_main";
                TreeView1.Nodes[0].ChildNodes.Add(node_temp);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode node_temp = new TreeNode();
                node_temp.Text = dt.Rows[i]["tablename"].ToString();
                node_temp.Value = dt.Rows[i]["table"].ToString();
                //node_temp.NavigateUrl = @"../main.aspx";
                //node_temp.Target = "_main";
                TreeView1.Nodes[1].ChildNodes.Add(node_temp);
            }
        }
        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {
            string table = "";
            table=TreeView1.SelectedNode.Value;
            Session["table"] = table;
            if (TreeView1.SelectedNode.Parent.Text == "录入模拟投递")
            {
                //Response.Write("<script>parent._main.location.reload()</script>");

                Response.Write("<script>parent._main.location='main.aspx'</script>");
            }
            else if (TreeView1.SelectedNode.Parent.Text == "录入情况")
            {
                //Response.Write("<script>parent._main.location.reload()</script>");
                Response.Write("<script>parent._main.location='TablesCount.aspx'</script>");

            }
        }
    }
}