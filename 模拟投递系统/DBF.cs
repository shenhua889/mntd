//2014-07-27 15:59
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Data.Odbc;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
namespace 模拟投递系统
{
    class DBF
    {
        public void UpdateDbf(string FileName, string ZipName, string zip, string AddName, string add)
        {
            string connStr = @"Provider=vfpoledb;Data Source=" + FileName + ";Collating Sequence=machine;";
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            //string s = "insert into " + table + " values('4','4')";
            string SQLstr = @"update " + FileName + " set  " + ZipName + "='" + zip + "' where " + AddName + "==[" + add + "]";
            OleDbCommand cmd = new OleDbCommand(SQLstr, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("dz", OleDbType.VarChar, 50).Value = add;
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }
        public void UpdateDatetableToDbf(string FileName, System.Data.DataTable dt, string ZipName, string AddName)
        {
            string connStr = @"Provider=vfpoledb;Data Source=" + FileName + ";Collating Sequence=machine;";
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            string SQLstr = "";
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                cmd.Parameters.Clear();
                SQLstr = "update " + FileName + " set  " + ZipName + "='" + dt.Rows[i][ZipName] + "' where " + AddName + "==?";
                cmd.Parameters.Add("?", OleDbType.VarChar, 100).Value = dt.Rows[i][AddName].ToString().Trim();
                cmd.CommandText = SQLstr;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            conn.Dispose();
        }
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ColName"></param>
        public void DbfAddCol(string FileName, string ColName)
        {
            string connStr = @"Provider=vfpoledb;Data Source=" + FileName + ";Collating Sequence=machine;";
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = connStr;
            OleDbCommand cmd = new OleDbCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "alter table ? add ? char(5)";
            cmd.Parameters.AddWithValue("?", FileName);
            cmd.Parameters.AddWithValue("?", ColName);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            cmd.CommandText = "update [" + FileName + "] set " + ColName + "=''";
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            cmd.CommandText = "alter table [" + FileName + "] alter " + ColName + " not null";
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }
        /// <summary>
        /// 清空字段
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ColName"></param>
        public void DbfCleanCol(string FileName, string ColName)
        {
            try
            {
                string connStr = @"Provider=vfpoledb;Data Source=" + FileName + ";Collating Sequence=machine;";
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = connStr;
                OleDbCommand cmd = new OleDbCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SET DELETED OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"update " + FileName + " set  " + ColName + "='1'";
                //cmd.Parameters.AddWithValue("?", ColName);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// 添加DBF
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="dt"></param>
        /// <param name="ZipName"></param>
        /// <param name="AddName"></param>
        public void InsertDatetableToDbf(string FileName, System.Data.DataTable dt, string ZipName, string AddName)
        {
            try
            {
                string connStr = @"Provider=vfpoledb;Data Source=" + FileName + ";Collating Sequence=machine;";
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = connStr;
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from ?";
                cmd.Parameters.AddWithValue("?", FileName);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                cmd.CommandText = "pack ?";
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string SQLstr = "";
                int count = dt.Rows.Count;
                string SQLstr1 = "";
                int Ccount = dt.Columns.Count;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    SQLstr1 = SQLstr1 + "?,";
                }
                SQLstr1 = SQLstr1.Remove(SQLstr1.Length - 1, 1);
                SQLstr = "insert into ? values(" + SQLstr1 + ")";
                cmd.CommandText = SQLstr;
                //for (int i = 0; i < count; i++)
                //{
                //    cmd.Parameters.Clear();
                //    cmd.Parameters.AddWithValue("?", FileName);
                //    for (int j = 0; j < Ccount; j++)
                //    {
                //        cmd.Parameters.AddWithValue("?", dt.Rows[i][j]);
                //    }
                //    cmd.ExecuteNonQuery();
                //}
                foreach (DataRow dr in dt.Rows)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("?", FileName);
                    foreach (DataColumn dc in dt.Columns)
                    {
                        cmd.Parameters.AddWithValue("?", dr[dc]);
                    }
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public void InsertDatetableToDbf1(string FileName, System.Data.DataTable dt, string ZipName, string AddName)
        {
            try
            {
                string connStr = @"Provider=vfpoledb;Data Source=" + FileName + ";Collating Sequence=machine;";
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = connStr;
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from ?";
                cmd.Parameters.AddWithValue("?", FileName);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                cmd.CommandText = "pack ?";
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                //string SQLstr = "";
                int count = dt.Rows.Count;
                string SQLstr1 = "";
                int Ccount = dt.Columns.Count;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    SQLstr1 = SQLstr1 + "?,";
                }
                SQLstr1 = SQLstr1.Remove(SQLstr1.Length - 1, 1);
                cmd.Parameters.Clear();
                //SQLstr = "insert into ? values";
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into ? values");
                cmd.Parameters.AddWithValue("?", FileName);
                for (int i = 0; i < count; i++)
                {

                }

                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }


        /// <summary>
        /// 这个是错的.以后改
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="dt"></param>
        /// <param name="ZipName"></param>
        /// <param name="AddName"></param>
        public void UpdateDatatableToDbf(string FileName, System.Data.DataTable dt, string ZipName, string AddName)
        {
            try
            {
                string connStr = @"Provider=vfpoledb;Data Source=" + FileName + ";Collating Sequence=machine;";
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = connStr;
                conn.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from ?";
                cmd.Parameters.AddWithValue("?", FileName);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                cmd.CommandText = "pack ?";
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();

                OleDbDataAdapter odda = new OleDbDataAdapter(@"select * from " + FileName, conn);
                DataTable dt1 = new DataTable();
                odda.Fill(dt1);
                foreach (DataRow dr in dt.Rows)
                {
                    dt1.ImportRow(dr);
                }
                OleDbCommandBuilder cb = new OleDbCommandBuilder(odda);
                odda.Update(dt1);
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public void UpdateDbf1(string FileName, string ZipName, string zip, string AddName, string add)
        {
            OdbcConnection cnn = new OdbcConnection("Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" + FileName + ";Exclusive=No;NULL=NO;Collate=Machine;BACKGROUNDFETCH=NO;DELETED=NO;");
            cnn.Open();
            string SQLstr = @"update " + FileName + " set  " + ZipName + "='" + zip + "' where " + AddName + "==?";
            OdbcCommand cmd = new OdbcCommand(SQLstr, cnn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("?", OdbcType.VarChar, 100).Value = add;
            cmd.ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();

        }

        public void CreatDbf(string SQLstr, string FileName)
        {
            string ole_connstring = @"Provider=VFPOLEDB.1;Data Source=" + FileName + ";";
            System.Data.OleDb.OleDbConnection ole_conn = new System.Data.OleDb.OleDbConnection(ole_connstring);
            try
            {
                ole_conn.Open();
                System.Data.OleDb.OleDbCommand cmd1 = new System.Data.OleDb.OleDbCommand
                         (SQLstr, ole_conn);
                //"Create Table TestTable (Field1 int, Field2 char(10),Field float(10,2))"
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                ole_conn.Close();
            }
        }
        //读取DBF
        public DataTable OpenDbf(string strFilePath)
        {
            string strConn = @"Provider=vfpoledb;Data Source=" + strFilePath + ";Collating Sequence=machine;";
            using (OleDbConnection myConnection = new OleDbConnection(strConn))
            {
                myConnection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SET DELETED OFF";
                cmd.ExecuteNonQuery();
                //myConnection.Close();
                //myConnection.Open();
                OleDbDataAdapter adpt = new OleDbDataAdapter("select * from ?", myConnection);
                adpt.SelectCommand.Parameters.AddWithValue("?", strFilePath);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                myConnection.Close();
                myConnection.Dispose();
                return dt;
            }
        }
        public DataTable OpenDbf(string strFilePath, int min, int max)
        {
            string strConn = @"Provider=vfpoledb;Data Source=" + strFilePath + ";Collating Sequence=machine;";
            using (OleDbConnection myConnection = new OleDbConnection(strConn))
            {
                myConnection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SET DELETED OFF";
                cmd.ExecuteNonQuery();
                //myConnection.Close();
                //myConnection.Open();
                OleDbDataAdapter adpt = new OleDbDataAdapter("select * from ?", myConnection);
                adpt.SelectCommand.Parameters.AddWithValue("?", strFilePath);
                DataTable dt = new DataTable();
                adpt.Fill(min, max, dt);

                myConnection.Close();
                myConnection.Dispose();
                return dt;
            }
        }
        public int dbfCount(string strFilePath)
        {
            string strConn = @"Provider=vfpoledb;Data Source=" + strFilePath + ";Collating Sequence=machine;";
            using (OleDbConnection myConnection = new OleDbConnection(strConn))
            {
                myConnection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SET DELETED OFF";
                cmd.ExecuteNonQuery();
                //myConnection.Close();
                //myConnection.Open();
                OleDbDataAdapter adpt = new OleDbDataAdapter("select count(*) from ?", myConnection);
                adpt.SelectCommand.Parameters.AddWithValue("?", strFilePath);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                int count = int.Parse(dt.Rows[0][0].ToString());
                return count;
            }
        }
        /// <summary>
        /// 写入DBF.DBF数据会被清空,file表必须和datatable的格式完全一致
        /// </summary>
        /// <param name="FullFile"></param>
        /// <param name="dt"></param>
        public void RewriteDBF(string FullFile, DataTable dt)
        {
            FileStream fz = new FileStream(@FullFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            int colSizeCount = 0;
            int colCount = 0;
            int headSize = 0;
            byte[] date = new byte[32];
            List<int> colSize = new List<int>();
            int rowCount = dt.Rows.Count;
            StringBuilder sb = new StringBuilder();
            bool headtemp = false;
            #region 读取文件有几个字段.每个字段的长度,计算头文件长度
            while (true)
            {
                fz.Read(date, 0, 32);
                headSize += 32;
                if (date[0] == 13)
                {
                    headSize -= 32;
                    break;
                }
                else if (headtemp == true)
                {
                    if (date[11] != 48)
                    {
                        colCount++;
                        colSize.Add((int)(date[16]));
                        colSizeCount += date[16];
                    }
                }
                headtemp = true;
            }
            #endregion
            fz.Position = 0;
            byte[] head = new byte[headSize + 264];
            byte[] body;
            byte[] end = new byte[1];
            end[0] = 26;

            for (int i = 0; i < rowCount; i++)
            {
                sb.Append(' ');
                for (int j = 0; j < colCount; j++)
                {
                    int bSize = Encoding.Default.GetByteCount(dt.Rows[i][j].ToString().Trim());
                    sb.Append(dt.Rows[i][j].ToString().Trim());
                    sb.Append("".PadRight(colSize[j] - bSize));
                }
            }
            fz.Position = 0;
            fz.Read(head, 0, head.Length);
            fz.Position = 0;
            head[4] = (byte)(rowCount % 256);
            head[5] = (byte)((rowCount % 65536) / 256);
            head[6] = (byte)(rowCount / 65536);
            fz.Write(head, 0, head.Length);
            int bodySize = 500000;
            for (int i = 0; i < sb.Length / bodySize; i++)
            {
                char[] c = new char[bodySize];
                sb.CopyTo(i * bodySize, c, 0, bodySize);
                body = Encoding.Default.GetBytes(c);
                fz.Write(body, 0, body.Length);
            }
            char[] Lastc = new char[sb.Length % bodySize];
            sb.CopyTo((int)((sb.Length / bodySize) * bodySize), Lastc, 0, (int)(sb.Length % bodySize));
            byte[] Last = Encoding.Default.GetBytes(Lastc);
            fz.Write(Last, 0, Last.Length);
            fz.Write(end, 0, 1);
            fz.Close();

        }
    }
}

