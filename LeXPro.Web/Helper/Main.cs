using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using MDMA.DB;
using System.Data.SqlClient;

namespace LeXPro.Core
{
    public class Main
    {
        public static string apppath;

        public static string mConnString = Func.ToStr(ConfigurationManager.ConnectionStrings["ConStr"]);

        public static string MyIP = Func.GetClientIPAddress();
        public static string MyHost = Environment.MachineName;

        public static string _WriteLog = Func.ToStr(ConfigurationManager.AppSettings["WriteLog"]);
        public static string _WriteTestLog = Func.ToStr(ConfigurationManager.AppSettings["WriteTestLog"]);
        public static string _WriteErrorLog = Func.ToStr(ConfigurationManager.AppSettings["WriteErrorLog"]);

        #region Server Connection

        public static string GetConStr()
        {
            return mConnString;
        }

        public static Result DataSetExecute(string sql)
        {
            Result res = new Result(true)
            {
                Data = CDataCon.ExecuteDataset(GetConStr(), System.Data.CommandType.Text, sql)
            };

            return res;
        }

        public static Result DataSetExecute(string sql, SqlParameter[] obj)
        {
            Result res = new Result(true);

            res.Data = CDataCon.ExecuteDataset(GetConStr(), System.Data.CommandType.Text, sql, obj);

            return res;
        }
        public static Result ExecuteNonQuery(string sql, SqlParameter[] obj)
        {
            Result res = new Result(true);

            res.Data = CDataCon.ExecuteNonQuery(GetConStr(), System.Data.CommandType.Text, sql, obj);

            return res;
        }
        public static Result ExecuteNonQuery(string sql)
        {
            Result res = new Result(true);

            res.Data = CDataCon.ExecuteNonQuery(GetConStr(), System.Data.CommandType.Text, sql);

            return res;
        }

        public static SqlDataReader ExecuteReader(string sql, SqlParameter[] obj)
        {
            return CDataCon.ExecuteReader(GetConStr(), System.Data.CommandType.Text, sql, obj);
        }
        public static SqlDataReader ExecuteReader(string sql)
        {
            return CDataCon.ExecuteReader(GetConStr(), System.Data.CommandType.Text, sql);
        }
        public static string GetConnectionFailedMessage(short lang)
        {
            if (lang == 0)
            {
                return "Холболт түр саатлаа! Түр хүлээгээд дахин нэвтэрнэ үү.";
            }
            else
                return "Cannot connect to the server! Try again later.";
        }
        #endregion


        #region Logging

        public static void TestLog(string func, string text)
        {
          
            try
            {
                StringBuilder tmpstr = new StringBuilder();
                if (apppath == null)
                {
                    string appPath = HttpContext.Current.Request.ApplicationPath;
                    apppath = HttpContext.Current.Request.MapPath(appPath);
                }
                string filename = Path.Combine(apppath, "Log\\TestLog_" + DateTime.Now.ToString("yyyyMMdd") + ".log");

                if (!Directory.Exists(apppath + "\\Log"))
                {
                    Directory.CreateDirectory(apppath + "\\Log");
                }

                tmpstr.Append(func + ": " + text);

                LogWrite(filename, tmpstr);
                tmpstr = null;
            }
            catch (Exception ex)
            {
                ErrorLog("TestLog", ex);
            }
        }

        public static void ErrorLog(string func, Exception pEx)
        {
            if (_WriteErrorLog != "Y")
                return;
            StringBuilder tmpstr = new StringBuilder();
            try
            {
                string filename = Path.Combine(apppath, "Log\\ErrorLog_" + DateTime.Now.ToString("yyyyMMdd") + ".log");

                if (!Directory.Exists(apppath + "\\Log"))
                {
                    Directory.CreateDirectory(apppath + "\\Log");
                }

                tmpstr.Append(func + "\n  Message: " + pEx.Message);
                tmpstr.Append("\n  Source: " + pEx.Source);
                tmpstr.Append("\n  ToString: " + pEx.ToString());

                LogWrite(filename, tmpstr);
                tmpstr = null;

            }
            catch (Exception ex)
            {
                //ErrorLog("TestLog", ex);
            }
        }
        public static void ErrorLog(string func, string pEx)
        {
            if (_WriteErrorLog != "Y")
                return;
            StringBuilder tmpstr = new StringBuilder();
            try
            {
                string filename = Path.Combine(apppath, "Log\\ErrorLog_" + DateTime.Now.ToString("yyyyMMdd") + ".log");

                if (!Directory.Exists(apppath + "\\Log"))
                {
                    Directory.CreateDirectory(apppath + "\\Log");
                }

                tmpstr.Append(func + "\n  Error: " + pEx);

                LogWrite(filename, tmpstr);
                tmpstr = null;

            }
            catch (Exception ex)
            {
                //ErrorLog("TestLog", ex);
            }
        }
        public static void LogWrite(string filename, StringBuilder pText)
        {
            StreamWriter sw = null;
            FileStream fs = null;
            using (fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                try
                {
                    sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + " : " + pText);
                }
                catch (Exception ex)
                {
                    //ErrorLog("LogWrite", ex);
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw.Dispose();

                        sw = null;
                    }

                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                }
            }
        }
        public static void LogWrite(string filename, String pText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(pText);
            LogWrite(filename, sb);
        }

        #endregion
    }
}
