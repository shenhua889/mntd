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
    public partial class InsertTable : System.Web.UI.Page
    {
        OpenToCsv openToCsv = new OpenToCsv();
        bool getDataFlat = false;
        DataTable dt;
        string mySqlConnect;
        MySqlHelper msh;
        protected void Page_Load(object sender, EventArgs e)
        {
            mySqlConnect = ConfigurationManager.AppSettings["mySqlConnect"].ToString();
            msh = new MySqlHelper(mySqlConnect);
            if (getDataFlat != true)
            {
                div_dropDown.Visible = false;
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(@"D:/temp/" + FileUpload1.FileName);
                OpenCsvToDataTable(FileUpload1.FileName);
                DropDownListLoad();
                dataTableNameLoad();
            }
        }
        private void dataTableNameLoad()
        {
            string[] fileNameSplit = FileUpload1.FileName.Split('.');
            tableNameText.Text = fileNameSplit[0];
        }
        private void DropDownListLoad()
        {
            div_dropDown.Visible = true;//dropdownlist的显示
            list_duan.Items.Clear();//dropdownlist的清空
            list_dz.Items.Clear();
            list_yb.Items.Clear();
            list_zhan.Items.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                list_duan.Items.Add(dt.Columns[i].ColumnName.ToString());
                list_dz.Items.Add(dt.Columns[i].ColumnName.ToString());
                list_yb.Items.Add(dt.Columns[i].ColumnName.ToString());
                list_zhan.Items.Add(dt.Columns[i].ColumnName.ToString());
            }
        }
        private void OpenCsvToDataTable(string fileName)
        {
            if (DecideToSuffix(fileName))
            {
                dt = openToCsv.SelectCSV(@"d:/temp/", fileName).Tables[0];
                ViewState["dt"] = dt;
                getDataFlat = true;
            }
        }
        private bool DecideToSuffix(string fileName)
        {
            string[] fileSplit = fileName.Split('.');
            if (fileSplit.Last() == "csv")
            {
                return true;
            }
            else
                return false;
        }
        public string GetTitle()
        {
            if (getDataFlat == true)
            {
                string title = "<tr>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    title += "<td>" + dt.Columns[i].ColumnName + "</td>";
                }
                title += "</tr>";
                return title;
            }
            return null;
        }
        public string GetContent()
        {
            if (getDataFlat == true)
            {
                string content = "";
                int rowsCount = 50;
                if (dt.Rows.Count < 50)
                {
                    rowsCount = dt.Rows.Count;
                }
                for (int i = 0; i < rowsCount; i++)
                {
                    content += "<tr>";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        content += "<td>" + dt.Rows[i][j] + "</td>";
                    }
                    content += "</tr>";
                }
                return content;
            }
            return null;
        }

        protected void button_update_Click(object sender, EventArgs e)
        {
            CreatTable();
            ChangeColumn(list_yb.Text, "yb");
            ChangeColumn(list_dz.Text, "dz");
            ChangeColumn(list_zhan.Text, "zhan");
            ChangeColumn(list_duan.Text, "duan");
        }
        private void CreatTable()
        {
            dt = (DataTable)ViewState["dt"];
            MySqlParameter[] msps = new MySqlParameter[0];
            //string sqlString="CREATE TABLE ?tableName (`rc`  int(10) NULL AUTO_INCREMENT ,";
            string sqlString = "CREATE TABLE `" + tableNameText.Text + "` (`rc`  int(10) NULL AUTO_INCREMENT ,";
            //msps[0] = new MySqlParameter("?tableName", MySqlDbType.VarChar);
            //msps[0].Value = tableNameText.Text;
            for (int i = 1; i < dt.Columns.Count + 1; i++)
            {
                //msps[i] = new MySqlParameter("?" + dt.Columns[i-1].ColumnName,dt.Columns[i-1].ColumnName);
                //sqlString += "?" + dt.Columns[i - 1].ColumnName + " varchar(255) NULL,";
                sqlString += "`" + dt.Columns[i - 1].ColumnName + "` varchar(255) NULL,";
            }
            sqlString += "PRIMARY KEY (`rc`))";
            msh.ExecuteNonQuery(sqlString, msps);
        }
        private void ChangeColumn(string oldColumnName, string newColumnName)
        {
            string tableName = tableNameText.Text;
            MySqlParameter[] msps = new MySqlParameter[0];
            string sqlString = "ALTER TABLE `"+tableName+"`CHANGE COLUMN `"+oldColumnName+"` `"+newColumnName+"`  varchar(255)";
            msh.ExecuteNonQuery(sqlString, msps);
        }
    }
}