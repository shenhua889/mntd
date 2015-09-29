using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Text;
using System.Data.OleDb;
namespace 模拟投递系统
{
    public class OpenToCsv
    {
        public DataSet OpenCsv(string strFilePath)
        {
            if (!File.Exists(strFilePath))
            {
                return null;
            }
            string strFolderPath = Path.GetDirectoryName(strFilePath);
            string strCSVFile = Path.GetFileName(strFilePath);

            DataSet ds = null;
            string strConnection = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + strFolderPath + ";Extensions=asc,csv,tab,txt;Persist Security Info=False";
            try
            {
                using (OdbcConnection conn = new OdbcConnection(strConnection.Trim()))
                {
                    conn.Open();
                    string strSql = "select * from [" + strCSVFile + "]";
                    OdbcDataAdapter odbcDAdapter = new OdbcDataAdapter(strSql, conn);
                    ds = new DataSet();
                    odbcDAdapter.Fill(ds, "table");
                    //ds.Tables[0].Rows[0]["zh"] = "12345678901234567890";
                    conn.Close();
                }
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
            return ds;
        }
        public DataSet OpenCsv2(string strFilePath)
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            StreamReader sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
            bool flat = false;
            while (sr.Peek() > 0)
            {
                string line = sr.ReadLine();
                string[] anyline = new string[70];
                anyline = line.Split(',');
                if (flat == false)
                {
                    for (int i = 0; i < anyline.Length; i++)
                    {
                        dt.Columns.Add(anyline[i], typeof(string));
                    }
                    flat = true;
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < anyline.Length; i++)
                    {
                        dr[i] = anyline[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            ds.Tables.Add(dt);
            sr.Close();
            return ds;
        }
        /// <summary>  
        /// 查询CSV文件记录  
        /// </summary>  
        /// <param name="directory">目录</param>  
        /// <param name="fileName">文件名(带后缀)</param>  
        /// <param name="tj">查询条件(例如："WHERE 姓名 = '凌老大'"，如无条件则为null)</param>  
        /// <returns></returns>  
        public DataSet SelectCSV(string directory, string fileName)
        {
            string ConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Text;CharacterSet=65001;'";
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("select * from [{0}] ", fileName), string.Format(ConnStr, directory));
            adapter.Fill(ds);
            return ds;
        }  
        public void SaveCSV(System.Data.DataTable dt, string fileName)
        {
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            //string data = "";
            StringBuilder data = new StringBuilder();
            //写出列名称
            int dtcount = dt.Columns.Count;
            int dtrowc = dt.Rows.Count;
            for (int i = 0; i < dtcount; i++)
            {
                //data += dt.Columns[i].ColumnName.ToString();
                data.Append(dt.Columns[i].ColumnName.ToString());
                if (i < dtcount - 1)
                {
                    //data += ",";
                    data.Append(",");
                }
            }
            sw.WriteLine(data);
            //写出各行数据
            data.Length = 0;
            for (int i = 0; i < dtrowc; i++)
            {

                for (int j = 0; j < dtcount; j++)
                {
                    //data += dt.Rows[i][j].ToString();
                    data.Append(dt.Rows[i][j].ToString());
                    if (j < dtcount - 1)
                    {
                        //data += ",";
                        data.Append(",");
                    }
                }
                if (i < dtrowc - 1)
                {
                    //data += "\r\n";
                    data.Append("\r\n");
                }
            }
            sw.Write(data);
            sw.Close();
            fs.Close();
            Debug.WriteLine("CSV文件保存成功！");
        }
    }
}